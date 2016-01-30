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
    public delegate void MoveKranarmSeitHandler(double z);
    public delegate void ArrivedAtBottomHandler();
    public class kranSeitsicht
    {
        Seitenansicht seitSicht;
        double xKoordinate;
        double yKoordiante;

        public event ArrivedAtBottomHandler fertig;

        Kran kranarmKlasse;



        //private int rahmenBreite = 560;
        //private int yRahmen = 100;
        //private int xRahmen = 20;
        private int maxHoehe = 10;
        private int minHoehe = 230;
        private int breiteArm = 10;
        private int hoeheArm = 150;
        private int yKoordinateAufhaengung = 85;
        private bool movingkranarm = false;

      

        private Line kranarm;
        private Line aufhaengung;

        public kranSeitsicht(Seitenansicht seit, double x, double y, Kran arm)
        {
            this.seitSicht = seit;
            this.xKoordinate = x;
            this.yKoordiante = y;

            erstelleKran();

            this.kranarmKlasse = arm;
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

        public double YKoordiante
        {
            get { return yKoordiante; }
            set { yKoordiante = value; }
        }

        public void setKranPosition(double x)
        {
            if (seitSicht.Dispatcher.CheckAccess())
            {
                this.kranarm.X1 = this.kranarm.X2 = x + 15;
                this.aufhaengung.X1 = this.aufhaengung.X2 = x  + 15;
                this.xKoordinate = this.kranarm.X1;
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
                this.yKoordiante = this.kranarm.Y1;

                this.yKoordiante = z;
                if (this.kranarmKlasse.kisteKran != null)
                {
                    this.kranarmKlasse.kisteKran.setKistenPosition(this.xKoordinate - 19, this.kranarmKlasse.YKoordinate); //x
                    this.kranarmKlasse.kisteKran.setKisteHoehe(this.yKoordiante + 149.0); 
                }
            }
            else
            {
                MoveKranarmSeitHandler handler =
                         new MoveKranarmSeitHandler(this.setKranarmHoehe);
                seitSicht.Dispatcher.BeginInvoke(handler, z);
            }
        }

        public bool Movingkranarm
        {
            get { return movingkranarm; }
            set { movingkranarm = value; }
        }

        public void moveKranarmOben()
        {
            while (!checkKranarmOben()) 
            {
                movingkranarm = true;
                this.yKoordiante--;
                this.setKranarmHoehe(this.yKoordiante);
                Thread.Sleep(50);
            }
        }


        public void moveKranarmUnten()
        {
            while (!checkKranarmUnten())
            {
                movingkranarm = true;
                this.yKoordiante++;
                this.setKranarmHoehe(this.yKoordiante);
                Thread.Sleep(50);
                
            }
        }

        public double setKranarmOben()
        {
            this.setKranarmHoehe(this.maxHoehe);

            return this.yKoordiante;
        }

        public double setKranarmUnten()
        {
            this.setKranarmHoehe(this.minHoehe);

            return this.yKoordiante;
        }

        public bool checkKranarmUnten()
        {
            if ((this.yKoordiante + this.hoeheArm) == this.minHoehe)
            {
                movekranarmObenTS();

                if (this.fertig != null)
                    this.fertig();

                return true;
                
            }
            return false;
        }

        public bool checkKranarmOben()
        {
            if (this.yKoordiante == this.maxHoehe)
                return true;

            //Console.WriteLine("Test oben");

            return false;
        }

        public void movekranarmObenTS()
        {
            ThreadStart ts = new ThreadStart(this.moveKranarmOben);
            Thread t = new Thread(ts);
            t.Start();
        }

        public void movekranarmUntenTS()
        {
            ThreadStart ts = new ThreadStart(this.moveKranarmUnten);
            Thread t = new Thread(ts);
            t.Start();
        }
    }
}
