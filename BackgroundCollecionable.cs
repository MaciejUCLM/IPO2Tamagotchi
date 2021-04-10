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
        private bool visited;

        public BackgroundCollecionable(string icon, Action<object> clickHandler)
            : base(icon, "Haz click para activar", clickHandler)
        {
            visited = false;
        }

        public bool Visit()
        {
            if (!visited)
            {
                visited = true;
                return true;
            }
            else
                return false;
        }

        public override Collecionable Copy()
        {
            return new BackgroundCollecionable(mPath, mAction);
        }
    }
}
