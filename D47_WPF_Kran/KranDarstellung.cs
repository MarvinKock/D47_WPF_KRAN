using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace D47_WPF_Kran
{
    /// <summary>
    /// Führen Sie die Schritte 1a oder 1b und anschließend Schritt 2 aus, um dieses benutzerdefinierte Steuerelement in einer XAML-Datei zu verwenden.
    ///
    /// Schritt 1a) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die im aktuellen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:D47_WPF_Kran"
    ///
    ///
    /// Schritt 1b) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die in einem anderen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:D47_WPF_Kran;assembly=D47_WPF_Kran"
    ///
    /// Darüber hinaus müssen Sie von dem Projekt, das die XAML-Datei enthält, einen Projektverweis
    /// zu diesem Projekt hinzufügen und das Projekt neu erstellen, um Kompilierungsfehler zu vermeiden:
    ///
    ///     Klicken Sie im Projektmappen-Explorer mit der rechten Maustaste auf das Zielprojekt und anschließend auf
    ///     "Verweis hinzufügen"->"Projekte"->[Navigieren Sie zu diesem Projekt, und wählen Sie es aus.]
    ///
    ///
    /// Schritt 2)
    /// Fahren Sie fort, und verwenden Sie das Steuerelement in der XAML-Datei.
    ///
    ///     <MyNamespace:KranDarstellung/>
    ///
    /// </summary>
    public class KranDarstellung : Canvas
    {
        static KranDarstellung()
        {
            
        }

        public KranDarstellung()
        {
            this.Background = Brushes.Bisque;
            erstelle_Rahmen(20, 580, 20, 20);
            erstelle_Rahmen(20, 580, 250, 250);
            erstelle_Rahmen(20, 20, 14, 256);
            erstelle_Rahmen(580, 580, 14, 256);
            

            erstelle_KranSchlitten(60, 60, 14, 256);

        }

        public void erstelle_Rahmen(int x1, int x2, int y1, int y2)
        {
            Line linie = new Line();
            linie.X1 = x1;
            linie.X2 = x2;
            linie.Y1 = y1;
            linie.Y2 = y2;
            linie.Fill = Brushes.Black;
            linie.Stroke = Brushes.Tomato;
            linie.StrokeThickness = 12;
            this.Children.Add(linie);
        }

        public void erstelle_KranSchlitten(int x1, int x2, int y1, int y2)
        {
            double width = 30.0;
            Line linie = new Line();

            linie.X1 = x1;
            linie.X2 = x2;
            linie.Y1 = y1;
            linie.Y2 = y2;

            linie.Fill = Brushes.Black;
            linie.Stroke = Brushes.LightSalmon;
            linie.StrokeThickness = 5;
            this.Children.Add(linie);

            Line linie2 = new Line();

            linie2.X1 = x1 + width;
            linie2.X2 = x2 + width;
            linie2.Y1 = y1 ;
            linie2.Y2 = y2 ;

            linie2.Fill = Brushes.Black;
            linie2.Stroke = Brushes.LightSalmon;
            linie2.StrokeThickness = 5;
            this.Children.Add(linie2);

            Rectangle kran = new Rectangle();
            kran.Fill = Brushes.SaddleBrown;
            kran.Width = width;
            kran.Height = 40.0;
            kran.SetValue(Canvas.TopProperty, 100.0);
            kran.SetValue(Canvas.LeftProperty, (double)x1);
            this.Children.Add(kran);
        }

        public void erstelle_Laufband(int x1, int x2, int y1, int y2)
        {


        }

        public void erstelle_Kiste(int x1, int x2, int y1, int y2)
        {

        }
    }
}
