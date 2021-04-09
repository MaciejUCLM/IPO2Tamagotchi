using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Tamagotchi
{
    class AchievementsController
    {
        private int gameMinutes = 0;

        private Panel mPanel;
        private PlayerData mPlayer;

        public AchievementsController(Panel panel, PlayerData player)
        {
            mPanel = panel;
            mPlayer = player;
        }

        public void Setup(MainWindow owner)
        {
            owner.EventStart += (s, e) => {
                Add(mPlayer.Achievements[0].Obtain());
            };
            owner.EventCollecionable += (s, e) => {
                Add(mPlayer.Achievements[1].Obtain());
            };
            owner.EventBonusUsed += (s, e) => {
                Add(mPlayer.Achievements[2].Obtain());
            };
            owner.EventTick += (s, e) => {
                if (mPlayer.Score.Minutes > gameMinutes)
                {
                    gameMinutes = mPlayer.Score.Minutes;
                    Add(mPlayer.Achievements[3].Obtain());
                }
            };
        }

        public void Refresh()
        {
            mPanel.Children.Clear();
            foreach (Achievement a in mPlayer.Achievements.Where(x => x.Level > 0))
                mPanel.Children.Add(a.GetImage());
        }

        public void Add(Achievement a)
        {
            if (a != null)
                mPanel.Children.Add(a.GetImage());
        }
    }
}
