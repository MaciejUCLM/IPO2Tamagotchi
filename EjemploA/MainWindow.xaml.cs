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
        private string mName;
        private bool isPlaying = true;

        public MainWindow()
        {
            InitializeComponent();
            welcomeDialog();
        }

        public void setName(string name) => mName = name;

        private void Timer_Tick(object sender, EventArgs e)
        {
            EnergyBar.Value -= mStep;
            DiversificationBar.Value -= mStep;
            FoodBar.Value -= mStep;

            if (EnergyBar.Value <= 0 || DiversificationBar.Value <= 0 || FoodBar.Value <= 0)
            {
                MsgBlock.Text = "GAME OVER";
                isPlaying = false;
                buttonsEnabled(false);
            }
        }

        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            ProgressBar bar = null;

            if (sender == DescansarBtn)
            {
                bar = EnergyBar;
                animDescansar();
            }
            else if (sender == AlimentarBtn)
            {
                bar = FoodBar;
                animEat();
            }
            else if (sender == JugarBtn)
            {
                bar = DiversificationBar;
                animPlay();
            }

            if (bar != null)
                bar.Value = Math.Min(100, bar.Value + 10);

            mStep = Math.Min(20, mStep + 0.1);
        }

        private void change_bg(object sender, MouseButtonEventArgs e)
        {
            BackgroundImage.Source = ((Image)sender).Source;
        }

        private DoubleAnimation closeEyeFactory(bool onComplete = false)
        {
            DoubleAnimation closeEye = new DoubleAnimation();
            closeEye.From = 15;
            closeEye.To = 4;
            closeEye.Duration = new Duration(TimeSpan.FromMilliseconds(700));
            closeEye.AutoReverse = true;
            if (onComplete)
                closeEye.Completed += CloseEye_Completed;
            return closeEye;
        }

        private void CloseEye_Completed(object sender, EventArgs e)
        {
            buttonsEnabled(true);
        }

        private void animDescansar()
        {
            buttonsEnabled(false);
            l_eye.BeginAnimation(Ellipse.HeightProperty, closeEyeFactory(true));
            r_eye.BeginAnimation(Ellipse.HeightProperty, closeEyeFactory());
        }

        private void animEat()
        {
            Storyboard anim = (Storyboard)Resources["eating"];
            buttonsEnabled(false);
            anim.Completed += Eat_Completed;
            anim.Begin();
        }

        private void animPlay()
        {
            Storyboard anim = (Storyboard)Resources["playing"];
            buttonsEnabled(false);
            anim.Completed += Eat_Completed;
            anim.Begin();
        }

        private void Eat_Completed(object sender, EventArgs e)
        {
            buttonsEnabled(true);
        }

        private void about_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Programa realizado por MN GBStudio.\nDesea salir?",
                "Acerca de Tamagotchi", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
                this.Close();
        }

        private void startGame(object sender, EventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += Timer_Tick;
            timer.Start();
            MsgBlock.Text = "Bienvenido " + mName;
            buttonsEnabled(true);
        }

        private void welcomeDialog()
        {
            WelcomeWindow window = new WelcomeWindow(this);
            window.Closed += startGame;
            window.Show();
        }

        private void buttonsEnabled(bool enabled)
        {
            if (!isPlaying)
                enabled = false;
            foreach (Button x in new Button[] { DescansarBtn, AlimentarBtn, JugarBtn })
                x.IsEnabled = enabled;
        }

        private void dragHatStart(object sender, MouseButtonEventArgs e)
        {
            DataObject data = new DataObject((Image)sender);
            DragDrop.DoDragDrop((Image)sender, data, DragDropEffects.Move);
        }
    }
}
