using System.Configuration;
using System.Data;
using System.Windows;

namespace Projet_Guichet
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Projet_Guichet.Models.Utilisateur CurrentUser { get; set; }
    }

}
