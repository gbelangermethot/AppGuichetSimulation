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
    /// <summary>
    /// Interaction logic for numericButton.xaml
    /// </summary>
    public partial class numericButton : UserControl
    {
        public numericButton()
        {
            InitializeComponent();
        }

        private string btnText;
        public string BtnText
        {
            get { return btnText; }
            set
            {
                btnText = value;
                customTb.Text = btnText;
            }
        }

        private double btnWidth;
        public double BtnWidth
        {
            get { return btnWidth; }
            set
            {
                btnWidth = value;
                customButton.Width = btnWidth + 5;
                customBorder.Width = btnWidth;
            }
        }

        private double btnHeight;
        public double BtnHeight
        {
            get { return btnHeight; }
            set
            {
                btnHeight = value;
                customButton.Height = btnHeight +5;
                customBorder.Height = btnHeight;
            }
        }
    }
}
