using Projet_Guichet.Models;
using Projet_Guichet.Services;
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
    /// Interaction logic for interfaceHypothequaire.xaml
    /// </summary>
    public partial class interfaceHypothequaire : Page
    {

        private Compte compte = BanqueService.getCompte(App.CurrentUser.Id, 3);
        public interfaceHypothequaire()
        {
            InitializeComponent();
            tbEntete.Text += $"Solde: {compte.Solde:C2}";
        }

        private void btnDepot_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InterfaceDepot(compte));
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
