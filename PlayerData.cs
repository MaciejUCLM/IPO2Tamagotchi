using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    [Serializable]
    public class PlayerData
    {
        private int mGames;
        private string mName;
        private TimeSpan mScore;
        private TimeSpan mBestScore;
        private Achievement[] mAchievements = new Achievement[]
        {
            new Achievement("media\\icons8-trophy.png", "Jugar {0} partidas"),
            new Achievement("media\\icons8-gift.png", "Conseguir {0} coleccionables"),
            new Achievement("media\\icons8-medal.png", "Usar {0} premios"),
            new Achievement("media\\icons8-prize.png", "Aguantar {0} minutos")
        };

        public int Games { get => mGames; set => mGames = value; }
        public string Name { get => mName; set => mName = value; }
        public long ScoreTicks { get => mScore.Ticks; set => mScore = TimeSpan.FromTicks(value); }
        public long BestScoreTicks { get => mBestScore.Ticks; set => mBestScore = TimeSpan.FromTicks(value); }
        public TimeSpan Score
        {
            get => mScore;
            set
            {
                mScore = value;
                if (mScore > mBestScore)
                    mBestScore = mScore;
            }
        }
        public TimeSpan BestScore { get => mBestScore; }
        public Achievement[] Achievements { get => mAchievements; set => mAchievements = value; }

        public PlayerData()
        {
            mGames = 0;
            mName = "";
            mBestScore = TimeSpan.Zero;
            Score = TimeSpan.Zero;
        }

        public PlayerData(string name) : this()
        {
            mName = name;
        }
    }
}
