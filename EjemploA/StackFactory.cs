using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Tamagotchi
{
    class StackFactory
    {
        protected StackPanel mPanel;

        public StackFactory(StackPanel target)
        {
            mPanel = target;
        }

        public void Add(UIElement elem)
        {
            mPanel.Children.Add(elem);
        }
    }
}
