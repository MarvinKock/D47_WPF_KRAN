using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace D47_WPF_Kran
{
    class kranSeitsicht
    {
        Seitenansicht seitSicht;
        double xKoordinate;
        double yKoordiante;

        private int rahmenBreite = 560;
        private int yRahmen = 100;
        private int xRahmen = 20;
        private int maxHoehe = 10;
        private int minHoehe = 230;
        private int breiteArm = 10;

        private Line kranarm;
        private Line aufhaengung;

        public kranSeitsicht(Seitenansicht seit, double x, double y)
        {
            this.seitSicht = seit;
            this.xKoordinate = x;
            this.yKoordiante = y;


        }

        private void erstelleKran()
        {
            //this.kranarm.X1 = this.actX + this.abstandArmX;
            //this.kranarm.X2 = this.actX + this.abstandArmX;
            //this.kranarm.Y1 = this.actY;
            //this.kranarm.Y2 = this.actY + this.hoeheArm;
            //this.kranarm.StrokeThickness = this.breiteArm;
            //this.kranarm.Stroke = Brushes.DarkGray;
            //this.kranarm.Fill = Brushes.Black;


            //this.aufhaengung.X1 = this.actX;
            //this.aufhaengung.X2 = this.actX + 30;
            //this.aufhaengung.Y1 = 90;
            //this.aufhaengung.Y2 = 90;
            //this.aufhaengung.Fill = Brushes.Black;
            //this.aufhaengung.Stroke = Brushes.Black;
            //this.aufhaengung.StrokeThickness = 12;
        }
    }
}
