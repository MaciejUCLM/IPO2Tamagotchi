using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tamagotchi
{
    abstract class Collecionable : Image
    {
        public Collecionable(string icon, string tooltip, MouseButtonEventHandler click)
        {
            InitializeImage(icon, tooltip);
            if (click != null)
                this.MouseLeftButtonDown += click;
        }

        private void InitializeImage(string src, string tooltip)
        {
            this.Width = 50;
            this.Stretch = Stretch.Uniform;
            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.VerticalAlignment = VerticalAlignment.Center;
            this.Source = GetImageFromUri(src);
            this.ToolTip = tooltip;
        }

        private ImageSource GetImageFromUri(string uri)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(uri, UriKind.Relative);
            img.EndInit();
            return img;
        }
    }
}
