using Microsoft.EntityFrameworkCore;
using Projet_Guichet.Models;
using Projet_Guichet.Services;
using Projet_Guichet.View.UserControls;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Projet_Guichet
{
    /// <summary>
    /// Logique d'interaction pour ClientDetails.xaml
    /// </summary>
    public partial class ClientDetails : Page
    {

        private Utilisateur _client;
        private Compte _compteCheque;
        private Compte _compteEpargne;
        private Compte _compteHypothequaire;
        private Compte _margeCredit;

        public ClientDetails(Utilisateur utilisateur)
        {
            InitializeComponent();
            using (var db = new GuichetContext())
            {
                var client = db.Utilisateurs.Include(u => u.Status)
                                            .Include(u => u.Comptes)
                                                .ThenInclude(c => c.Type)
                                            .FirstOrDefault(u => u.Id == utilisateur.Id);
                _client = client;

            }
            tbEntete.Text += _client.Id;
            tbNom.Text += _client.Nom;
            tbPrenom.Text += _client.Prenom;
            tbTelephone.Text += _client.Telephone;
            tbCourriel.Text += _client.Courriel;
            tbStatus.Text += _client.Status.Nom;
            tbDate.Text += _client.DateCreation;

            _compteCheque = BanqueService.getCompte(_client.Id, 1);
            _compteEpargne = BanqueService.getCompte(_client.Id, 2);
            _compteHypothequaire = BanqueService.getCompte(_client.Id, 3);
            _margeCredit = BanqueService.getCompte(_client.Id, 4);

            ActualiserBouton(btnCheque, _compteCheque);
            ActualiserBouton(btnEpargne, _compteEpargne);
            ActualiserBouton(btnHypothequaire, _compteHypothequaire);
            ActualiserBouton(btnMargeCredit, _margeCredit);

            if (_client.StatusId == 1)
            {
                btnBloquer.BtnText = "Bloquer";
            }
            else
            {
                btnBloquer.BtnText = "Debloquer";
            }
        }

        private void ActualiserBouton(CustomBtn btn, Compte compte)
        {
            if (compte != null)
            {
                btn.IsEnabled = true;
                btn.BtnText += compte.Solde < 0 ?
                    $"-{Math.Abs(compte.Solde):C2}" :
                    $"{compte.Solde:C2}";
            }
            else
            {
                btn.IsEnabled = false;
                btn.BtnText += "Aucun compte";
            }
        }

        private void btnCheque_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Transactions(_compteCheque));
        }

        private void btnEpargne_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Transactions(_compteEpargne));
        }

        private void btnHypothequaire_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Transactions(_compteHypothequaire));
        }

        private void btnMargeCredit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Transactions(_margeCredit));
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ClientListe());
        }

        private void btnBloquer_Click(object sender, RoutedEventArgs e)
        {
            BanqueService.BloquerClient(_client);
            this.NavigationService.Navigate(new ClientDetails(_client));
        }
    }

       
}
