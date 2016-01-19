using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D47_WPF_Kran
{
    public class Kisten
    {
        private Kiste seitAnsicht;
        private Kiste draufSicht;
        private int kisteID;

        public double xKoordinate;
        public double yKoordinate;
        public double zKoordinate;

        private bool angehoben = false;

        public Kisten(Kiste seit, Kiste drauf, int kisteID)
        {
            this.seitAnsicht = seit;
            this.draufSicht = drauf;
            this.kisteID = kisteID;

            this.xKoordinate = this.draufSicht.xKoordinate;
            this.yKoordinate = this.draufSicht.yKoordinate;
            this.zKoordinate = this.seitAnsicht.yKoordinate;
        }

        public double getXfromPos(int pos)
        {
            return this.draufSicht.getXfromPosition(pos);
        }

        public double getYfromPos(int pos)
        {
            return this.draufSicht.getYfromPosition(pos);
        }

        public int getKistenID()
        {
            return this.kisteID;
        }

        public void setKistenPosition(int x, int y, int z)
        {
            this.seitAnsicht.setPosition(x, y);
            this.draufSicht.setPosition(x, z);
        }

        public void moveKistenXPositiv()
        {
            seitAnsicht.bewegungXrichtungPositiv();
            draufSicht.bewegungXrichtungPositiv();
            this.xKoordinate = draufSicht.xKoordinate;
        }

        public void moveKistenXNegativ()
        {
            seitAnsicht.bewegungXrichtungNegativ();
            draufSicht.bewegungXrichtungNegativ();
            this.xKoordinate = draufSicht.xKoordinate;
        }

        public void moveKistenYPositiv()
        {
            draufSicht.bewegungYrichtungPositiv();
            this.yKoordinate = draufSicht.yKoordinate;
        }

        public void moveKistenYNegativ()
        {
            draufSicht.bewegungYrichtungNegativ();
            this.yKoordinate = draufSicht.yKoordinate;
        }

        public void moveKistenOben()
        {
            seitAnsicht.bewegungYrichtungPositiv();
            this.zKoordinate = seitAnsicht.yKoordinate;
        }

        public void moveKistenUnten()
        {
            seitAnsicht.bewegungYrichtungNegativ();
            this.zKoordinate = seitAnsicht.yKoordinate;
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
    }
}
