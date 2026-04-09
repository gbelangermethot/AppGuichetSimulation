using Projet_Guichet.Models;
using Projet_Guichet.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.EntityFrameworkCore; 


namespace Projet_Guichet
{
    /// <summary>
    /// Logique d'interaction pour InterfaceTransfer.xaml
    /// </summary>
    public partial class InterfaceTransfer : Page
    {
        public ObservableCollection<Compte> ListeComptes { get; set; }

        public void ChargerComptes()
        {
            using (var db = new GuichetContext())
            {
                // Fetch users and convert to a list
                var comptes = db.Comptes
                                .Include(c => c.Type)
                                .Where(c => c.UtilisateurId == App.CurrentUser.Id && c.TypeId != 1)
                                .ToList();

                // Populate the observable collection
                ListeComptes = new ObservableCollection<Compte>(comptes);
            }

            // Ensure the UI knows the data is ready
            this.DataContext = this;
        }

        private decimal montant = 00.00m;
        private long _cents = 0;

        private Compte _compteOrg;
        
        public InterfaceTransfer(Compte compte)
        {
            _compteOrg = compte;
            InitializeComponent();
            ChargerComptes();
            this.DataContext = this;
            MyKeyboard.OnKeyPressed += HandleKeyboardInput;
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

        private void btnSoumettre_Click(object sender, RoutedEventArgs e)
        {
            int selectedCompteId = 0;

            if (cmbCompte.SelectedValue != null)
                selectedCompteId = (int)cmbCompte.SelectedValue;
            
            if (selectedCompteId != 0)
            {
                BanqueService.Transfer(_compteOrg.Id, selectedCompteId, montant);
                this.NavigationService.Navigate(new UserInterface());
            }
            else 
            {
                MessageBox.Show("Veuillez selectionner un compte destinataire", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new UserInterface());
        }
    }
}
