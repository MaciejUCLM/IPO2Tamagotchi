using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tamagotchi
{
    [Serializable]
    public class Achievement
    {
        private string mDescription;
        private string mSource;

        public string Source { get => mSource; set => mSource = value; }
        public string Descritpion { get => mDescription; set => mDescription = value; }

        public Achievement()
        {
        }

        public Achievement(string src, string description)
        {
            mDescription = description;
            mSource = src;
        }

        public Image GetImage()
        {
            Image img = new Image();
            img.Stretch = Stretch.Uniform;
            img.HorizontalAlignment = HorizontalAlignment.Center;
            img.VerticalAlignment = VerticalAlignment.Center;
            img.Source = GetImageFromUri(Source);
            img.ToolTip = Descritpion;
            return img;
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
