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
    class CollecionablesFactory
    {
        private List<Collecionable> mItems;
        private Random mRnd;
        private WrapPanel mPanel;

        public CollecionablesFactory(WrapPanel target)
        {
            mRnd = new Random();
            mPanel = target;
            mItems = new List<Collecionable>();
            Init();
        }

        private void Init()
        {
            mItems.AddRange(new Collecionable[] {
                new BackgroundCollecionable("media\\gdansk.jpg"),
                new BackgroundCollecionable("media\\hamburg.jpg"),
                new BackgroundCollecionable("media\\istanbul.jpg"),
                new BackgroundCollecionable("media\\petersburg.jpg"),
                new DraggableCollecionable("media\\c1.png"),
                new DraggableCollecionable("media\\c2.png"),
                new DraggableCollecionable("media\\c3.png"),
                new ItemCollecionable("media\\i1.png", ItemCollecionable.TYPES.REFILL),
                new ItemCollecionable("media\\i2.png", ItemCollecionable.TYPES.RESTORE_DIVERSION),
                new ItemCollecionable("media\\i2.png", ItemCollecionable.TYPES.RESTORE_ENERGY),
                new ItemCollecionable("media\\i2.png", ItemCollecionable.TYPES.RESTORE_FOOD),
                new ItemCollecionable("media\\i3.png", ItemCollecionable.TYPES.FREEZE_DIVERSION),
                new ItemCollecionable("media\\i3.png", ItemCollecionable.TYPES.FREEZE_ENERGY),
                new ItemCollecionable("media\\i3.png", ItemCollecionable.TYPES.FREEZE_FOOD)
            });
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
                Add(c.Icon);
            }
        }
    }
}
