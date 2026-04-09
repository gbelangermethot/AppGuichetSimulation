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
using System.Linq;
using Microsoft.EntityFrameworkCore.Query;

namespace Projet_Guichet
{
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _userId = "";
        private string _nip = "";
        private bool _isNipActive = false;

        public string UserId
        {
            get => _userId;
            set { _userId = value; OnPropertyChanged(nameof(UserId)); }
        }

        public string Nip
        {
            get => _nip;
            set { _nip = value; OnPropertyChanged(nameof(Nip)); }
        }
        public Login()
        {
            InitializeComponent();
            this.DataContext = this;
            MyKeyboard.OnKeyPressed += HandleKeyboardInput;
        }

        //private void HandleKeyboardInput(string key)
        //{
        //    string currentVal = _isNipActive ? Nip : UserId;

        //    if (key == "Éffacer" || key == "Effacer")
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

        //    if (_isNipActive) Nip = currentVal;
        //    else UserId = currentVal;
        //}

        private void HandleKeyboardInput(string key)
        {
            if (_isNipActive)
            {
                if (key == "Effacer" || key == "Effacer")
                {
                    txtNIP.Password = "";
                }
                else if (key == "Corriger")
                {
                    if (txtNIP.Password.Length > 0)
                    {
                        txtNIP.Password = txtNIP.Password.Remove(txtNIP.Password.Length - 1);
                    }
                }
                else
                {
                    txtNIP.Password += key;
                }

                Nip = txtNIP.Password;
            }
            else
            {
                if (key == "Éffacer" || key == "Effacer") UserId = "";
                else if (key == "Corriger")
                {
                    if (UserId.Length > 0) UserId = UserId.Remove(UserId.Length - 1);
                }
                else UserId += key;
            }
        }

        protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void txtUserId_PreviewMouseDown(object sender, MouseButtonEventArgs e) => _isNipActive = false;
        private void txtNip_PreviewMouseDown(object sender, MouseButtonEventArgs e) => _isNipActive = true;

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (UserId == "") UserId = "0";
            if (Nip == "") Nip = "0";
            int code = int.Parse(UserId);
            int nip = int.Parse(Nip);

            using (var db = new GuichetContext())
            {
                // On cherche la correspondance dans la base de données
                var user = db.Utilisateurs.FirstOrDefault(u => u.Id == code && u.Nip == nip);

                if (user != null && user.StatusId == 1)
                {
                    // 1. On stocke l'utilisateur dans la variable globale
                    App.CurrentUser = user;

                    // 2. Redirection selon le RoleId
                    if (user.RoleId == 1) // Admin
                    {
                        this.NavigationService.Navigate(new AdminInterface());
                    }
                    else // User (RoleId == 2)
                    {
                        this.NavigationService.Navigate(new UserInterface());
                    }
                }
                else if(user != null && user.StatusId == 2)
                {
                    MessageBox.Show("Votre compte a été bloqué, veuillez contacter un administrateur pour le réactiver.", 
                        "Compte bloqué", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    // 3. Échec de connexion
                    MessageBox.Show("Code client ou NIP incorrect.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    Nip = ""; // On efface le NIP par sécurité
                }
            }
        }

        
    }
}
