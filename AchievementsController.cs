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
                Add(new Achievement("media\\icons8-trophy.png", "Jugar {0} partidas").Obtain());
            };
            owner.EventCollecionable += (s, e) => {
                Add(new Achievement("media\\icons8-gift.png", "Conseguir {0} coleccionables").Obtain());
            };
            owner.EventBonusUsed += (s, e) => {
                Add(new Achievement("media\\icons8-medal.png", "Usar {0} premios").Obtain());
            };
            //new Achievement("media\\icons8-prize.png", "").Obtain()
        }

        public void Refresh()
        {
            mPanel.Children.Clear();
            foreach (Achievement a in mPlayer.Achievements)
                mPanel.Children.Add(a.GetImage());
        }

        public void Add(Achievement a)
        {
            if (a != null)
            {
                if (a.Level < 2)
                    mPlayer.Achievements.Add(a);
                mPanel.Children.Add(a.GetImage());
            }
        }
    }
}
