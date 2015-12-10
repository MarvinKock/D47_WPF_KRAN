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
    ///     <MyNamespace:Seitenansicht/>
    ///
    /// </summary>

    public delegate void MoveKranarmHandler();

    public class Seitenansicht : Canvas
    {
        private int xRahmen = 20;
        private int yRahmen = 100;
        private int hoeheRahmen = 150;
        private int breiteRahmen = 560;

        private int startX = 100;

        public Kranarm kranarmPic;

        public Seitenansicht()
        {
            CanvasInit();
            KranInit();
            RahmenInit(this.xRahmen, this.xRahmen + this.breiteRahmen, this.yRahmen, this.yRahmen);
           // RahmenInit(this.xRahmen, this.xRahmen + this.breiteRahmen, this.yRahmen + this.hoeheRahmen, this.yRahmen + this.hoeheRahmen);
            RahmenInit(this.xRahmen, this.xRahmen, this.yRahmen - 6, this.yRahmen + this.hoeheRahmen + 6);
            RahmenInit(this.xRahmen + this.breiteRahmen, this.xRahmen + this.breiteRahmen, this.yRahmen - 6, this.yRahmen + this.hoeheRahmen + 6);
            //laufband
            erstelle_Laufband();
            //kiste
            //lager
              
        }

        private void CanvasInit()
        {
            this.Background = Brushes.Bisque;
            this.Height = 270;
            this.Width = 600;
        }

        private void RahmenInit(int x1, int x2, int y1, int y2)
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

        private void KranInit()
        {
            Line arm = new Line();
            this.Children.Add(arm);
            Line aufhaengung = new Line();
            this.Children.Add(aufhaengung);
            this.kranarmPic = new Kranarm(this.breiteRahmen, 10, 215, 10, 140, this.startX, 70, arm, aufhaengung);
        }

        public void erstelle_Laufband()
        {
            erstelle_Lager(30.0, 240.0);
            erstelle_Lager(90.0, 240.0);

            Rectangle Lagerturm = new Rectangle();
            Lagerturm.Stroke = Brushes.Yellow;
            Lagerturm.StrokeThickness = 5;
            Lagerturm.Width = 50.0;
            Lagerturm.Height = 80.0;
            Lagerturm.SetValue(Seitenansicht.LeftProperty, 445.0);
            Lagerturm.SetValue(Seitenansicht.TopProperty, 165.0);
            this.Children.Add(Lagerturm);

            Rectangle laufband = new Rectangle();
            laufband.Fill = Brushes.Gray;
            laufband.Width = 270.0;
            laufband.Height = 15.0;
            laufband.SetValue(Seitenansicht.LeftProperty, 225.0);
            laufband.SetValue(Seitenansicht.TopProperty, 240.0);
            this.Children.Add(laufband);

            erstelle_Lager(180.0, 240.0);
            erstelle_Lager(225.0, 240.0);
            erstelle_Lager(290.0, 240.0);
            erstelle_Lager(355.0, 240.0);                

        }

        public void erstelle_Lager(double x, double y)
        {
            Rectangle lager = new Rectangle();
            lager.Fill = Brushes.Red;
            lager.Width = 45.0;
            lager.Height = 15.0;
            lager.SetValue(Canvas.TopProperty, y);
            lager.SetValue(Canvas.LeftProperty, x);
            this.Children.Add(lager);
        }

        public void moveKranarmUnten()
        {
            if (this.Dispatcher.CheckAccess())
            {
                this.kranarmPic.moveArmUnten();
                zeichnerArm();
            }
            else if (this.kranarmPic.testArmUnten() == false)
            {
                MoveKranarmHandler handler =
                         new MoveKranarmHandler(this.moveKranarmUnten);   // TryMoveBall
                this.Dispatcher.BeginInvoke(handler);
            }
        }

        public void moveKranarmHoch()
        {
            Console.WriteLine("<moveKranarmHoch");
            if (this.Dispatcher.CheckAccess())
            {
                Console.WriteLine("<<Dispatcher rein");
                this.kranarmPic.moveArmOben();
                zeichnerArm();
            }
            else if (this.kranarmPic.testArmOben() == false)
            {
                Console.WriteLine("<<<Getestet");
                MoveKranarmHandler handler =
                         new MoveKranarmHandler(this.moveKranarmHoch);   // TryMoveBall
                this.Dispatcher.BeginInvoke(handler);
            }
            Console.WriteLine("<<<<Nach testen");
        }

        private void zeichnerArm()
        {
            this.kranarmPic.kranarm.Y1 = this.kranarmPic.actY;
            this.kranarmPic.kranarm.Y2 = this.kranarmPic.actY + this.kranarmPic.hoeheArm;
        }
    }
}
