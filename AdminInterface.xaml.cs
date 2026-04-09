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

namespace Projet_Guichet
{
    /// <summary>
    /// Interaction logic for AdminInterface.xaml
    /// </summary>
    public partial class AdminInterface : Page
    {
        public AdminInterface()
        {
            InitializeComponent();
        }

        private void btnCreerClient_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CreationClient());
        }

        private void btnCreerCompte_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CreationCompte());
        }

        private void btnListeClients_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ClientListe());
        }

        private void btnPayerInterets_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Intérêts versés aux comptes épargne", "comfirmation d'opération", MessageBoxButton.OK, MessageBoxImage.Information);
            BanqueService.PayerInterets();
        }

        private void btnCollecterInterets_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Intérêts collectés des marges de crédit", "comfirmation d'opération", MessageBoxButton.OK, MessageBoxImage.Information);
            BanqueService.CollecterInterets();
        }

        private void btnPrelevementHypothequaire_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PrelevementHypothequaire());
        }

        private void btnRemplitGuichet_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RemplirGuichet());
        }

        private void btnVoirTransactions_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Transactions());
        }

        private void btnFermerGuicher_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
}

        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }

        private void btnListeComptes_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CompteListe());
        }

    }
}
