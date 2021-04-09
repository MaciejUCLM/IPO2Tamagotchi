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
        protected string mPath;
        protected Action<object> mAction;

        private bool mOneShot = true;

        public bool OneShot { get => mOneShot; set { mOneShot = value; } }

        public Collecionable(string icon, string tooltip, Action<object> click, bool oneshot)
            : this(icon, tooltip, click)
        {
            mOneShot = oneshot;
        }

        public Collecionable(string icon, string tooltip, Action<object> click)
        {
            mAction = click;
            mPath = icon;
            InitializeImage(icon, tooltip);
            this.MouseLeftButtonDown += Collecionable_MouseLeftButtonDown;
        }

        private void Collecionable_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mAction != null)
                mAction.Invoke(sender);
        }

        public abstract Collecionable Copy();

        private void InitializeImage(string src, string tooltip)
        {
            this.Stretch = Stretch.Uniform;
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.VerticalAlignment = VerticalAlignment.Stretch;
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
