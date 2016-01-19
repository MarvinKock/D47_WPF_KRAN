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
    public delegate void MoveKranDrausichtHandler(double x, double y);
    public class kranDraufsicht
    {
        KranDarstellung draufSicht;
        double xKoordinate;
        double yKoordiante;

        private int rahmenBreite = 560;
        private int rahmenHoehe = 268;
        private int yRahmen = 20;
        private int xRahmen = 20;
        private int breiteHalterung = 5;
        private int breiteRahmen = 12;
        private double schlittenHoehe = 40.0;
        private double schlittenBreite = 30.0;

        private Rectangle kran;
        private Line links;
        private Line rechts;

        public kranDraufsicht(KranDarstellung drauf, double x, double y)
        {
            this.draufSicht = drauf;
            this.xKoordinate = x;
            this.yKoordiante = y;

            erstelleKran();
            erstelleStrebe(this.xKoordinate - 3);
        }

        private void erstelleKran()
        {
            this.kran = new Rectangle();
            this.kran.Fill = Brushes.Black;
            this.kran.Width = this.schlittenBreite;
            this.kran.Height = this.schlittenHoehe;
            kran.SetValue(Canvas.TopProperty, yKoordiante);
            kran.SetValue(Canvas.LeftProperty, xKoordinate);
        }
            

        private void erstelleStrebe(double start)
        {

        }

        public void setKranPosition(double x, double y)
        {
            if (draufSicht.Dispatcher.CheckAccess())
            {
                this.kran.SetValue(Canvas.TopProperty, y);
                this.kran.SetValue(Canvas.LeftProperty, x);
            }
            else
            {
                MoveKranDrausichtHandler handler =
                         new MoveKranDrausichtHandler(this.setKranPosition); 
                draufSicht.Dispatcher.BeginInvoke(handler, x, y);
            }
        }
    }
}
