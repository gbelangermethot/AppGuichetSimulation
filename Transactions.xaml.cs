using Microsoft.EntityFrameworkCore;
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
    /// Logique d'interaction pour Transactions.xaml
    /// </summary>
    public partial class Transactions : Page
    {
        private Compte? _compte;
        private string NomComplet;
        private TextBox? _activeTextBox;


        public List<Transaction> MesTransactions { get; set; } = new List<Transaction>();
        public Transactions(Compte? compte = null)
        {
            InitializeComponent();
            _compte = compte;
            
            tbEntete.Text += NomComplet;
            if (compte != null) 
            {
                CompteIdFilter.Text = compte.Id.ToString();
                CompteIdFilter.IsReadOnly = true;
                CompteIdFilter.Background = Brushes.LightGray;
            }
            if (App.CurrentUser.RoleId == 2)
            {
                UtilisateurIdFilter.Text = App.CurrentUser.Id.ToString();
                UtilisateurIdFilter.IsReadOnly = true;
                UtilisateurIdFilter.Background = Brushes.LightGray; 
            }
            MyKeyboard.OnKeyPressed += HandleKeyboardInput;
            ChargerFiltres();
            ChargerDonnees();
        }

        private void ChargerDonnees()
        {
            using (var db = new GuichetContext())
            {
                var query = db.Transactions
                              .Include(t => t.Type)
                              .Include(t => t.Compte)
                              .AsQueryable();

                if (int.TryParse(TransactionIdFilter.Text, out int tId))
                    query = query.Where(t => t.Id == tId);

                if (int.TryParse(CompteIdFilter.Text, out int cId))
                    query = query.Where(t => t.CompteId == cId);

                if (int.TryParse(UtilisateurIdFilter.Text, out int uId))
                    query = query.Where(t => t.Compte.UtilisateurId == uId);

                if (TypeFilter.SelectedItem is TypeTransaction selectedType && selectedType.Id > 0)
                {
                    query = query.Where(t => t.TypeId == selectedType.Id);
                }

                MesTransactions = query.OrderByDescending(t => t.Id).ToList();
            }

            this.DataContext = null;
            this.DataContext = this;
        }

        public void ChargerFiltres()
        {
            using (var db = new GuichetContext())
            {
                // 1. Get the real types from the DB
                var types = db.TypeTransactions.OrderBy(t => t.Nom).ToList();

                // 2. Add the "All" option at index 0
                types.Insert(0, new TypeTransaction { Id = 0, Description = "-- Tous les types --" });

                // 3. Bind to the ComboBox
                TypeFilter.ItemsSource = types;
                TypeFilter.DisplayMemberPath = "Description";

                // Pre-select "All"
                TypeFilter.SelectedIndex = 0;
            }
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            if (App.CurrentUser.RoleId == 2)
                this.NavigationService.Navigate(new UserInterface());
            else
                this.NavigationService.Navigate(new AdminInterface());
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 1. Cast the SelectedItem to a Transaction object
            if (sender is ListBox listBox && listBox.SelectedItem is Transaction selectedTransaction)
            {
                // 2. Navigate to TransactionDetails, passing the selected object
                this.NavigationService.Navigate(new TransactionDetails(selectedTransaction, _compte));

                // 3. Optional: Reset selection so the item isn't "stuck" as blue when you come back
                listBox.SelectedIndex = -1;
            }
        }

        private void Filtre_Changed(object sender, EventArgs e)
        {
            ChargerDonnees();
        }

        private void CompteIdFilter_PreviewMouseDown(object sender, EventArgs e) 
        {
            if (!CompteIdFilter.IsReadOnly)
                _activeTextBox = CompteIdFilter;
        }

        private void TransactionIdFilter_PreviewMouseDown(object sender, EventArgs e)
        {
            _activeTextBox = TransactionIdFilter;
        }

        private void UtilisateurIdFilter_PreviewMouseDown(object sender, EventArgs e)
        {
            if (!UtilisateurIdFilter.IsReadOnly)
                _activeTextBox = UtilisateurIdFilter;
        }
        private void HandleKeyboardInput(string key)
        {
            if (_activeTextBox == null) return;

            if (key == "Éffacer" || key == "Effacer")
            {
                _activeTextBox.Text = "";
            }
            else if (key == "Corriger")
            {
                if (_activeTextBox.Text.Length > 0)
                {
                    _activeTextBox.Text = _activeTextBox.Text.Remove(_activeTextBox.Text.Length - 1);
                }
            }
            else
            {
                _activeTextBox.Text += key;
            }
        }
    }
}
