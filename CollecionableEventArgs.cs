using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    public class CollecionableEventArgs : EventArgs
    {
        public Collecionable Element { get; set; }
    }
}
