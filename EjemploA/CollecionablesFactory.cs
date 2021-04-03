using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Tamagotchi
{
    class CollecionablesFactory : StackFactory
    {
        private static string[] DRAGGABLES = {
            "media\\c1.png", "media\\c2.png", "media\\c3.png"
        };
        private static string[] ITEMS = {
            "media\\i1.png", "media\\i2.png", "media\\i3.png"
        };
        private static string[] BACKGROUNDS = {
            "media\\gdansk.jpg", "media\\hamburg.jpg", "media\\istanbul.jpg", "media\\petersburg.jpg"
        };

        private List<Collecionable> mItems;
        private Random mRnd;

        public CollecionablesFactory(StackPanel target) : base(target)
        {
            mRnd = new Random();
        }

        public void PushRandomCollecionable()
        {
            Collecionable c;
            int i;
            Image im;

            if (mItems.Count > 0)
            {
                i = mRnd.Next(0, mItems.Count);
                c = mItems[i];
                mItems.RemoveAt(i);

                im = CreateImageElement(c.Icon, c.Click);
                im.ToolTip = c.Tooltip;
                Add(im);
            }
        }
    }
}
