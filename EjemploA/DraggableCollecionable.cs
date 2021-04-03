using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tamagotchi
{
    class DraggableCollecionable : Collecionable
    {
        public static MouseButtonEventHandler clickHandler = null;

        public DraggableCollecionable(string icon)
            : base(icon, "Arrestra a Tamagotchi para activar", clickHandler)
        {
        }
    }
}
