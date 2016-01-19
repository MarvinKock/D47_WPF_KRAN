using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace D47_WPF_Kran
{
    public class Kranarm
    {
        private int rahmenBreite;
        private int maxHoehe;
        private int minHoehe;
        public int breiteArm;
        public int hoeheArm;
        public int actX;
        public int actY;
        public int abstandArmX = 15;

        public Line kranarm;
        public Line aufhaengung;

        public Kranarm(int rahmenBreite, int maxHoehe, int minHoehe, int breiteArm, int hoeheArm, int startX, int startY, Line arm, Line aufhaengung)
        {
            this.rahmenBreite = rahmenBreite;
            this.maxHoehe = maxHoehe;
            this.minHoehe = minHoehe;
            this.breiteArm = breiteArm;
            this.hoeheArm = hoeheArm;
            this.actX = startX;
            this.actY = startY;

            Console.WriteLine("Kranarm Konstruktor");

            this.kranarm = arm;
            this.aufhaengung = aufhaengung;

            KranarmInit();
        }

        private void KranarmInit()
        {
            
            kranarm.X1 = this.actX + this.abstandArmX;
            kranarm.X2 = this.actX + this.abstandArmX;
            kranarm.Y1 = this.actY;
            kranarm.Y2 = this.actY + this.hoeheArm;
            kranarm.StrokeThickness = this.breiteArm;
            kranarm.Stroke = Brushes.DarkGray;
            kranarm.Fill = Brushes.Black;


            this.aufhaengung.X1 = this.actX ;
            this.aufhaengung.X2 = this.actX + 30;
            this.aufhaengung.Y1 = 90;
            this.aufhaengung.Y2 = 90;
            this.aufhaengung.Fill = Brushes.Black;
            this.aufhaengung.Stroke = Brushes.Black;
            this.aufhaengung.StrokeThickness = 12;

            Console.WriteLine("Kranarm Init");
            //this.Children.Add(linie);
        }

        public void moveRechts()
        {
            this.actX++;
            
        }

        public void moveLinks()
        {
            this.actX--;
        }

        public void moveArmUnten()
        {
            this.actY++;
        }

        public void moveArmOben()
        {
            this.actY--;
        }

        public bool testArmOben()
        {
            if (this.actY == this.maxHoehe)
                return true;

            Console.WriteLine("Test oben");

            return false;
        }

        public bool testArmUnten()
        {
            if ((this.actY + this.hoeheArm) == this.minHoehe)
            {
                return true;
            }

            Console.WriteLine("Test unten");

            return false;
        } 
    }
}
