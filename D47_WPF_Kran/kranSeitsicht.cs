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
    public delegate void MoveKranarmSeitHandler(double z);
    public class kranSeitsicht
    {
        Seitenansicht seitSicht;
        double xKoordinate;
        double yKoordiante;

        //private int rahmenBreite = 560;
        //private int yRahmen = 100;
        //private int xRahmen = 20;
        private int maxHoehe = 10;
        private int minHoehe = 230;
        private int breiteArm = 10;
        private int hoeheArm = 130;
        private int yKoordinateAufhaengung = 85;

        private Line kranarm;
        private Line aufhaengung;

        public kranSeitsicht(Seitenansicht seit, double x, double y)
        {
            this.seitSicht = seit;
            this.xKoordinate = x;
            this.yKoordiante = y;

            erstelleKran();
        }

        private void erstelleKran()
        {
            this.kranarm = new Line();
            this.kranarm.X1 = this.xKoordinate;
            this.kranarm.X2 = this.xKoordinate;
            this.kranarm.Y1 = this.yKoordiante;
            this.kranarm.Y2 = this.yKoordiante + this.hoeheArm;
            this.kranarm.StrokeThickness = this.breiteArm;
            this.kranarm.Stroke = Brushes.DarkGray;
            this.kranarm.Fill = Brushes.Black;
            this.seitSicht.Children.Add(this.kranarm);

            this.aufhaengung = new Line();
            this.aufhaengung.X1 = this.xKoordinate;
            this.aufhaengung.X2 = this.xKoordinate;
            this.aufhaengung.Y1 = this.yKoordinateAufhaengung;
            this.aufhaengung.Y2 = this.yKoordinateAufhaengung + 10;
            this.aufhaengung.Fill = Brushes.Black;
            this.aufhaengung.Stroke = Brushes.Black;
            this.aufhaengung.StrokeThickness = 30;
            this.seitSicht.Children.Add(this.aufhaengung);
        }

        public void setKranPosition(double x)
        {
            if (seitSicht.Dispatcher.CheckAccess())
            {
                this.kranarm.X1 = this.kranarm.X2 = x + 15;
                this.aufhaengung.X1 = this.aufhaengung.X2 = x  + 15;
            }
            else
            {
                MoveKranarmSeitHandler handler =
                         new MoveKranarmSeitHandler(this.setKranPosition);
                seitSicht.Dispatcher.BeginInvoke(handler, x);
            }
        }

        public void setKranarmHoehe(double z)
        {
            if (seitSicht.Dispatcher.CheckAccess())
            {
                this.kranarm.Y1 = z;
                this.kranarm.Y2 = this.kranarm.Y1 + this.hoeheArm;
            }
            else
            {
                MoveKranarmSeitHandler handler =
                         new MoveKranarmSeitHandler(this.setKranarmHoehe);
                seitSicht.Dispatcher.BeginInvoke(handler, z);
            }
        }

        public void setKranarmOben()
        {
            this.setKranarmHoehe(this.maxHoehe);
        }

        public void setKranarmUnten()
        {
            this.setKranarmHoehe(this.minHoehe);
        }

        public bool checkKranarmUnten()
        {
            if ((this.yKoordiante + this.hoeheArm) == this.minHoehe)
            {
                return true;
            }
            return false;
        }

        public bool checkKranarmOben()
        {
            if (this.yKoordiante == this.maxHoehe)
                return true;

            Console.WriteLine("Test oben");

            return false;
        }
    }
}
