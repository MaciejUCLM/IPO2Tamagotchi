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
    /// Interaction logic for GameoverWindow.xaml
    /// </summary>
    public partial class GameoverWindow : Window
    {
        private MainWindow mOwner;

        public string TextScore { get => labelScore.Content as string; set => labelScore.Content = value; }
        public string TextName { get => labelName.Content as string; set => labelName.Content = value; }

        public GameoverWindow(MainWindow owner)
        {
            InitializeComponent();
            mOwner = owner;
        }

        private void Win_Closed(object sender, EventArgs e)
        {
            mOwner.Close();
        }
    }
}
