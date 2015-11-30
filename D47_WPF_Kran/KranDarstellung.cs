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
        public Kran KranPic;
        //Konstruktor
        //Erstellt das Aussehen des CustumControls
        public KranDarstellung()
        {
            this.Background = Brushes.Bisque;
            erstelle_Rahmen(20, 580, 20, 20);
            erstelle_Rahmen(20, 580, 250, 250);
            erstelle_Rahmen(20, 20, 14, 256);
            erstelle_Rahmen(580, 580, 14, 256);



            erstelle_Kiste(100.0, 100.0);
            
            Line schiene_links = new Line();
            this.Children.Add(schiene_links);
            Line schiene_rechts = new Line();
            this.Children.Add(schiene_rechts);
            Rectangle kran = new Rectangle();
            this.Children.Add(kran);
           
            this.KranPic = new Kran(60, 60, 14, 256, kran, schiene_links, schiene_rechts);
            erstelle_Laufband();
        }

        //Erstellt den Rahmen des Kranes
        //Übergabe der Koordinaten
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

        //Erstellt den Kranschlitten und die Befestigung
        //Übergabe der Koordinaten
        

        //Erstellt das Laufband und die dazugehörigen Lager
        public void erstelle_Laufband()
        {
            double width = 15.0;

            erstelle_Lager(260.0, 130.0);
            erstelle_Lager(300.0, 170.0);
            erstelle_Lager(350.0, 170.0);
            erstelle_Lager(400.0, 170.0);

            Rectangle laufband = new Rectangle();
            laufband.Fill = Brushes.LightGray;
            laufband.Width = 150.0;
            laufband.Height = 40.0;
            laufband.SetValue(Canvas.LeftProperty, 300.0);
            laufband.SetValue(Canvas.TopProperty, 130.0);
            this.Children.Add(laufband);


        }

        //Erstellen der Kiste
        //Übergabe der Start-Koordinaten
        public void erstelle_Kiste(double x, double y)
        {
            Rectangle kiste = new Rectangle();
            
            this.Children.Add(kiste);
            Kiste kistePic = new Kiste(x, y, kiste);  
        }

        public void erstelle_Lager(double x, double y)
        {
            Rectangle lager = new Rectangle();
            lager.Fill = Brushes.SaddleBrown;
            lager.Width = 40.0;
            lager.Height = 40.0;
            lager.SetValue(Canvas.TopProperty, y);
            lager.SetValue(Canvas.LeftProperty, x);
            this.Children.Add(lager);
        }

        public void moveKran()
        {
            KranPic.bewegungXrichtungPositiv();

        }
    }
}
