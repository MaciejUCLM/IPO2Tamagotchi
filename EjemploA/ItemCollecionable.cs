using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tamagotchi
{
    class ItemCollecionable : Collecionable
    {
        public static MouseButtonEventHandler clickHandler = null;

        public ItemCollecionable(string icon)
            : base(icon, "Haz click para activar", clickHandler)
        {
            mImage.MouseLeftButtonDown += Image_Click;
        }

        private void Image_Click(object sender, MouseButtonEventArgs e)
        {
            // effect
        }
    }
}
