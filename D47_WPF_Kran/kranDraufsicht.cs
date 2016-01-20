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

        //private int rahmenBreite = 560;
        private int rahmenHoehe = 301;
        private int yRahmen = 20;
        //private int xRahmen = 20;
        private int breiteHalterung = 5;
        private int breiteRahmen = 6;
        private double schlittenHoehe = 40.0;
        private double schlittenBreite = 30.0;

        private Rectangle kran;
        private Line linkeStrebe;
        private Line rechteStrebe;

        public kranDraufsicht(KranDarstellung drauf, double x, double y)
        {
            this.draufSicht = drauf;
            this.xKoordinate = x;
            this.yKoordiante = y;

            erstelleStreben();
            erstelleKran();
        }

        private void erstelleKran()
        {
            this.kran = new Rectangle();
            this.kran.Fill = Brushes.Black;
            this.kran.Width = this.schlittenBreite;
            this.kran.Height = this.schlittenHoehe;
            kran.SetValue(Canvas.TopProperty, yKoordiante);
            kran.SetValue(Canvas.LeftProperty, xKoordinate);
            draufSicht.Children.Add(kran);
        }
            

        private void erstelleStreben()
        {
            this.linkeStrebe = new Line();
            linkeStrebe.X1 = linkeStrebe.X2 = this.xKoordinate ;
            linkeStrebe.Y1 = this.yRahmen - breiteRahmen;
            linkeStrebe.Y2 = this.yRahmen + this.rahmenHoehe + breiteRahmen;
            linkeStrebe.Fill = Brushes.Black;
            linkeStrebe.Stroke = Brushes.Gray;
            linkeStrebe.StrokeThickness = breiteHalterung;
            draufSicht.Children.Add(linkeStrebe);

            this.rechteStrebe = new Line();
            rechteStrebe.X1 = rechteStrebe.X2 = linkeStrebe.X1 + this.schlittenBreite;
            rechteStrebe.Y1 = this.yRahmen - breiteRahmen;
            rechteStrebe.Y2 = this.yRahmen + this.rahmenHoehe + breiteRahmen;
            rechteStrebe.Fill = Brushes.Black;
            rechteStrebe.Stroke = Brushes.Gray;
            rechteStrebe.StrokeThickness = breiteHalterung;
            draufSicht.Children.Add(rechteStrebe);
        }

        public void setKranPosition(double x, double y)
        {
            if (draufSicht.Dispatcher.CheckAccess())
            {
                this.kran.SetValue(Canvas.TopProperty, y);
                this.kran.SetValue(Canvas.LeftProperty, x);
                this.linkeStrebe.X1 = this.linkeStrebe.X2 = x;
                this.rechteStrebe.X1 = this.rechteStrebe.X2 = this.linkeStrebe.X1 + this.schlittenBreite;
            }
            else
            {
                MoveKranDrausichtHandler handler =
                         new MoveKranDrausichtHandler(this.setKranPosition); 
                draufSicht.Dispatcher.BeginInvoke(handler, x, y);
            }
        }

        //public bool testOben()
        //{
        //    if (this.topProperty == (this.yRahmen + this.breiteRahmen / 2))
        //        return true;

        //    return false;
        //}

        //public bool testUnten()
        //{
        //    if (this.topProperty == (this.yRahmen + this.RahmenHoehe - this.height - this.breiteRahmen / 2))
        //        return true;

        //    return false;
        //}


        //public bool testRechts()
        //{
        //    if (this.x2_right == (this.xRahmen + this.RahmenBreite - this.breiteHalterung))
        //    {

        //        Console.WriteLine("Rechts");
        //        return true;
        //    }
        //    int test = this.xRahmen + this.breiteRahmen - this.breiteHalterung - this.breiteRahmen / 2;
        //    Console.WriteLine("{0}", test);

        //    //Console.WriteLine("<<<< Test Rechts >>>>");
        //    return false;
        //}

        //public bool testLinks()
        //{
        //    if (this.x1_left == (this.xRahmen + this.breiteRahmen / 2))
        //        return true;

        //    return false;
        //}
    }
}
