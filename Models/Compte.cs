using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Projet_Guichet.Models;

public partial class Compte
{
    public int Id { get; set; }

    public int UtilisateurId { get; set; }

    public int TypeId { get; set; }

    public decimal Solde { get; set; }

    public DateOnly DateCreation { get; set; }

    public string DisplayCompte => $"{(Type?.Nom ?? "Hypothèque")} {Id} - {(Utilisateur?.Nom ?? "Client")}, {(Utilisateur?.Prenom ?? "")}";


    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual TypeCompte Type { get; set; } = null!;

    public virtual Utilisateur Utilisateur { get; set; } = null!;

    public override string ToString()
    {
        return $"{(Type?.Nom ?? "Inconnu")} {Id} - {(Utilisateur?.Nom ?? "Client")}, {(Utilisateur?.Prenom ?? "")} - Solde: {Solde:C2}";
    }


}
