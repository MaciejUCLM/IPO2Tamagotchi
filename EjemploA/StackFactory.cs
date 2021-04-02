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

namespace EjemploA
{
    class StackFactory
    {
        protected StackPanel mPanel;

        public StackFactory(StackPanel target)
        {
            mPanel = target;
        }

        public void Add(UIElement elem)
        {
            mPanel.Children.Add(elem);
        }

        public Image CreateImageElement(string src)
        {
            Image elem = new Image();
            elem.Width = 80;
            elem.Stretch = Stretch.Uniform;
            elem.Source = GetImageFromUri(src);
            return elem;
        }

        public Image CreateImageElement(string src, MouseButtonEventHandler clickEvent)
        {
            Image elem = CreateImageElement(src);
            elem.MouseLeftButtonDown += clickEvent;
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
