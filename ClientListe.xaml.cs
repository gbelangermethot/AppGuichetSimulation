using Projet_Guichet.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
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
    /// Logique d'interaction pour ClientListe.xaml
    /// </summary>
    public partial class ClientListe : Page
    {
        public List<Utilisateur> MesClients { get; set; } = new List<Utilisateur>();
        public ClientListe()
        {
            InitializeComponent();
            ChargerDonnees();
        }

        private void ChargerDonnees()
        {
            using (var db = new GuichetContext())
            {
                var query = db.Utilisateurs
                              .Where(u => u.RoleId == 2)
                              .AsQueryable();

                if (int.TryParse(ClientIdFilter.Text, out int Id))
                    query = query.Where(u => u.Id == Id);

                if (!string.IsNullOrWhiteSpace(NomFilter.Text))
                    query = query.Where(u => u.Nom.Contains(NomFilter.Text));

                if (!string.IsNullOrWhiteSpace(PrenomFilter.Text))
                    query = query.Where(u => u.Prenom.Contains(PrenomFilter.Text));

                if (!string.IsNullOrWhiteSpace(CourrielFilter.Text))
                    query = query.Where(u => u.Courriel.Contains(CourrielFilter.Text));


                if (!string.IsNullOrWhiteSpace(TelephoneFilter.Text))
                    query = query.Where(u => u.Telephone.Contains(TelephoneFilter.Text));

                MesClients = query.OrderBy(u => u.Nom).ToList();
            }

            this.DataContext = null;
            this.DataContext = this;
        }

        private void Filtre_Changed(object sender, EventArgs e)
        {
            ChargerDonnees();
        }
        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AdminInterface());
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Caster le SelectedItem a un objet Utilisateur
            if (sender is ListBox listBox && listBox.SelectedItem is Utilisateur selectedUtilisateur)
            {
                // Naviguer au details du client
                this.NavigationService.Navigate(new ClientDetails(selectedUtilisateur));

                // resetter la selection.
                listBox.SelectedIndex = -1;
            }
        }
    }
}
