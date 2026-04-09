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
        /// Logique d'interaction pour Transactions.xaml
        /// </summary>
        public partial class TransactionDetails : Page
        {
            private Compte? _compte;
            private Transaction _transaction;

            public TransactionDetails(Transaction transaction, Compte? compte = null )
            {
                InitializeComponent();
                _compte = compte;
                using (var db = new GuichetContext())
                {
                    var transactionAffiche = db.Transactions.Include(t => t.Type)
                                                            .Include(t => t.Compte)
                                                                .ThenInclude(c => c.Type)
                                                            .FirstOrDefault(t => t.Id == transaction.Id);
                    _transaction = transactionAffiche;
                }
                tbEntete.Text += _transaction.Id;
                tbCompte.Text += _transaction.Compte.Type.Description + " " + _transaction.Compte.Id;
                tbType.Text += _transaction.Type.Description;
                tbMontant.Text += $"{_transaction.Montant:C2}";
                tbDate.Text += _transaction.Date;
                tbDescription.Text += _transaction.Description;
            }

            

            private void btnRetour_Click(object sender, RoutedEventArgs e)
            {   
                this.NavigationService.Navigate(new Transactions(_compte));
            }
        }
    }


