﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    [Serializable]
    class PlayerData
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

        public PlayerData(string name, TimeSpan bestScore)
        {
            mName = name;
            mScore = TimeSpan.Zero;
            mBestScore = bestScore;
            mAchievements = new List<Achievement>();
        }
    }
}
