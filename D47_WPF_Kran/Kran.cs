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
    public class Kran
    {
        double width = 30.0;
        double height = 40.0;
        public double topProperty = 100.0;
        public double leftProperty = 0.0;
        public double x1_left;
        public double x2_left;
        public double x1_right;
        public double x2_right;
        public double y1_left;
        public double y2_left;
        public double y1_right;
        public double y2_right;
        private int RahmenBreite;
        private int RahmenHoehe;
        private int yRahmen;
        private int xRahmen;
        private int breiteHalterung = 5;
        private int breiteRahmen = 12;
        //private int xKoordinate = 0;
       // private int yKoordinate = 0;
        public Rectangle kran;
        public Line links;
        public Line rechts;


        public Kran(double x1, double x2, double y1, double y2, int breite, int hoehe, int yRahmen, int xRahmen, Rectangle kran, Line links, Line rechts)
        {
            this.x1_left = x1;
            this.x2_left = x2;
            this.x1_right = x1 + width;
            this.x2_right = x2 + width;
            this.y1_left = y1;
            this.y2_left = y2;
            this.y1_right = y1;
            this.y2_right = y2;

            this.kran = kran;
            this.links = links;
            this.rechts = rechts;

            Console.WriteLine("{0}----{1}", this.x1_left, this.x2_right);

            links.X1 = this.x1_left;
            links.X2 = this.x2_left;
            links.Y1 = this.y1_left;
            links.Y2 = this.y2_left;

            links.Fill = Brushes.Black;
            links.Stroke = Brushes.Gray;
            links.StrokeThickness = 5;
            //this.Children.Add(linie);



            rechts.X1 = this.x1_right;
            rechts.X2 = this.x2_right;
            rechts.Y1 = this.y1_right;
            rechts.Y2 = this.y2_right;

            rechts.Fill = Brushes.Black;
            rechts.Stroke = Brushes.Gray;
            rechts.StrokeThickness = 5;
            // this.Children.Add(linie2);

            this.leftProperty = x1;
            kran.Fill = Brushes.Black;
            kran.Width = this.width;
            kran.Height = this.height;
            kran.SetValue(Canvas.TopProperty, topProperty);
            kran.SetValue(Canvas.LeftProperty, leftProperty);
            //this.Children.Add(kran);

            this.RahmenBreite = breite;
            this.RahmenHoehe = hoehe;
            this.xRahmen = xRahmen;
            this.yRahmen = yRahmen;
            
        }

        public void setPosition(double x, double y)
        {

        }
        

        public bool testOben()
        {
            if (this.topProperty == (this.yRahmen + this.breiteRahmen/2))
                return true;

            return false;
        }

        public bool testUnten()
        {
            if (this.topProperty == (this.yRahmen + this.RahmenHoehe - this.height - this.breiteRahmen / 2))
                return true;

            return false;
        }


        public bool testRechts()
        {
            if (this.x2_right == (this.xRahmen + this.RahmenBreite - this.breiteHalterung ))
            {
                
                Console.WriteLine("Rechts");
                return true;
            }
            int test = this.xRahmen + this.breiteRahmen - this.breiteHalterung - this.breiteRahmen / 2;
            Console.WriteLine("{0}", test);

            //Console.WriteLine("<<<< Test Rechts >>>>");
            return false;
        }

        public bool testLinks()
        {
            if (this.x1_left == (this.xRahmen + this.breiteRahmen/2))
                return true;

            return false;
        }


        public void bewegungXrichtungPositiv()
        {
            this.leftProperty += 1.0;
            this.x1_left += 1;
            this.x1_right += 1;
            this.x2_left += 1;
            this.x2_right += 1;

            //Console.WriteLine("{0}--{1}--{2}", this.leftProperty, this.x1_left, this.x1_right);
        }

        public void bewegungXrichtungNegativ()
        {
            this.leftProperty -= 1.0;
            this.x1_left -= 1;
            this.x1_right -= 1;
            this.x2_left -= 1;
            this.x2_right -= 1;
        }

        public void bewegungYrichtungPositiv()
        {
            this.topProperty += 1.0;
        }

        public void bewegungYrichtungNegativ()
        {
            this.topProperty -= 1.0;
        }
       

    }

}
