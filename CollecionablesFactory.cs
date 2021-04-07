﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Tamagotchi
{
    class CollecionablesFactory
    {
        private List<Collecionable> mItems;
        private Random mRnd;
        private WrapPanel mPanel;

        public CollecionablesFactory(WrapPanel target, Collecionable[] collecionables)
        {
            mRnd = new Random();
            mPanel = target;
            mItems = new List<Collecionable>();
            mItems.AddRange(collecionables);
        }

        public void Add(UIElement elem)
        {
            mPanel.Children.Add(elem);
        }

        public void PushRandomCollecionable()
        {
            if (mItems.Count > 0)
            {
                int i = mRnd.Next(0, mItems.Count);
                Collecionable c = mItems[i];
                mItems.RemoveAt(i);
                if (!c.OneShot)
                    mItems.Add(c.Copy());
                Add(c);
            }
        }
    }
}
