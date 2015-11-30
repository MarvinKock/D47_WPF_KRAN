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
    class Kran
    {
        public Kran(int x1, int x2, int y1, int y2, Rectangle kran, Line links, Line rechts)
        {
            double width = 30.0;

            links.X1 = x1;
            links.X2 = x2;
            links.Y1 = y1;
            links.Y2 = y2;

            links.Fill = Brushes.Black;
            links.Stroke = Brushes.Gray;
            links.StrokeThickness = 5;
            //this.Children.Add(linie);



            rechts.X1 = x1 + width;
            rechts.X2 = x2 + width;
            rechts.Y1 = y1;
            rechts.Y2 = y2;

            rechts.Fill = Brushes.Black;
            rechts.Stroke = Brushes.Gray;
            rechts.StrokeThickness = 5;
            // this.Children.Add(linie2);


            kran.Fill = Brushes.Black;
            kran.Width = width;
            kran.Height = 40.0;
            kran.SetValue(Canvas.TopProperty, 100.0);
            kran.SetValue(Canvas.LeftProperty, (double)x1);
            //this.Children.Add(kran);
        }


    }

}
