using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace D47_WPF_Kran
{
    public class Kisten
    {
        KranDarstellung draufSicht;
        Seitenansicht seitSicht;
        double xKoordinate;
        double yKoordinate;
        double zKoordinate;

        private double zKisteUnten = 228.0;
        private double zKisteOben = 138.0;
        private Kiste seitKiste;
        private Kiste draufKiste;
        private int kisteID;

        public int KisteID
        {
            get { return kisteID; }
            set { kisteID = value; }
        }

        public Kisten(KranDarstellung drauf, Seitenansicht seit, double x, double y, double z, int kisteID)
        {
            this.seitSicht = seit;
            this.draufSicht = drauf;
            this.kisteID = kisteID;

            this.xKoordinate = x;
            this.yKoordinate = y;
            this.zKoordinate = z;

            erstelleKisten();
        }

        private void erstelleKisten()
        {
            this.seitKiste = new Kiste(this.seitSicht, this.xKoordinate, this.zKoordinate, true);
            this.draufKiste = new Kiste(this.draufSicht, this.xKoordinate, this.yKoordinate, false);
        }

        public Rectangle getRectangleDraufsicht()
        {
            Rectangle rec = draufKiste.getRectangle();
            return rec;
        }

        public Rectangle getRectangleSeitsicht()
        {
            Rectangle rec = seitKiste.getRectangle();
            return rec;
        }

        public void setKistenPosition(double x, double y)
        {
            this.seitKiste.setKistePosition(x, this.zKoordinate);
            this.xKoordinate = x;
            this.yKoordinate = y;
            this.draufKiste.setKistePosition(x, y);
        }

        public void setKisteHoehe(double z)
        {        
            this.seitKiste.setKistePosition(this.xKoordinate, z);
            this.zKoordinate = z;
        }

        public void setKisteOben()
        {
            this.seitKiste.setKisteOben();
            this.zKoordinate = zKisteOben;
        }

        public void setKisteUnten()
        {
            this.seitKiste.setKisteUnten();
            this.zKoordinate = zKisteUnten;
        }

        public void moveLeftTillStop()
        {
            ThreadStart ts = new ThreadStart(this.moveLeft);
            Thread t = new Thread(ts);
            t.Start();

        }

        private void moveLeft()
        {
            while(this.kisteID == 0)
            {
                this.xKoordinate--;
                this.draufKiste.setKistePosition(this.xKoordinate, this.yKoordinate);
                this.seitKiste.setKistePosition(this.xKoordinate, this.zKoordinate);
                Thread.Sleep(50);
            }
        }

        public void moveKistetoLager(int Lager)
        {
            switch(Lager)
            {
                case 5: setKistenPosition(161.0, 53.0); setKisteHoehe(228.0); break;
                case 6: setKistenPosition(215.0, 53.0); setKisteHoehe(228.0); break;
                case 4: setKistenPosition(215.0, 186.0); setKisteHoehe(228.0); break;
                case 3: setKistenPosition(271.0, 239.0); setKisteHoehe(228.0); break;
                case 2: setKistenPosition(351.0, 239.0); setKisteHoehe(228.0); break;
                case 1: setKistenPosition(433.0, 239.0); setKisteHoehe(228.0); break;
                default: break;
            }
        }

        public void setKisteToPusher(int pusher)
        {
            switch(pusher)
            {
                case 3: setKistenPosition(271.0, 186.0); setKisteHoehe(228.0); break;
                case 2: setKistenPosition(351.0, 186.0); setKisteHoehe(228.0); break;
                case 1: setKistenPosition(433.0, 186.0); setKisteHoehe(228.0); break;
                default: break;
            }
        }
     
        public void kisteToPos()
        {
            ThreadStart ts = new ThreadStart(moveKisteToPos);
            Thread t = new Thread(ts);
            t.Start();
        }

        public void moveKisteToPos()
        {
            double zielX = this.getXfromID();
            double zielY = this.getYfromID();
            Console.WriteLine("zielX: {0}, zielY: {1}", zielX, zielY);
            while (!(this.xKoordinate == zielX && this.yKoordinate == zielY))
            {
                //Console.WriteLine("Bewege Kiste");
                if (this.xKoordinate == zielX)
                {
                    if (this.yKoordinate > zielY)
                        this.yKoordinate--;//kiste.moveKistenYNegativ();
                    if (this.yKoordinate < zielY)
                        this.yKoordinate++; //kiste.moveKistenYPositiv();
                    //if (kiste.yKoordinate == kiste.getYfromPos(pos))
                    //    this.isRunning = false;
                }
                if (this.xKoordinate > zielX)
                {
                    this.xKoordinate--;
                    //kiste.moveKistenXNegativ();
                }
                if (this.xKoordinate < zielX)
                {
                    this.xKoordinate++;
                    //kiste.moveKistenXPositiv();
                }

                this.draufKiste.setKistePosition(this.xKoordinate, this.yKoordinate);
                this.seitKiste.setKistePosition(this.xKoordinate, this.zKoordinate);
                //Console.WriteLine("{0} ... {1} ", kiste.yKoordinate, kiste.getYfromPos(pos));
                Thread.Sleep(30);
            }
        }

        public double getXfromID()
        {
            return this.draufKiste.getXfromID(this.kisteID);
        }

        public double getYfromID()
        {
            return this.draufKiste.getYfromID(this.kisteID);
        }     
    }
}
