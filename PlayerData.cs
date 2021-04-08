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
        private long mScore;
        private long mBestScore;
        private List<Achievement> mAchievements;

        public int Games { get => mGames; set => mGames = value; }
        public string Name { get => mName; set => mName = value; }
        public long ScoreTicks { get => mScore; set => mScore = value; }
        public long BestScoreTicks { get => mBestScore; set => mBestScore = value; }
        public TimeSpan Score
        {
            get => TimeSpan.FromTicks(mScore);
            set
            {
                mScore = value.Ticks;
                if (mScore > mBestScore)
                    mBestScore = mScore;
            }
        }
        public TimeSpan BestScore { get => TimeSpan.FromTicks(mBestScore); set => mBestScore = value.Ticks; }
        public List<Achievement> Achievements { get => mAchievements; }

        public PlayerData()
        {
            mGames = 0;
            mName = "";
            Score = TimeSpan.Zero;
            BestScore = TimeSpan.Zero;
            mAchievements = new List<Achievement>();
        }

        public PlayerData(string name) : this()
        {
            mName = name;
        }
    }
}
