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
        double topProperty = 100.0;
        double leftProperty = 0.0;
        int x1_left;
        int x2_left;
        int x1_right;
        int x2_right;
        int y1_left;
        int y2_left;
        int y1_right;
        int y2_right;
        private int xKoordinate;
        private int yKoordinate;
        Rectangle kran;
        Line links;
        Line rechts;


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
