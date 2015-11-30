using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace D47_WPF_Kran
{
    class Kiste
    {
        private int breite;
        private int hoehe;
        private Rectangle kiste;
        private int xKoordinate;
        private int yKoordinate;

        public Kiste(int breite, int hoehe, int x, int y, Rectangle kiste)
        {
            this.breite = breite;
            this.hoehe = hoehe;
            this.kiste = kiste;
            this.xKoordinate = x;
            this.yKoordinate = y;
        }

        public void bewegungXrichtungPositiv()
        {
            this.xKoordinate++;
        }

        public void bewegungXrichtungNegativ()
        {
            this.xKoordinate--;
        }

        public void bewegungYrichtungPositiv()
        {
            this.yKoordinate++;
        }

        public void bewegungYrichtungNegativ()
        {
            this.yKoordinate--;
        }
    }
}
