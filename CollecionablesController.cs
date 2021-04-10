using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Tamagotchi
{
    class CollecionablesController
    {
        private List<Collecionable> mItems;
        private Random mRnd;
        private Panel mPanel;

        public CollecionablesController(Panel target, Collecionable[] collecionables)
        {
            mRnd = new Random();
            mPanel = target;
            mItems = new List<Collecionable>();
            mItems.AddRange(collecionables);
            mPanel.Children.Clear();
        }

        public void Add(UIElement elem)
        {
            mPanel.Children.Add(elem);
        }

        public Collecionable PushRandomCollecionable()
        {
            if (mItems.Count > 0)
            {
                int i = mRnd.Next(0, mItems.Count);
                Collecionable c = mItems[i];
                mItems.RemoveAt(i);
                if (!c.OneShot)
                    mItems.Add(c.Copy());
                Add(c);
                return c;
            }
            return null;
        }
    }
}
