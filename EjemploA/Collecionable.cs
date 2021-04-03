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
    abstract class Collecionable
    {
        protected string mTooltip;
        protected MouseButtonEventHandler mClick;
        protected Image mImage;

        public Image Icon { get => mImage; }
        public string Tooltip { get => mTooltip; }
        public MouseButtonEventHandler Click { get => mClick; }

        public Collecionable(string icon, string tooltip, MouseButtonEventHandler click)
        {
            mTooltip = tooltip;
            mClick = click;
            mImage = CreateImageElement(icon);
            if (Click != null)
                mImage.MouseLeftButtonDown += Click;
        }

        private Image CreateImageElement(string src)
        {
            Image elem = new Image();
            elem.Width = 50;
            elem.Stretch = Stretch.Uniform;
            elem.HorizontalAlignment = HorizontalAlignment.Center;
            elem.VerticalAlignment = VerticalAlignment.Center;
            elem.Source = GetImageFromUri(src);
            return elem;
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
