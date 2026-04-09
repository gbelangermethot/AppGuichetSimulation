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
using Projet_Guichet.Services;

namespace Projet_Guichet
{
    /// <summary>
    /// Logique d'interaction pour ChoixNIP.xaml
    /// </summary>
    public partial class ChoixNIP : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _Nip1;
        private string _Nip2;
        private bool _isNip2Active = false;

        public string Nip1
        {
            get => _Nip1;
            set { _Nip1 = value; OnPropertyChanged(nameof(Nip1)); }
        }

        public string Nip2
        {
            get => _Nip2;
            set { _Nip2 = value; OnPropertyChanged(nameof(Nip2)); }
        }
        protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void txtNip1_PreviewMouseDown(object sender, MouseButtonEventArgs e) => _isNip2Active = false;
        private void txtNip2_PreviewMouseDown(object sender, MouseButtonEventArgs e) => _isNip2Active = true;

        private string _Nom;
        private string _Prenom;
        private string _Telephone;
        private string _Courriel;
        private int _Nip;
        

        public ChoixNIP(string Nom, string Prenom, string Telephone, string Courriel)
        {
            _Nom = Nom;
            _Prenom = Prenom;
            _Telephone = Telephone;
            _Courriel = Courriel;

            InitializeComponent();
            this.DataContext = this;
            MyKeyboard.OnKeyPressed += HandleKeyboardInput;
        }

        //private void HandleKeyboardInput(string key)
        //{
        //    string currentVal = _isNip2Active ? Nip2 : Nip1;

        //    if (key == "Éffacer")
        //    {
        //        currentVal = "";
        //    }
        //    else if (key == "Corriger")
        //    {
        //        if (currentVal.Length > 0)
        //        {
        //            currentVal = currentVal.Remove(currentVal.Length - 1);
        //        }
        //    }
        //    else
        //    {
        //        currentVal += key;
        //    }

        //    if (_isNip2Active) Nip2 = currentVal;
        //    else Nip1 = currentVal;
        //}

        private void HandleKeyboardInput(string key)
        {
            // Determine which PasswordBox to target
            PasswordBox activeBox = _isNip2Active ? txtNip2 : txtNip1;

            // 1. Handle "Éffacer" (Clear) - Note: check your button text for accents!
            if (key.Contains("ffacer"))
            {
                activeBox.Password = "";
            }
            // 2. Handle "Corriger" (Backspace)
            else if (key.Contains("orriger"))
            {
                if (activeBox.Password.Length > 0)
                {
                    activeBox.Password = activeBox.Password.Remove(activeBox.Password.Length - 1);
                }
            }
            // 3. Handle Numbers (Single digits)
            else if (key.Length == 1)
            {
                activeBox.Password += key;
            }

            // Sync your local variables (Nip1/Nip2) if you use them for the Submit logic
            if (_isNip2Active) Nip2 = txtNip2.Password;
            else Nip1 = txtNip1.Password;
        }

        private void btnSoumettre_Click(object sender, RoutedEventArgs e)
        {
            if (Nip1 != null && Nip2 != null)
            {
                if (Nip1 == Nip2)
                {
                    _Nip = int.Parse(Nip1);
                    var Utilisateur = BanqueService.creerClient(_Nom, _Prenom, _Telephone, _Courriel, _Nip);
                    BanqueService.creerCompte(Utilisateur.Id,1 );
                    MessageBox.Show("Nouveau client " + _Nom + ", " + _Prenom + " crée avec succès", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.NavigationService.Navigate(new AdminInterface());
                }
                else
                {
                    MessageBox.Show("Veuillez entrer des Nips identiques dans les 2 champs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    Nip1 = ""; // On efface le NIP par sécurité
                    Nip1 = "";
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer le Nip dans les 2 champs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                Nip1 = ""; // On efface le NIP par sécurité
                Nip1 = "";
            }
        }
    }
}
