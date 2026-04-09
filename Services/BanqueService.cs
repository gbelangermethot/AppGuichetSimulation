using Microsoft.EntityFrameworkCore;
using Projet_Guichet.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;

namespace Projet_Guichet.Services
{
    public static class BanqueService
    {
        public static bool creerCompte(int UtilisateurId, int TypeCompte)
        {
            using (var db = new GuichetContext())
            {

                var comptes = db.Comptes.Where(c => c.UtilisateurId == UtilisateurId).ToList();

                foreach (var compte in comptes)
                {
                    if (TypeCompte == compte.TypeId)
                    {
                        MessageBox.Show("L'utilisateur a déjà un compte de ce type", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }

                var nouveauCompte = new Compte
                {
                    UtilisateurId = UtilisateurId,
                    TypeId = TypeCompte,
                    DateCreation = DateOnly.FromDateTime(DateTime.Today)
                };

                db.Comptes.Add(nouveauCompte);
                db.SaveChanges();
                return true;
            }
        }

        public static Utilisateur creerClient(string Nom, string Prenom, string Telephone, string Courriel, int Nip)
        {
            using (var db = new GuichetContext())
            {
                var nouveauClient = new Utilisateur
                {
                    Nom = Nom,
                    Prenom = Prenom,
                    Telephone = Telephone,
                    Courriel = Courriel,
                    Nip = Nip,
                    RoleId = 2,
                    StatusId = 1,
                    DateCreation = DateOnly.FromDateTime(DateTime.Today)
                };

                db.Utilisateurs.Add(nouveauClient);
                db.SaveChanges();
                return nouveauClient;
            }
        }

        public static Compte getCompte(int utilisateurId, int typeId)
        {
            using (var db = new GuichetContext())
            {
                var compte = db.Comptes.FirstOrDefault(c => c.UtilisateurId == utilisateurId && c.TypeId == typeId);

                return compte;

            }
        }
        public static void Depot(int compteId, decimal montant)
        {
            using (var db = new GuichetContext())
            {
                var compte = db.Comptes.FirstOrDefault(c => c.Id == compteId);

                if (compte != null)
                {
                    compte.Solde += montant;

                    var nouvelleTransaction = new Transaction
                    {
                        CompteId = compteId,
                        TypeId = 1, // Assuming 1 is the ID for "Depot" in your TypeTransaction table
                        Montant = montant,
                        Date = DateOnly.FromDateTime(DateTime.Today),
                        Description = $"Dépôt au guichet de {montant:C2}"
                    };

                    db.Transactions.Add(nouvelleTransaction);

                    db.SaveChanges();
                }
                else
                {
                    // Échec : Aucun compte trouvé
                    MessageBox.Show("Erreur : Ce compte n'existe pas.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public static void Retrait(int compteId, decimal montant)
        {
            using (var db = new GuichetContext())
            {
                
                var compte = db.Comptes.FirstOrDefault(c => c.Id == compteId);
                var guichet = db.Guichets.FirstOrDefault();
                var typeTransaction = db.TypeTransactions.Where(t => t.Id == 2).FirstOrDefault();

                if (compte == null)
                {
                    MessageBox.Show("Compte non trouvé.",
                                    "Erreur Guichet", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (guichet == null)
                {
                    MessageBox.Show("Guichet non trouvé.",
                                    "Erreur Guichet", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                if (guichet.Balance < montant)
                {
                    MessageBox.Show("Le guichet n'a pas assez de fonds pour cette transaction.",
                                    "Erreur Guichet", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (montant > 1000)
                {
                    MessageBox.Show("Vous pouves retirer un maximum de 1000$",
                                    "Erreur Guichet", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (compte.Solde >= montant)
                {
                    
                    guichet.Balance -= montant;

                    FinaliserRetrait(db, compte, montant, $"Retrait au guichet de {montant:C2}");
                    db.SaveChanges();
                    MessageBox.Show("Retrait de " + montant + " $ Effectué avec succès", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                GererDecouvert(db, compte, guichet, montant, typeTransaction);
            }
        }

        private static bool GererDecouvert(GuichetContext db, Compte compte, Guichet guichet, decimal montant, TypeTransaction typeTransaction)
        {
            var margeCredit = db.Comptes.FirstOrDefault(c => c.UtilisateurId == compte.UtilisateurId && c.TypeId == 4);

            if (margeCredit == null)
            {
                MessageBox.Show(
                "Erreur: Fonds insuffisants dans le compte!",
                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            MessageBoxResult result = MessageBox.Show(
                "Erreur: Fonds insuffisants dans le compte. Voulez vous retirer la balance sur votre marge de crédit?",
                "Erreur", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
            {
                // Update Guichet physical cash
                if(typeTransaction.Id == 2)
                { 
                    guichet.Balance -= montant;
                }

                decimal montantCompte = compte.Solde;
                decimal montantMarge = montant - montantCompte;
                decimal montantFacture = montant - 1.25m;


                string descriptionCompte = typeTransaction.Id switch
                {
                    2 => $"Retrait de {montant:C2} effectué. {montantCompte:C2} a été debité du compte. {montantMarge:C2} a été mit sur la marge de crédit",
                    3 => $"Paiment de {montantFacture:C2} effectué. Charge de 1.25 $ ajoutée pour l'opération pour un total de {montant:C2} " +
                    $"{montantCompte:C2} a été debité du compte. {montantMarge:C2} a été mit sur la marge de crédit",
                    4 => $"Transfer de {montant:C2} effectué. {montantCompte:C2} a été debité du compte. {montantMarge:C2} a été mit sur la marge de crédit",
                    7 => $"Prélevement hypothéquaire de {montant:C2} effectué. {montantCompte:C2} a été debité du compte. {montantMarge:C2} a été mit sur la marge de crédit",
                    _ => $"Transaction de {montant:C2} effectué. {montantCompte:C2} a été debité du compte. {montantMarge:C2} a été mit sur la marge de crédit"
                };

                string descriptionMarge = typeTransaction.Id switch
                {
                    2 => $"Retrait de {montant:C2}  Couverture decouvert de {montantMarge:C2}",
                    3 => $"Paiement de facture de {montant:C2} Couverture decouvert de {montantMarge:C2}",
                    4 => $"Transfert de {montant:C2} Couverture decouvert de {montantMarge:C2}",
                    7 => $"Prélevement hypothéquaire de {montant:C2} Couverture decouvert de {montantMarge:C2}",
                    _ => $"Transaction de {montant:C2} Couverture decouvert de {montantMarge:C2}"
                };

                string operation = typeTransaction.Id switch
                {
                    2 => $"Retrait de {montantCompte:C2} Effectué sur votre compte et ",
                    3 => $"Paiement de facture de {montantFacture:C2} + une charge de 1.25 $ pour un total de {montant:C2} " +
                    $"a été effectué. {montantCompte:C2} a été debité de votre compte et ",
                    4 => $"Transfert de {montantCompte:C2} a partir de votre compte et ",
                    7 => $"Prélevement hypothéquaire de {montantCompte:C2} effectué sur votre compte et ",
                    _ => $"Transaction de {montantCompte:C2} effectué sur votre compte et )"
                };

                // 1. Empty the first account
                compte.Solde = 0;
                db.Transactions.Add(new Transaction
                {
                    CompteId = compte.Id,
                    TypeId = typeTransaction.Id,
                    Montant = montantCompte,
                    Date = DateOnly.FromDateTime(DateTime.Today),
                    Description = descriptionCompte
                });

                // 2. Take the rest from the Credit Line
                margeCredit.Solde -= montantMarge; // This will now be tracked!
                db.Transactions.Add(new Transaction
                {
                    CompteId = margeCredit.Id,
                    TypeId = typeTransaction.Id,
                    Montant = montantMarge,
                    Date = DateOnly.FromDateTime(DateTime.Today),
                    Description = descriptionMarge
                });

                db.SaveChanges();
                MessageBox.Show($"{operation}{montantMarge:C2} On été mis sur votre marge de crédit", 
                    "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                MessageBox.Show(
                "Transaction annulée",
                "Information", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private static void FinaliserRetrait(GuichetContext db, Compte compte, decimal montant, string description)
        {
            compte.Solde -= montant;
            db.Transactions.Add(new Transaction
            {
                CompteId = compte.Id,
                TypeId = 2,
                Montant = montant,
                Date = DateOnly.FromDateTime(DateTime.Today),
                Description = description
            });
        }

        public static void Transfer(int compteIdOrg, int compteIdDest, decimal montant)
        {
            using (var db = new GuichetContext())
            {

                var compteOrg = db.Comptes.Include(c => c.Type).FirstOrDefault(c => c.Id == compteIdOrg);
                var compteDest = db.Comptes.Include(c => c.Type).FirstOrDefault(c => c.Id == compteIdDest);
                var guichet = db.Guichets.FirstOrDefault();
                var typeTransaction = db.TypeTransactions.Where(t => t.Id == 4).FirstOrDefault();

                if (compteOrg == null)
                {
                    MessageBox.Show("Compte d'origine non trouvé.",
                                    "Erreur Guichet", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (compteDest == null)
                {
                    MessageBox.Show("Compte destinataire non trouvé.",
                                    "Erreur Guichet", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (compteOrg.Solde >= montant)
                {

                    FinaliserTransfer(db, compteOrg, compteDest, montant);
                    db.SaveChanges();
                    switch (compteDest.TypeId) {
                        case 2:
                            MessageBox.Show($"Transfer de {montant:C2} effectué avec succès vers le compte épargne", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        case 3:
                            MessageBox.Show($"Transfer de {montant:C2} effectué avec succès vers le compte hypothéquaire", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        case 4:
                            MessageBox.Show($"Transfer de {montant:C2} effectué avec succès vers la marge de crédit", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                    }
                }
                else
                {
                    if (GererDecouvert(db, compteOrg, guichet, montant, typeTransaction))
                    {
                        compteDest.Solde += montant;

                        db.Transactions.Add(new Transaction
                        {
                            CompteId = compteDest.Id,
                            TypeId = 5,
                            Montant = montant,
                            Date = DateOnly.FromDateTime(DateTime.Today),
                            Description = $"Transfer reçu du compte {compteOrg.Type.Nom} de {montant:C2}"
                        });
                        db.SaveChanges();
                    }
                }
            }
        }

        private static void FinaliserTransfer(GuichetContext db, Compte compteOrg, Compte compteDest, decimal montant)
        {
            compteOrg.Solde -= montant;
            
            db.Transactions.Add(new Transaction
            {
                CompteId = compteOrg.Id,
                TypeId = 4,
                Montant = montant,
                Date = DateOnly.FromDateTime(DateTime.Today),
                Description = $"Transfer vers le compte {compteDest.Type.Nom} de {montant:C2}"
            });

            compteDest.Solde += montant;

            db.Transactions.Add(new Transaction
            {
                CompteId = compteDest.Id,
                TypeId = 5,
                Montant = montant,
                Date = DateOnly.FromDateTime(DateTime.Today),
                Description = $"Transfer reçu du compte {compteOrg.Type.Nom} de {montant:C2}"
            });
        }

        public static void Paiementfacture(int compteId, decimal montantPaiement)
        {
            decimal montant = montantPaiement + 1.25m;

            using (var db = new GuichetContext())
            {

                var compte = db.Comptes.FirstOrDefault(c => c.Id == compteId);
                var guichet = db.Guichets.FirstOrDefault();
                var typeTransaction = db.TypeTransactions.Where(t => t.Id == 3).FirstOrDefault();

                if (compte == null)
                {
                    MessageBox.Show("Compte non trouvé.",
                                    "Erreur Guichet", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (guichet == null)
                {
                    MessageBox.Show("Guichet non trouvé.",
                                    "Erreur Guichet", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                if (compte.Solde >= montant)
                {

                    FinaliserPaiementFacture(db, compte, montant, $"Paiement de facture au guichet de {montantPaiement:C2} + une charge de 1.25 $ pour un total de {montant:C2}");
                    db.SaveChanges();
                    MessageBox.Show($"Paiement de {montant:C2} effectué avec succès, une charge de 1.25 $ a été ajouté pour le traitement de l'opération",
                        "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                GererDecouvert(db, compte, guichet, montant, typeTransaction);
            }
        }

        private static void FinaliserPaiementFacture(GuichetContext db, Compte compte, decimal montant, string description)
        {
            compte.Solde -= montant;
            db.Transactions.Add(new Transaction
            {
                CompteId = compte.Id,
                TypeId = 3,
                Montant = montant,
                Date = DateOnly.FromDateTime(DateTime.Today),
                Description = description
            });
        }

        public static void BloquerClient(Utilisateur client) 
        {
            using (var db = new GuichetContext())
            {
                db.Utilisateurs.Attach(client);

                if (client.StatusId == 1)
                {
                    client.StatusId = 2;
                    MessageBox.Show("l'utilisateur " + client + " a été bloqué",
                                    "Utilisateur Bloqué", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    client.StatusId = 1;
                    MessageBox.Show("l'utilisateur " + client + " é eté debloqué",
                                    "Utilisateur Debloqué", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                db.Entry(client).Property(x => x.StatusId).IsModified = true;
                db.SaveChanges();
            }
        }

        public static void PayerInterets() 
        {
            using (var db = new GuichetContext())
            {
                var comptesEpargne = db.Comptes.Where(c => c.TypeId == 2).ToList();
                if (comptesEpargne.Count == 0) return;

                foreach (var compte in comptesEpargne)
                {
                    decimal montantInteret = compte.Solde * 0.01m;
                    compte.Solde += montantInteret;

                    
                    db.Transactions.Add(new Transaction
                    {
                        CompteId = compte.Id,
                        Montant = montantInteret,
                        TypeId = 6, 
                        Description = "Intérêts ajoute (1%)",
                        Date = DateOnly.FromDateTime(DateTime.Today)
                    });
                }

                var guichet = db.Guichets.FirstOrDefault();

                db.OperationAdmins.Add(new OperationAdmin
                {
                    AdminId = App.CurrentUser.Id,
                    TypeId = 2,
                    GuichetId = guichet.Id,
                    Date = DateOnly.FromDateTime(DateTime.Today),
                    Description = "Intérêts ajoute aux comptes epargnes (1%)"
                });

                // 5. Sauvegarder tous les changements d'un coup
               db.SaveChanges();
            }
        }

        public static void CollecterInterets()
        {
            using (var db = new GuichetContext())
            {
                var margesCredit = db.Comptes.Where(c => c.TypeId == 4).ToList();
                if (margesCredit.Count == 0) return;

                foreach (var compte in margesCredit)
                {
                    if(compte.Solde < 0){ 
                        decimal montantInteret = compte.Solde * 0.05m;
                        compte.Solde += montantInteret;


                        db.Transactions.Add(new Transaction
                        {
                            CompteId = compte.Id,
                            Montant = montantInteret,
                            TypeId = 6,
                            Description = "Intérêts ajoute (5%)",
                            Date = DateOnly.FromDateTime(DateTime.Today)
                        });
                    }        
                }

                var guichet = db.Guichets.FirstOrDefault();

                db.OperationAdmins.Add(new OperationAdmin
                {
                    AdminId = App.CurrentUser.Id,
                    TypeId = 1,
                    GuichetId = guichet.Id,
                    Date = DateOnly.FromDateTime(DateTime.Today),
                    Description = "Intérêts ajoute aux Marges de credits (5%)"
                });

                // 5. Sauvegarder tous les changements d'un coup
                db.SaveChanges();
            }
        }

        public static void PrelevementHypothequaire(Compte compte, decimal montant)
        {
            using (var db = new GuichetContext())
            {
                var _compte = db.Comptes.FirstOrDefault(c => c.Id == compte.Id);
                var guichet = db.Guichets.FirstOrDefault();
                var typeTransaction = db.TypeTransactions.Where(t => t.Id == 7).FirstOrDefault();

                if (_compte.Solde >= montant)
                {

                    FinaliserPrelevement(db, _compte, montant);
                    db.SaveChanges();
                    MessageBox.Show($"Prélevement de {montant:C2} effectué avec succès",
                        "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                GererDecouvert(db, _compte, guichet, montant, typeTransaction);
            }
        }

        public static void FinaliserPrelevement(GuichetContext db, Compte compte, decimal montant)
        {
            compte.Solde -= montant;

            var nouvelleTransaction = new Transaction
            {
                CompteId = compte.Id,
                TypeId = 7,
                Montant = montant,
                Date = DateOnly.FromDateTime(DateTime.Today),
                Description = $"Prélevement hypothequaire de {montant:C2}"
            };

            db.Transactions.Add(nouvelleTransaction);

            var guichet = db.Guichets.FirstOrDefault();

            db.OperationAdmins.Add(new OperationAdmin
            {
                AdminId = App.CurrentUser.Id,
                TypeId = 2,
                GuichetId = guichet.Id,
                Date = DateOnly.FromDateTime(DateTime.Today),
                Description = $"Prélevement hypothéquaire de {montant:C2}"
            });
        }

        public static void RemplirGuichetPlein(GuichetContext db, Guichet guichet)
        {
            var balance = guichet.Balance;
            var montantAjoute = 20000.00m - balance;
            guichet.Balance = 20000.00m;
            db.OperationAdmins.Add(new OperationAdmin
            {
                AdminId = App.CurrentUser.Id,
                TypeId = 3,
                GuichetId = guichet.Id,
                Date = DateOnly.FromDateTime(DateTime.Today),
                Description = $"{montantAjoute:C2} ajouté au Guichet, Guichet rempli à 20000 $"
            });
            db.SaveChanges();
            MessageBox.Show($"{montantAjoute:C2} ajouté au Guichet, Guichet rempli à 20000 $",
                        "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void RemplirGuichet(GuichetContext db, Guichet guichet, decimal montant)
        { 
            var balance = guichet.Balance;
            if (balance + montant > 20000.00m)
            {
                MessageBox.Show($"Il ne peut pas y avoir plus de 20000 $ dans le guichet, Veuillez entrer un montant qui ne fera pas dépasser la limite",
                        "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            { 
                guichet.Balance += montant;
                db.OperationAdmins.Add(new OperationAdmin
                {
                    AdminId = App.CurrentUser.Id,
                    TypeId = 3,
                    GuichetId = guichet.Id,
                    Date = DateOnly.FromDateTime(DateTime.Today),
                    Description = $"{montant:C2} ajouté au guichet. Guichet rempli à {guichet.Balance:C2}"
                });
                MessageBox.Show($"{montant:C2} ajouté au guichet. Guichet rempli à {guichet.Balance:C2}",
                        "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                db.SaveChanges();
            }

        }
    }
}
