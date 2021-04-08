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
        private List<Achievement> mAchievements;

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
        public List<Achievement> Achievements { get => mAchievements; }

        public PlayerData()
        {
            mGames = 0;
            mName = "";
            mBestScore = TimeSpan.Zero;
            Score = TimeSpan.Zero;
            mAchievements = new List<Achievement>();
        }

        public PlayerData(string name) : this()
        {
            mName = name;
        }
    }
}
