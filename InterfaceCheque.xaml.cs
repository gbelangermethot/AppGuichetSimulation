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
using Projet_Guichet.Services;
using Projet_Guichet.Models;

namespace Projet_Guichet
{
    /// <summary>
    /// Interaction logic for InterfaceCheque.xaml
    /// </summary>
    public partial class InterfaceCheque : Page
    {

        private Compte compte = BanqueService.getCompte(App.CurrentUser.Id, 1);
        public InterfaceCheque()
        {
            InitializeComponent();
            tbEntete.Text += $"Solde: {compte.Solde:C2}";
        }

        
        private void btnDepot_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InterfaceDepot(compte));
        }

        private void btnRetrait_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InterfaceRetrait(compte));
        }

        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InterfaceTransfer(compte));
        }

        private void btnPaiement_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InterfacePaiementFacture(compte));
        }

        private void btnTransactions_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Transactions(compte));
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new UserInterface());
        }
    }
}
