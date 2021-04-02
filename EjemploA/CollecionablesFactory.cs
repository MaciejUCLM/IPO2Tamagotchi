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

        public CollecionablesFactory(StackPanel target) : base(target)
        {
            mRnd = new Random();
        }

        public void AddRandomCollecionable()
        {
            double r = mRnd.NextDouble();
            if (r < 0.5)
            {
                Image draggable = CreateImageElement(DRAGGABLES[mRnd.Next(0, DRAGGABLES.Length)]);
                Add(draggable);
            }
            else
            {
                Image background = CreateImageElement(DRAGGABLES[mRnd.Next(0, DRAGGABLES.Length)]);
                Add(background);
            }
        }
    }
}
