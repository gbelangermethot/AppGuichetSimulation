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
    public partial class ListBtn : UserControl
    {
        public ListBtn()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty BtnTextProperty =
        DependencyProperty.Register("BtnText", typeof(string), typeof(ListBtn),
        new PropertyMetadata(string.Empty));

        private string btnText
        {
            get => (string)GetValue(BtnTextProperty);
            set => SetValue(BtnTextProperty, value);
        }
        public string BtnText
        {
            get { return btnText; }
            set
            {
                btnText = value;
                customTb.Text = btnText;
            }
        }


        private double btnHeight;
        public double BtnHeight
        {
            get { return btnHeight; }
            set
            {
                btnHeight = value;
            }
        }
    }
}