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
        private List<Achievement> mItems;
        private Panel mPanel;
        private PlayerData mPlayer;

        public AchievementsController(Panel panel, PlayerData player, Achievement[] achievements)
        {
            mPanel = panel;
            mPlayer = player;
            mItems = new List<Achievement>();
            mItems.AddRange(achievements);
        }

        public void Refresh()
        {
            mPanel.Children.Clear();
            foreach (Achievement a in mPlayer.Achievements)
                mPanel.Children.Add(a.GetImage());
        }

        public void Add(Achievement a)
        {
            mPanel.Children.Add(a.GetImage());
        }
    }
}
