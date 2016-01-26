using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D47_WPF_Kran
{
    public class Kisten
    {
        KranDarstellung draufSicht;
        Seitenansicht seitSicht;
        double xKoordinate;
        double yKoordinate;
        double zKoordinate;

        private Kiste seitKiste;
        private Kiste draufKiste;
        private int kisteID;


        private bool angehoben = false;

        public Kisten(KranDarstellung drauf, Seitenansicht seit, double x, double y, double z, int kistenID)
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

        public void changeID(int ID)
        {
            this.kisteID = ID;
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

        public void kisteAnheben()
        {
            this.angehoben = true;
        }

        public bool getAngehoben()
        {
            if (this.angehoben == true)
                return true;
            else
                return false;
        }

        public void kisteLoslassen()
        {
            this.angehoben = false;
        }

        public void moveKistetoLager(int Lager)
        {
            switch(Lager)
            {
                case 1: setKistenPosition(161.0, 53.0); setKisteHoehe(228.0); break;
                case 2: setKistenPosition(215.0, 53.0); setKisteHoehe(228.0); break;
                case 3: setKistenPosition(215.0, 186.0); setKisteHoehe(228.0); break;
                case 4: setKistenPosition(271.0, 239.0); setKisteHoehe(228.0); break;
                case 5: setKistenPosition(351.0, 239.0); setKisteHoehe(228.0); break;
                case 6: setKistenPosition(433.0, 239.0); setKisteHoehe(228.0); break;
                default: break;
            }
        }

       /*  erstelle_Lager(211.0, 184.0, 45, 45); //
		  erstelle_Lager(267.0, 229.0,  55, 45);
		  erstelle_Lager(347.0, 229.0,  55, 45);
		  erstelle_Lager(429.0, 229.0,  55, 45);
		  erstelle_Lager(156.0, 50.0,  45, 45);
		  erstelle_Lager(211.0, 50.0,  45, 45);*/
        //public double getXfromPos(int pos)
        //{
        //    return this.draufSicht.getXfromPosition(pos);
        //}

        //public double getYfromPos(int pos)
        //{
        //    return this.draufSicht.getYfromPosition(pos);
        //}

        //public int getKistenID()
        //{
        //    return this.kisteID;
        //}

        //public void setKistenPosition(int x, int y, int z)
        //{
        //    this.seitAnsicht.setPosition(x, y);
        //    this.draufSicht.setPosition(x, z);
        //}

        //public void moveKistenXPositiv()
        //{
        //    seitAnsicht.bewegungXrichtungPositiv();
        //    draufSicht.bewegungXrichtungPositiv();
        //    this.xKoordinate = draufSicht.xKoordinate;
        //}

        //public void moveKistenXNegativ()
        //{
        //    seitAnsicht.bewegungXrichtungNegativ();
        //    draufSicht.bewegungXrichtungNegativ();
        //    this.xKoordinate = draufSicht.xKoordinate;
        //}

        //public void moveKistenYPositiv()
        //{
        //    draufSicht.bewegungYrichtungPositiv();
        //    this.yKoordinate = draufSicht.yKoordinate;
        //}

        //public void moveKistenYNegativ()
        //{
        //    draufSicht.bewegungYrichtungNegativ();
        //    this.yKoordinate = draufSicht.yKoordinate;
        //}

        //public void moveKistenOben()
        //{
        //    seitAnsicht.bewegungYrichtungPositiv();
        //    this.zKoordinate = seitAnsicht.yKoordinate;
        //}

        //public void moveKistenUnten()
        //{
        //    seitAnsicht.bewegungYrichtungNegativ();
        //    this.zKoordinate = seitAnsicht.yKoordinate;
        //}

        
    }
}
