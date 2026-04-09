using Projet_Guichet.Models;
using Projet_Guichet.Services;
using System;
using System.Collections.Generic;
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
using Projet_Guichet.View.UserControls;

namespace Projet_Guichet
{
    /// <summary>
    /// Interaction logic for UserInterface.xaml
    /// </summary>
    public partial class UserInterface : Page
    {
        public UserInterface()
        {
            InitializeComponent();
            VerifierComptes();
        }
        private void VerifierComptes()
        {
            int userId = App.CurrentUser.Id;

            // Check each account type (assuming: 1=Cheque, 2=Epargne, 3=Hypo, 4=Marge)
            ActualiserBouton(btnCheque, BanqueService.getCompte(userId, 1));
            ActualiserBouton(btnEpargne, BanqueService.getCompte(userId, 2));
            ActualiserBouton(btnHypothequaire, BanqueService.getCompte(userId, 3));
            ActualiserBouton(btnMargeCredit, BanqueService.getCompte(userId, 4));
        }

        private void ActualiserBouton(CustomBtn btn, Compte compte)
        {
            if (compte != null)
            {
                btn.IsEnabled = true;
                // The CustomBtn will automatically set Opacity to 1.0
                btn.BtnText += compte.Solde < 0  ? 
                    $"-{Math.Abs(compte.Solde):C2}" :
                    $"{compte.Solde:C2}";
            }
            else
            {
                btn.IsEnabled = false;
                // The CustomBtn will automatically set Opacity to 0.4 (or whatever you set in the Trigger)
                btn.BtnText += "Aucun compte";
            }
        }
        private void btnCheque_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InterfaceCheque());
        }

        private void btnEpargne_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InterfaceEpargne());
        }

        private void btnHypothequaire_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new interfaceHypothequaire());
        }

        private void btnMargeCredit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InterfaceMargeCredit());
        }

        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }
    }
}
