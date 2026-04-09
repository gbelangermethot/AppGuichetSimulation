using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Projet_Guichet.View.UserControls
{
    public partial class CustomBtn : UserControl
    {
        public CustomBtn()
        {
            InitializeComponent();
            this.IsEnabledChanged += CustomBtn_IsEnabledChanged;
        }

        private void CustomBtn_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue) // If IsEnabled is False
            {
                MainBorder.Opacity = 0.4; // Make it pale
                this.Cursor = Cursors.No; // Optional: change cursor
            }
            else
            {
                MainBorder.Opacity = 1.0;
                this.Cursor = Cursors.Hand;
            }
        }



        // Propriété pour le texte (comme une prop en React/Vue)
        public static readonly DependencyProperty BtnTextProperty =
            DependencyProperty.Register("BtnText", typeof(string), typeof(CustomBtn), new PropertyMetadata("Bouton"));

        public string BtnText
        {
            get => (string)GetValue(BtnTextProperty);
            set => SetValue(BtnTextProperty, value);
        }

        public static readonly DependencyProperty BtnFontsizeProperty =
            DependencyProperty.Register("BtnFontsize", typeof(double), typeof(CustomBtn), new PropertyMetadata(16.0));

        public double BtnFontsize
        {
            // Utilise bien 'BtnFontsizeProperty' ici !
            get => (double)GetValue(BtnFontsizeProperty);
            set => SetValue(BtnFontsizeProperty, value);
        }

        public static readonly DependencyProperty BtnBackgroundProperty =
            DependencyProperty.Register("BtnBackground", typeof(Brush), typeof(CustomBtn),
            new PropertyMetadata(new SolidColorBrush(Colors.LightCoral))); // Valeur par défaut

        public Brush BtnBackground
        {
            get => (Brush)GetValue(BtnBackgroundProperty);
            set => SetValue(BtnBackgroundProperty, value);
        }

        public static readonly DependencyProperty BtnForegroundProperty =
            DependencyProperty.Register("BtnForeground", typeof(Brush), typeof(CustomBtn),
            new PropertyMetadata(new SolidColorBrush(Colors.White))); // Valeur par défaut

        public Brush BtnForeground
        {
            get => (Brush)GetValue(BtnForegroundProperty);
            set => SetValue(BtnForegroundProperty, value);
        }

        // L'événement que les pages ecoutent
        public event RoutedEventHandler Click;

        // Action quand on clique (On simule un bouton standard)
        private void MainBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.IsEnabled && e.LeftButton == MouseButtonState.Pressed)
            {
                Click?.Invoke(this, new RoutedEventArgs());
            }
        }

        
        private void MainBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.IsEnabled) MainBorder.Opacity = 0.8;
        }
        private void MainBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.IsEnabled) MainBorder.Opacity = 1.0;
            else MainBorder.Opacity = 0.4; // Stay pale if disabled
        }
    }
}
