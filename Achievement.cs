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
        public delegate bool dIsObtained();

        private dIsObtained isObtained;
        private string mDescription;
        private string mSource;
        private int mLevel;

        public string Source { get => mSource; set => mSource = value; }
        public string Descritpion { get => mDescription; set => mDescription = value; }
        public int Level { get => mLevel; set => mLevel = value; }

        public Achievement()
        {
        }

        public Achievement(string src, string description, dIsObtained obtained)
        {
            isObtained = obtained;
            mDescription = description;
            mSource = src;
            mLevel = 0;
        }

        public Achievement Obtain()
        {
            if (isObtained.Invoke())
            {
                Level += 1;
                return this;
            }
            else
                return null;
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
