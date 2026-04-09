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

namespace Projet_Guichet.View.UserControls
{
    
    

    public partial class numericKeyboard : UserControl
    {

        public event Action<string>? OnKeyPressed;
        public numericKeyboard()
        {
            InitializeComponent();
        }

        private void SendKey(string key) => OnKeyPressed?.Invoke(key);

        private void btn1_Click(object sender, RoutedEventArgs e) => SendKey("1");

        private void btn2_Click(object sender, RoutedEventArgs e) => SendKey("2");
       
        private void btn3_Click(object sender, RoutedEventArgs e) => SendKey("3");
       
        private void btn4_Click(object sender, RoutedEventArgs e) => SendKey("4");
      
        private void btn5_Click(object sender, RoutedEventArgs e) => SendKey("5");

        private void btn6_Click(object sender, RoutedEventArgs e) => SendKey("6");

        private void btn7_Click(object sender, RoutedEventArgs e) => SendKey("7");

        private void btn8_Click(object sender, RoutedEventArgs e) => SendKey("8");

        private void btn9_Click(object sender, RoutedEventArgs e) => SendKey("9");

        private void btnEffacer_Click(object sender, RoutedEventArgs e) => SendKey("Effacer");

        private void btn0_Click(object sender, RoutedEventArgs e) => SendKey("0");

        private void btnCorriger_Click(object sender, RoutedEventArgs e) => SendKey("Corriger");

    }
}
