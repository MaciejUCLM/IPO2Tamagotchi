using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EjemploA
{
    class CollecionablesFactory : StackFactory
    {
        private static string[] DRAGGABLES = {
            "media\\c1.png", "media\\c2.png", "media\\c3.png"
        };
        private static string[] BACKGROUNDS = {
            "media\\gdansk.jpg", "media\\hamburg.jpg", "media\\istanbul.jpg", "media\\petersburg.jpg"
        };

        private Random mRnd;
        private MouseButtonEventHandler mHandlerDrag, mHandlerClick;

        public CollecionablesFactory(StackPanel target, MouseButtonEventHandler drag, MouseButtonEventHandler click) : base(target)
        {
            mRnd = new Random();
            mHandlerDrag = drag;
            mHandlerClick = click;
        }

        public void PushRandomCollecionable()
        {
            double r = mRnd.NextDouble();
            string c;
            Image i;
            if (r < 0.5)
            {
                c = DRAGGABLES[mRnd.Next(0, DRAGGABLES.Length)];
                i = CreateImageElement(c, mHandlerDrag);
                i.ToolTip = "Arrasta este elemento a Tamagotchi";
                Add(i);
            }
            else
            {
                c = BACKGROUNDS[mRnd.Next(0, BACKGROUNDS.Length)];
                i = CreateImageElement(c, mHandlerClick);
                i.ToolTip = "Haz click para activar";
                Add(i);
            }
        }
    }
}
