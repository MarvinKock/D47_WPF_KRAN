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
            this.draufKiste.setKistePosition(x, y);
        }

        public void setKisteHoehe(double z)
        {
            if(this.angehoben)
                this.seitKiste.setKistePosition(this.xKoordinate, z);
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
