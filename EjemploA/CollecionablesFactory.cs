using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EjemploA
{
    class CollecionablesFactory
    {
        private static ImageSource[] IMAGES;
        private StackPanel mPanel;

        public CollecionablesFactory(StackPanel target)
        {
            mPanel = target;
        }

        private Image createImageElement(ImageSource src)
        {
            Image elem = new Image();
            elem.Source = src;
            return elem;
        }

        private Image createImageElement(ImageSource src, MouseButtonEventHandler clickEvent)
        {
            Image elem = createImageElement(src);
            elem.MouseLeftButtonDown += clickEvent;
            return elem;
        }

        public void addRandomCollecionable()
        {
            mPanel.Children.Add(createImageElement());
        }
    }
}
