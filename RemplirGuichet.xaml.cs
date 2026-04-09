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

namespace Projet_Guichet
{
    /// <summary>
    /// Interaction logic for RemplirGuichet.xaml
    /// </summary>
    public partial class RemplirGuichet : Page
    {

        private decimal montant = 00.00m;
        private long _cents = 0;
        public RemplirGuichet()
        {
            InitializeComponent();
            this.DataContext = this;
            MyKeyboard.OnKeyPressed += HandleKeyboardInput;
            afficherBalance();
        }

        private void afficherBalance()
        {
            using (var db = new GuichetContext())
            {
                var guichet = db.Guichets.FirstOrDefault();
                tbBalance.Text += $"{guichet.Balance:C2}";
            }
        }
        private void HandleKeyboardInput(string key)
        {
            if (key == "Éffacer" || key == "Effacer")
            {
                _cents = 0;
            }
            else if (key == "Corriger")
            {
                _cents /= 10; 
            }
            else if (int.TryParse(key, out int digit))
            {
                
                if (_cents < 10000000)
                    _cents = (_cents * 10) + digit;
            }

            montant = _cents / 100m;

            txtMontant.Text = montant.ToString("C2");
        }

        private void btnRemplir_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new GuichetContext()) 
            {
                var guichet = db.Guichets.FirstOrDefault();
                BanqueService.RemplirGuichetPlein(db, guichet);
                this.NavigationService.Navigate(new RemplirGuichet());
            }
        }

        private void btnSoumettre_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new GuichetContext())
            {
                var guichet = db.Guichets.FirstOrDefault();
                BanqueService.RemplirGuichet(db, guichet, montant);
                this.NavigationService.Navigate(new RemplirGuichet());
            }
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AdminInterface());
        }
    }
}
