using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace EjemploA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;

        private double mStep = 1;
        private double mEnergyCoef = 1;
        private double mDiversificationCoef = 1;
        private double mFoodCoef = 1;

        private string mName;
        private bool isPlaying = true;

        public void SetName(string name) => mName = name;

        public MainWindow()
        {
            InitializeComponent();
            WelcomeDialog();
        }

        private void GameOver()
        {
            MsgBlock.Text = "GAME OVER";
            isPlaying = false;
            ButtonsEnabled(false);
        }

        private void ChangeBackground(object sender, MouseButtonEventArgs e)
        {
            BackgroundImage.Source = ((Image)sender).Source;
        }

        private void BeginStoryboard(string name)
        {
            Storyboard anim = (Storyboard)Resources[name];
            ButtonsEnabled(false);
            anim.Completed += (object sender, EventArgs e) => ButtonsEnabled(true);
            anim.Begin();
        }

        private void About_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Programa realizado por MN GBStudio.\nDesea salir?",
                "Acerca de Tamagotchi", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
                this.Close();
        }

        private void DragCollecionableStart(object sender, MouseButtonEventArgs e)
        {
            DataObject data = new DataObject((Image)sender);
            DragDrop.DoDragDrop((Image)sender, data, DragDropEffects.Move);
        }

        private void StartGame(object sender, EventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += Timer_Tick;
            timer.Start();
            MsgBlock.Text = "Bienvenido " + mName;
            ButtonsEnabled(true);
        }

        private void WelcomeDialog()
        {
            WelcomeWindow window = new WelcomeWindow(this);
            Visibility = Visibility.Hidden;
            window.Closed += StartGame;
            window.Show();
        }

        private void ButtonsEnabled(bool enabled)
        {
            if (!isPlaying)
                enabled = false;
            foreach (Button x in new Button[] { DescansarBtn, AlimentarBtn, JugarBtn })
                x.IsEnabled = enabled;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            EnergyBar.Value -= mStep * mEnergyCoef;
            DiversificationBar.Value -= mStep * mDiversificationCoef;
            FoodBar.Value -= mStep * mFoodCoef;

            if (EnergyBar.Value <= 0 || DiversificationBar.Value <= 0 || FoodBar.Value <= 0)
                GameOver();
        }

        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            ProgressBar bar = null;

            if (sender == DescansarBtn)
            {
                bar = EnergyBar;
                BeginStoryboard("sleeping");
            }
            else if (sender == AlimentarBtn)
            {
                bar = FoodBar;
                BeginStoryboard("eating");
            }
            else if (sender == JugarBtn)
            {
                bar = DiversificationBar;
                BeginStoryboard("playing");
            }

            if (bar != null)
                bar.Value = Math.Min(100, bar.Value + 10);

            mStep = Math.Min(20, mStep + 0.1);
        }

        private void CanvasDropped(object sender, DragEventArgs e)
        {

        }

        private void Hat_Click(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
