using Projet_Guichet.Models;
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

namespace Projet_Guichet
{
    /// <summary>
    /// Logique d'interaction pour InterfaceDepot.xaml
    /// </summary>
    public partial class InterfaceDepot : Page
    {
        private decimal montant = 00.00m;
        private long _cents = 0;
        
        private Compte _compte;
        public InterfaceDepot(Compte compte)
        {
            _compte = compte;
            InitializeComponent();
            this.DataContext = this;
            MyKeyboard.OnKeyPressed += HandleKeyboardInput;
            switch (compte.TypeId) 
            {
                case 1: tbEntete.Text += "Chèque";
                    break;

                case 2: tbEntete.Text += "Épargne";
                    break;

                case 3: tbEntete.Text += "Hypothéquaire";
                    break;

                case 4: tbEntete.Text += "Marge de credit";
                    break;
            }
        }

        private void btnSoumettre_Click(object sender, RoutedEventArgs e)
        {
            BanqueService.Depot(_compte.Id, montant);
            this.NavigationService.Navigate(new UserInterface());
            MessageBox.Show($"dépot de {montant:C2} Effectué avec succès", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void HandleKeyboardInput(string key)
        {
            if (key == "Éffacer" || key == "Effacer")
            {
                _cents = 0;
            }
            else if (key == "Corriger")
            {
                _cents /= 10; // Removes the last digit
            }
            else if (int.TryParse(key, out int digit))
            {
                // Limit to 8 digits to prevent overflow (optional)
                if (_cents < 10000000)
                    _cents = (_cents * 10) + digit;
            }

            // Convert cents to decimal (e.g., 123 becomes 1.23)
            montant = _cents / 100m;

            // Format for display: "C2" is Currency format (01.23$)
            txtMontant.Text = montant.ToString("C2");
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new UserInterface());
        }


    }
}
