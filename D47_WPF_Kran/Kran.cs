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
        public int x1_left;
        public int x2_left;
        public int x1_right;
        public int x2_right;
        public int y1_left;
        public int y2_left;
        public int y1_right;
        public int y2_right;
        //private int xKoordinate = 0;
       // private int yKoordinate = 0;
        public Rectangle kran;
        public Line links;
        public Line rechts;


        public Kran(int x1, int x2, int y1, int y2, Rectangle kran, Line links, Line rechts)
        {
                 this.x1_left = x1;
                 this.x2_left = x2;
                 this.x1_right = x1 + (int)width;
                 this.x2_right = x2 + (int)width;
                 this.y1_left = y1;
                 this.y2_left = y2;
                 this.y1_right = y1;
                 this.y2_right = y2;

                 this.kran = kran;
                 this.links = links;
                 this.rechts = rechts;

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

           
            
        }


        public void bewegungXrichtungPositiv()
        {
            this.leftProperty += 10.0;
            this.x1_left += 10;
            this.x1_right += 10;

            Console.WriteLine("{0}--{1}--{2}", this.leftProperty, this.x1_left, this.x1_right);
        }

        public void bewegungXrichtungNegativ()
        {
            this.leftProperty--;
        }

        public void bewegungYrichtungPositiv()
        {
            this.topProperty++;
        }

        public void bewegungYrichtungNegativ()
        {
            this.topProperty--;
        }
       

    }

}
