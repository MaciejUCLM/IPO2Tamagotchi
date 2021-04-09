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
        public BackgroundCollecionable(string icon, Action<object> clickHandler)
            : base(icon, "Haz click para activar", clickHandler)
        {
        }

        public override Collecionable Copy()
        {
            return new BackgroundCollecionable(mPath, mAction);
        }
    }
}
