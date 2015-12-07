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
        private int breiteArm;
        private int hoeheArm;
        private int startX;
        private int startY;

        public Line kranarm;

        public Kranarm(int rahmenBreite, int maxHoehe, int minHoehe, int breiteArm, int hoeheArm, int startX, int startY)
        {
            this.rahmenBreite = rahmenBreite;
            this.maxHoehe = maxHoehe;
            this.minHoehe = minHoehe;
            this.breiteArm = breiteArm;
            this.hoeheArm = hoeheArm;
            this.startX = startX;
            this.startY = startY;

            KranarmInit();
        }

        private void KranarmInit()
        {
            kranarm = new Line();
            kranarm.X1 = this.startX;
            kranarm.X2 = this.startX;
            kranarm.Y1 = this.startY;
            kranarm.Y2 = this.startY + this.hoeheArm;
            kranarm.StrokeThickness = this.breiteArm;
            kranarm.Stroke = Brushes.Black;
        }
    }
}
