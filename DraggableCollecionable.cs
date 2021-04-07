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
        public DraggableCollecionable(string icon, MouseButtonEventHandler clickHandler)
            : base(icon, "Arrestra a Tamagotchi para activar", clickHandler)
        {
        }

        public override Collecionable Copy()
        {
            return new DraggableCollecionable(mPath, mHandler);
        }
    }
}
