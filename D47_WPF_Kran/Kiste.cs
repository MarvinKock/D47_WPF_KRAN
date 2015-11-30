using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace D47_WPF_Kran
{
    class Kiste
    {
        private Rectangle kiste;
        private double xKoordinate;
        private double yKoordinate;

        public Kiste(double x, double y, Rectangle kiste)
        {
            this.kiste = kiste;
            this.xKoordinate = x;
            this.yKoordinate = y;
            this.kiste.Fill = Brushes.Brown;
            this.kiste.Width = kiste.Height = 37.0;
            this.kiste.SetValue(Canvas.LeftProperty, x);
            this.kiste.SetValue(Canvas.TopProperty, y);
        }

        public void bewegungXrichtungPositiv()
        {
            this.xKoordinate++;
            this.kiste.SetValue(Canvas.LeftProperty, this.xKoordinate);
        }

        public void bewegungXrichtungNegativ()
        {
            this.xKoordinate--;
            this.kiste.SetValue(Canvas.LeftProperty, this.xKoordinate);
        }

        public void bewegungYrichtungPositiv()
        {
            this.yKoordinate++;
            this.kiste.SetValue(Canvas.TopProperty, this.yKoordinate);
        }

        public void bewegungYrichtungNegativ()
        {
            this.yKoordinate--;
            this.kiste.SetValue(Canvas.TopProperty, this.yKoordinate);
        }
    }
}
