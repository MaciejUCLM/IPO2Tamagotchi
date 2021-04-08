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
        private string mName;
        private TimeSpan mScore;
        private TimeSpan mBestScore;
        private List<Achievement> mAchievements;

        public string Name { get => mName; set => mName = value; }
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
        public TimeSpan BestScore { get => mBestScore; set => mBestScore = value; }
        public List<Achievement> Achievements { get => mAchievements; }

        public PlayerData()
        {
            mName = "";
            mScore = TimeSpan.Zero;
            mBestScore = TimeSpan.Zero;
            mAchievements = new List<Achievement>();
        }

        public PlayerData(string name, TimeSpan bestScore) : this()
        {
            mName = name;
            mBestScore = bestScore;
        }
    }
}
