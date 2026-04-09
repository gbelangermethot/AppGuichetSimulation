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
using Microsoft.EntityFrameworkCore;
using Projet_Guichet.Models;

namespace Projet_Guichet
{
    /// <summary>
    /// Logique d'interaction pour CompteListe.xaml
    /// </summary>
    public partial class CompteListe : Page
    {
        public List<Compte> MesComptes { get; set; } = new List<Compte>();
        public CompteListe()
        {
            InitializeComponent();
            ChargerFiltres();
            ChargerDonnees();
        }

        private void ChargerDonnees()
        {
            using (var db = new GuichetContext())
            {
                var query = db.Comptes
                              .Include(c => c.Utilisateur)
                              .Include(c => c.Type)
                              .AsQueryable();

                if (int.TryParse(CompteIdFilter.Text, out int Id))
                    query = query.Where(c => c.Id == Id);

                if (!string.IsNullOrWhiteSpace(NomFilter.Text))
                    query = query.Where(c => c.Utilisateur.Nom.Contains(NomFilter.Text));

                if (!string.IsNullOrWhiteSpace(PrenomFilter.Text))
                    query = query.Where(c => c.Utilisateur.Prenom.Contains(PrenomFilter.Text));

                if (!string.IsNullOrWhiteSpace(CourrielFilter.Text))
                    query = query.Where(c => c.Utilisateur.Courriel.Contains(CourrielFilter.Text));


                if (TypeFilter.SelectedItem is TypeCompte selectedType && selectedType.Id > 0)
                {
                    query = query.Where(c => c.TypeId == selectedType.Id);
                }

                MesComptes = query.OrderBy(c => c.Id).ToList();
            }

            this.DataContext = null;
            this.DataContext = this;
        }

        private void Filtre_Changed(object sender, EventArgs e)
        {
            ChargerDonnees();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 1. Cast the SelectedItem to a Transaction object
            if (sender is ListBox listBox && listBox.SelectedItem is Compte selectedCompte)
            {
                // 2. Navigate to TransactionDetails, passing the selected object
                this.NavigationService.Navigate(new Transactions(selectedCompte));

                // 3. Optional: Reset selection so the item isn't "stuck" as blue when you come back
                listBox.SelectedIndex = -1;
            }
        }

        public void ChargerFiltres()
        {
            using (var db = new GuichetContext())
            {
                
                var types = db.TypeComptes.OrderBy(t => t.Description).ToList();

                
                types.Insert(0, new TypeCompte { Id = 0, Description = "-- Tous les types --" });

                TypeFilter.ItemsSource = types;
                TypeFilter.DisplayMemberPath = "Description";

                TypeFilter.SelectedIndex = 0;
            }
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AdminInterface());
        }
    }
}
