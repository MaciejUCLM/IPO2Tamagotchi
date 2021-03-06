using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tamagotchi
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        private MainWindow mOwner;

        public WelcomeWindow(MainWindow owner)
        {
            InitializeComponent();
            mOwner = owner;
        }

        private void empezar_Click(object sender, RoutedEventArgs e)
        {
            if (nameBox.Text.Length > 0)
                Close();
        }

        private void Win_Closed(object sender, EventArgs e)
        {
            mOwner.SetName(nameBox.Text);
            mOwner.Visibility = Visibility.Visible;
        }

        private void Win_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                empezar_Click(sender, e);
        }
    }
}
