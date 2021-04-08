using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Tamagotchi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random mRnd;
        private DispatcherTimer timer;
        private DispatcherTimer freezer;

        private double mStep = 1;
        private double mEnergyCoef = 1;
        private double mDiversificationCoef = 1;
        private double mFoodCoef = 1;

        private CollecionablesController mCollecionables;
        private CollecionablesController mBonuses;

        private string mName;
        private bool isPlaying = true;

        public void SetName(string name) => mName = name;

        public MainWindow()
        {
            InitializeComponent();
            mRnd = new Random();
            freezer = new DispatcherTimer();
            freezer.Interval = TimeSpan.FromSeconds(15);
            freezer.Tick += Freezer_Tick;

            Collecionable[] collecionables = new Collecionable[] {
                new BackgroundCollecionable("media\\gdansk.jpg", ChangeBackground),
                new BackgroundCollecionable("media\\hamburg.jpg", ChangeBackground),
                new BackgroundCollecionable("media\\istanbul.jpg", ChangeBackground),
                new BackgroundCollecionable("media\\petersburg.jpg", ChangeBackground),
                new DraggableCollecionable("media\\c1.png", DragCollecionable_Down),
                new DraggableCollecionable("media\\c2.png", DragCollecionable_Down),
                new DraggableCollecionable("media\\c3.png", DragCollecionable_Down)
            };
            Collecionable[] bonuses = {
                new ItemCollecionable("media\\icons8-double-right.png",
                                ItemCollecionable.TYPES.REFILL,
                                Item_Click),
                new ItemCollecionable("media\\icons8-question-mark.png",
                                ItemCollecionable.TYPES.RESTORE_DIVERSION,
                                Item_Click),
                new ItemCollecionable("media\\icons8-question-mark.png",
                                ItemCollecionable.TYPES.RESTORE_ENERGY,
                                Item_Click),
                new ItemCollecionable("media\\icons8-question-mark.png",
                                ItemCollecionable.TYPES.RESTORE_FOOD,
                                Item_Click),
                new ItemCollecionable("media\\icons8-skip-15-seconds-back.png",
                                ItemCollecionable.TYPES.FREEZE_DIVERSION,
                                Item_Click),
                new ItemCollecionable("media\\icons8-skip-15-seconds-back.png",
                                ItemCollecionable.TYPES.FREEZE_ENERGY,
                                Item_Click),
                new ItemCollecionable("media\\icons8-skip-15-seconds-back.png",
                                ItemCollecionable.TYPES.FREEZE_FOOD,
                                Item_Click)
            };
            mCollecionables = new CollecionablesController(StackCollecionables, collecionables);
            mBonuses = new CollecionablesController(StackRewards, bonuses);

            WelcomeWindow window = new WelcomeWindow(this);
            Visibility = Visibility.Hidden;
            window.Closed += StartGame;
            window.Show();
        }

        private void StartGame(object sender, EventArgs e)
        {
            if (mName == "")
                Close();
            else
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(1000);
                timer.Tick += Timer_Tick;
                timer.Start();
                MsgBlock.Text = "Bienvenido " + mName;
                ButtonsEnabled(true);
            }
        }

        private void GameOver()
        {
            timer.Stop();
            MsgBlock.Text = "GAMEOVER";
            isPlaying = false;
            ButtonsEnabled(false);

            GameoverWindow window = new GameoverWindow(this);
            window.TextName = mName;
            window.TextScore = "";
            window.Show();
        }

        private void ChangeBackground(object sender, MouseButtonEventArgs e)
        {
            Image image = ((Image)sender);
            ImageSource source = BackgroundImage.Source;
            BackgroundImage.Source = image.Source;
            image.Source = source;
        }

        private void BeginStoryboard(string name)
        {
            Storyboard anim = (Storyboard)Resources[name];
            ButtonsEnabled(false);
            anim.Completed += (object sender, EventArgs e) => ButtonsEnabled(true);
            anim.Begin();
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
            else if (mRnd.NextDouble() < 0.02)
                mBonuses.PushRandomCollecionable();
        }

        private void Freezer_Tick(object sender, EventArgs e)
        {
            freezer.Stop();
            mDiversificationCoef = 1;
            mEnergyCoef = 1;
            mFoodCoef = 1;
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
                bar.Value = Math.Min(100, bar.Value + mRnd.Next(9,15));

            mStep = Math.Min(20, mStep + 0.1);

            if (mRnd.NextDouble() < 0.2)
                mCollecionables.PushRandomCollecionable();
        }

        private void About_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Programa realizado por MN GBStudio.\nDesea salir?",
                "Acerca de Tamagotchi", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
                this.Close();
        }

        private void Item_Click(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ItemCollecionable;
            StackRewards.Children.Remove(item);
            switch (item.Type)
            {
                case ItemCollecionable.TYPES.FREEZE_DIVERSION:
                    mDiversificationCoef = 0;
                    freezer.Start();
                    break;
                case ItemCollecionable.TYPES.FREEZE_ENERGY:
                    mEnergyCoef = 0;
                    freezer.Start();
                    break;
                case ItemCollecionable.TYPES.FREEZE_FOOD:
                    mFoodCoef = 0;
                    freezer.Start();
                    break;
                case ItemCollecionable.TYPES.REFILL:
                    EnergyBar.Value = 100;
                    DiversificationBar.Value = 100;
                    FoodBar.Value = 100;
                    break;
                case ItemCollecionable.TYPES.RESTORE_DIVERSION:
                    DiversificationBar.Value = 100;
                    break;
                case ItemCollecionable.TYPES.RESTORE_ENERGY:
                    EnergyBar.Value = 100;
                    break;
                case ItemCollecionable.TYPES.RESTORE_FOOD:
                    FoodBar.Value = 100;
                    break;
            }
        }

        private void Hat_Click(object sender, MouseButtonEventArgs e)
        {
            imgHat.Visibility = Visibility.Hidden;
        }

        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            Image img = (Image)e.Data.GetData(typeof(DraggableCollecionable));
            imgHat.Source = img.Source;
            imgHat.Visibility = Visibility.Visible;
        }

        private void DragCollecionable_Down(object sender, MouseButtonEventArgs e)
        {
            DataObject data = new DataObject(sender as Image);
            DragDrop.DoDragDrop((Image)sender, data, DragDropEffects.Move);
        }
    }
}
