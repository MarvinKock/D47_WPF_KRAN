using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace D47_WPF_Kran
{
    public class Kiste
    {
        private Rectangle kiste;
        public double xKoordinate;
        public double yKoordinate;
        private bool seitlicheKiste;
        private bool angehoben;

        public Kiste(double x, double y, Rectangle kiste, bool seitDarstellung)
        {
            this.angehoben = false;
            this.seitlicheKiste = seitDarstellung;

            this.kiste = kiste;
            this.xKoordinate = x;
            this.yKoordinate = y;
            this.kiste.Fill = Brushes.Brown;

            if (seitDarstellung == false)
            {
                this.kiste.Width = kiste.Height = 39.0;
            }
            else
            {
                this.kiste.Width = 39.0;
                this.kiste.Height = 13.0;
            }
            
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

        public double getXfromPosition(int pos)
        {
            double toX;

            if (pos == 1)
            {
                toX = 183.0;
            }
            else if(pos == 2)
            {
                toX = 228.0;
            }
            else if(pos == 3)
            {
                toX = 293.0;
            }
            else
            {
                toX = 358.0;
            }

            //Console.WriteLine("x-Position: {0}", toX);

            return toX;
        }

        public double getYfromPosition(int pos)
        {
            double toY;

            if (pos == 1)
            {
                toY = 123.0;
            }
            else if (pos == 2)
            {
                toY = 168.0;
            }
            else if (pos == 3)
            {
                toY = 168.0;
            }
            else
            {
                toY = 168.0;
            }

            //Console.WriteLine("y-Position: {0}", toY);

            return toY;
        }

        public void kisteAufnhemen()
        {
            this.angehoben = true;
        }

        public bool testKisteAngehoben()
        {
            if (this.angehoben == false)
                return false;
            else
                return true;
        }
    }
}
