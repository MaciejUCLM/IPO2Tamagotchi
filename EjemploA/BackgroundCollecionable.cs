using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tamagotchi
{
    class BackgroundCollecionable : Collecionable
    {
        public static MouseButtonEventHandler clickHandler = null;

        public BackgroundCollecionable(string icon)
            : base(icon, "Haz click para activar", clickHandler)
        {
        }
    }
}
