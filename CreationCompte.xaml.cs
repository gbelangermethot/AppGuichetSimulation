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
using System.Collections.ObjectModel;
using System.Linq;
using Projet_Guichet.Services;

namespace Projet_Guichet
{
    /// <summary>
    /// Logique d'interaction pour CreationCompte.xaml
    /// </summary>
    public partial class CreationCompte : Page
    {

        public ObservableCollection<Utilisateur> ListeUtilisateurs { get; set; }

        public void ChargerUtilisateurs()
        {
            using (var db = new GuichetContext())
            {
                // Fetch users and convert to a list
                var users = db.Utilisateurs.Where(u => u.RoleId == 2).ToList();

                // Populate the observable collection
                ListeUtilisateurs = new ObservableCollection<Utilisateur>(users);
            }

            // Ensure the UI knows the data is ready
            this.DataContext = this;
        }

        public ObservableCollection<TypeCompte> ListeTypeComptes { get; set; }

        public void ChargerTypeComptes() {
            using (var db = new GuichetContext())
            { 
                var TypeComptes = db.TypeComptes.ToList();
                
                ListeTypeComptes = new ObservableCollection<TypeCompte>(TypeComptes);
            }

            this.DataContext = this;
        }

        public CreationCompte()
        {
            InitializeComponent();
            ChargerUtilisateurs();
            ChargerTypeComptes();
        }

        private void btnSoumettre_Click(object sender, RoutedEventArgs e)
        {
            int selectedClientId = 0;
            int selectedTypeCompteId = 0;

            if (cmbClient.SelectedValue != null)
                selectedClientId = (int)cmbClient.SelectedValue;
            if (cmbType.SelectedValue != null)
                selectedTypeCompteId = (int)cmbType.SelectedValue;

            if (selectedClientId != 0 && selectedTypeCompteId != 0)
            {
                if (BanqueService.creerCompte(selectedClientId, selectedTypeCompteId))
                {
                    MessageBox.Show("Nouveau compte crée avec succès", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Veuillez selectionner un utilisateur et un type de compte", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AdminInterface());
        }
    }
}
