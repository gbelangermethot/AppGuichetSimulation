using Projet_Guichet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Logique d'interaction pour CreationClient.xaml
    /// </summary>
    public partial class CreationClient : Page
    {
       

        private string Nom = "";
        private string Prenom = "";
        private string Telephone = "";
        private string Courriel = "";

        
        public CreationClient()
        {
            InitializeComponent();
        }

        private void btnSoumettre_Click(object sender, RoutedEventArgs e)
        {
            Nom = txtNom.Text;
            Prenom = txtPrenom.Text;
            Telephone = txtTelephone.Text;
            Courriel = txtCourriel.Text;

            if (Nom != "" && Prenom != "" && Telephone != "" && Courriel != "")
            this.NavigationService.Navigate(new ChoixNIP(Nom, Prenom, Telephone, Courriel));
            else
            MessageBox.Show("Veuillez remplir tout les champs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AdminInterface());
        }
    }
}
