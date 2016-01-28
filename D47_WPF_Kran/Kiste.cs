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
    public delegate void moveKistePositionHandler(double x, double y);
    public delegate void erstelleKisteHandler();
    public class Kiste
    {
        double xKoordinate;
        double yKoordinate;
        
        private Rectangle kiste;

        private bool seitlicheKiste;
        private bool angehoben;
        private Canvas oberflaeche;

        private double zKisteUnten = 228.0;
        private double zKisteOben = 138.0;


        public Rectangle getRectangle()
        {
            return kiste;
        }
        public Kiste(Canvas flaeche, double x, double y, bool seitDarstellung)
        {
            this.angehoben = false;
            this.seitlicheKiste = seitDarstellung;
            this.xKoordinate = x;
            this.yKoordinate = y;
            this.oberflaeche = flaeche;

            erstelleKiste();
        }

        private void erstelleKiste()
        {
            if (this.oberflaeche.Dispatcher.CheckAccess())
            {
                this.kiste = new Rectangle();
                this.kiste.Fill = Brushes.Brown;

                if (this.seitlicheKiste == false)
                {
                    this.kiste.Width = kiste.Height = 39.0;
                }
                else
                {
                    this.kiste.Width = 39.0;
                    this.kiste.Height = 13.0;
                }

                this.kiste.SetValue(Canvas.LeftProperty, this.xKoordinate);
                this.kiste.SetValue(Canvas.TopProperty, this.yKoordinate);

                this.oberflaeche.Children.Insert(12, this.kiste);
            }
            else
            {
                erstelleKisteHandler handler =
                         new erstelleKisteHandler(this.erstelleKiste);
                this.oberflaeche.Dispatcher.BeginInvoke(handler);
            }
        }

        public void setKistePosition(double x, double y)
        {
            if (this.oberflaeche.Dispatcher.CheckAccess())
            {
                this.kiste.SetValue(Canvas.TopProperty, y);
                this.kiste.SetValue(Canvas.LeftProperty, x);
            }
            else
            {
                moveKistePositionHandler handler =
                         new moveKistePositionHandler(this.setKistePosition);
                this.oberflaeche.Dispatcher.BeginInvoke(handler, x, y);
            
            
            }
        }

        public void setKisteOben()
        {
            this.setKistePosition(this.xKoordinate, zKisteOben);
        }

        public void setKisteUnten()
        {
            this.setKistePosition(this.xKoordinate, zKisteUnten);
        }

        //public void bewegungXrichtungPositiv()
        //{
        //    this.xKoordinate++;
        //    this.kiste.SetValue(Canvas.LeftProperty, this.xKoordinate);
        //}

        //public void bewegungXrichtungNegativ()
        //{
        //    this.xKoordinate--;
        //    this.kiste.SetValue(Canvas.LeftProperty, this.xKoordinate);
        //}

        //public void bewegungYrichtungPositiv()
        //{
        //    this.yKoordinate++;
        //    this.kiste.SetValue(Canvas.TopProperty, this.yKoordinate);
        //}

        //public void bewegungYrichtungNegativ()
        //{
        //    this.yKoordinate--;
        //    this.kiste.SetValue(Canvas.TopProperty, this.yKoordinate);
        //}

        public double getXfromID(int ID)
        {
            double toX;

            if (ID == 1 || ID == 5 ||ID == 0)
            {
                toX = 433.0; // 183.0;
            }
            else if (ID == 2 || ID == 6)
            {
                toX = 351.0; // 228.0;
            }
            else if (ID == 3 ||  ID == 7)
            {
                toX = 271.0; // 293.0;
            }
            else 
            {
                toX = 215.0; // 358.0;
            }
            return toX;
        }

        public double getYfromID(int ID)
        {
            double toY;

            if (ID == 1)
            {
                toY = 239.0; // 123.0;
            }
            else if (ID == 2)
            {
                toY = 239.0; // 168.0;
            }
            else if (ID == 3)
            {
                toY = 239.0; // 168.0;
            }
            else
            {
                toY = 186.0; // 168.0;
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
