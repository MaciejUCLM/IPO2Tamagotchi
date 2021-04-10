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
        private static double DROP_CHANCE = 0.1;
        private static double BONUS_CHANCE = 0.03;

        private Random rnd;
        private DispatcherTimer timer;
        private DispatcherTimer freezer;
        private Storyboard idleAnimation;

        private double step;
        private double mEnergyCoef;
        private double mDiversificationCoef;
        private double mFoodCoef;

        private AchievementsController achievements;
        private CommentsFactory comments;
        private CollecionablesController collecionables;
        private CollecionablesController bonuses;

        private List<PlayerData> database;
        private PlayerData mPlayer;
        private bool isPlaying = true;

        public event EventHandler EventTick;
        public event EventHandler EventStart;
        public event EventHandler EventGameover;
        public event EventHandler EventCollecionable;
        public event EventHandler EventCollecionableUsed;
        public event EventHandler EventBonus;
        public event EventHandler EventBonusUsed;

        public double Energy { get => EnergyBar.Value; set => EnergyBar.Value = value; }
        public double Diversion { get => DiversificationBar.Value; set => DiversificationBar.Value = value; }
        public double Food { get => FoodBar.Value; set => FoodBar.Value = value; }

        public MainWindow()
        {
            InitializeComponent();
            rnd = new Random();
            idleAnimation = (Storyboard)Resources["Idle"];
            foreach (string s in new string[] { "eating", "playing", "sleeping" })
            {
                Storyboard anim = (Storyboard)Resources[s];
                anim.Completed += (object sender, EventArgs e) => { ButtonsEnabled(true); idleAnimation.Begin(); };
            }
            Initialize();
        }

        public void Initialize()
        {
            step = 1;
            mEnergyCoef = 1;
            mDiversificationCoef = 1;
            mFoodCoef = 1;
            Energy = 100;
            Diversion = 100;
            Food = 100;

            freezer = new DispatcherTimer();
            freezer.Interval = TimeSpan.FromSeconds(15);
            freezer.Tick += Freezer_Tick;

            collecionables = new CollecionablesController(StackCollecionables, new Collecionable[] {
                new BackgroundCollecionable("media\\gdansk.jpg", ChangeBackground),
                new BackgroundCollecionable("media\\hamburg.jpg", ChangeBackground),
                new BackgroundCollecionable("media\\istanbul.jpg", ChangeBackground),
                new BackgroundCollecionable("media\\petersburg.jpg", ChangeBackground),
                new DraggableCollecionable("media\\c1.png", DragCollecionable_Down),
                new DraggableCollecionable("media\\c2.png", DragCollecionable_Down),
                new DraggableCollecionable("media\\c3.png", DragCollecionable_Down)
            });
            bonuses = new CollecionablesController(StackRewards, new Collecionable[] {
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
            });
            comments = new CommentsFactory();

            WelcomeWindow window = new WelcomeWindow(this);
            Visibility = Visibility.Hidden;
            window.Closed += StartGame;
            window.Show();
        }

        public void SetName(string name)
        {
            var persistence = Persistence.GetInstance();
            if (persistence.Exists())
                database = persistence.Load<List<PlayerData>>();
            else
                database = new List<PlayerData>();
            database.RemoveAll(x => x.Name == "");
            RankingFactory.PopulateRanking(StackRanking, database);

            mPlayer = database.Find(x => x.Name == name);
            if (mPlayer == null)
            {
                mPlayer = new PlayerData(name);
                database.Add(mPlayer);
            }

            achievements = new AchievementsController(StackAchievements, mPlayer);
            achievements.Setup(this);
            achievements.Refresh();
        }

        private void StartGame(object sender, EventArgs e)
        {
            if (mPlayer.Name == "")
                Close();
            else
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(1000);
                timer.Tick += Timer_Tick;
                timer.Start();
                MsgBlock.Text = "Bienvenido " + mPlayer.Name;
                isPlaying = true;
                ButtonsEnabled(true);
                EventStart?.Invoke(this, null);
                idleAnimation.Begin();
            }
        }

        private void GameOver()
        {
            freezer.Stop();
            timer.Stop();
            MsgBlock.Text = "GAMEOVER";
            isPlaying = false;
            ButtonsEnabled(false);
            Persistence.GetInstance().Save(database);
            EventGameover?.Invoke(this, null);

            GameoverWindow window = new GameoverWindow(this);
            window.TextName = mPlayer.Name;
            window.TextScore = mPlayer.Score.ToString();
            window.Show();
        }

        private void BeginStoryboard(string name)
        {
            Storyboard anim = (Storyboard)Resources[name];
            ButtonsEnabled(false);
            idleAnimation.Stop();
            anim.Begin();
        }

        private void ButtonsEnabled(bool enabled)
        {
            if (!isPlaying)
                enabled = false;
            DescansarBtn.IsEnabled = enabled;
            AlimentarBtn.IsEnabled = enabled;
            JugarBtn.IsEnabled = enabled;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Energy -= step * mEnergyCoef;
            Diversion -= step * mDiversificationCoef;
            Food -= step * mFoodCoef;

            mPlayer.Score += timer.Interval;

            EventTick?.Invoke(sender, e);
            if (Energy <= 0 || Diversion <= 0 || Food <= 0)
                GameOver();
            else if (rnd.NextDouble() < BONUS_CHANCE)
            {
                MsgBlock.Text = comments.GetComment(CommentsFactory.TYPES.BONUS);
                var c = bonuses.PushRandomCollecionable();
                EventBonus?.Invoke(c, null);
            }
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
                MsgBlock.Text = comments.GetComment(CommentsFactory.TYPES.SLEEPING);
                BeginStoryboard("sleeping");
            }
            else if (sender == AlimentarBtn)
            {
                bar = FoodBar;
                MsgBlock.Text = comments.GetComment(CommentsFactory.TYPES.EATING);
                BeginStoryboard("eating");
            }
            else if (sender == JugarBtn)
            {
                bar = DiversificationBar;
                MsgBlock.Text = comments.GetComment(CommentsFactory.TYPES.PLAYING);
                BeginStoryboard("playing");
            }

            if (bar != null)
                bar.Value = Math.Min(100, bar.Value + rnd.Next(9,15));

            step = Math.Min(20, step + 0.1);

            if (rnd.NextDouble() < DROP_CHANCE)
            {
                MsgBlock.Text = comments.GetComment(CommentsFactory.TYPES.COLLECIONABLE);
                var c = collecionables.PushRandomCollecionable();
                EventCollecionable?.Invoke(c, null);
            }
        }

        private void About_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Programa realizado por Maciej Nalepa.\nDesea salir?",
                "Acerca de Tamagotchi", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
                this.Close();
        }

        private void ChangeBackground(object sender)
        {
            Image image = (Image)sender;
            ImageSource source = BackgroundImage.Source;
            BackgroundImage.Source = image.Source;
            image.Source = source;
            EventCollecionableUsed?.Invoke(sender, null);
        }

        private void Item_Click(object sender)
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
                    Energy = 100;
                    Diversion = 100;
                    Food = 100;
                    break;
                case ItemCollecionable.TYPES.RESTORE_DIVERSION:
                    Diversion = 100;
                    break;
                case ItemCollecionable.TYPES.RESTORE_ENERGY:
                    Energy = 100;
                    break;
                case ItemCollecionable.TYPES.RESTORE_FOOD:
                    Food = 100;
                    break;
            }
            EventBonusUsed?.Invoke(sender, null);
        }

        private void DragCollecionable_Down(object sender)
        {
            DataObject data = new DataObject(sender as Image);
            DragDrop.DoDragDrop((Image)sender, data, DragDropEffects.Move);
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
            EventCollecionableUsed?.Invoke(img, null);
        }

        private void Win_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Persistence.GetInstance().Save(database);
        }
    }
}
