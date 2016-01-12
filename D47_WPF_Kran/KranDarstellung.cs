using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// 
     public delegate void MoveKranHandler();
     public delegate void MoveKisteHandler(int pos);
    public class KranDarstellung : Canvas
    {
        public Kran KranPic;
        Rectangle kran;
        public Kiste kistePic;
        private int xRahmen = 20;
        private int yRahmen = 20;
        private int hoeheRahmen = 230;
        private int breiteRahmen = 560;

        private Seitenansicht sideView;

        private int startX = 100;

        public void setSideView(Seitenansicht view)
        {
            this.sideView = view;
        }

        //Konstruktor
        //Erstellt das Aussehen des CustumControls
        public KranDarstellung()
        {
            this.Background = Brushes.Bisque;
            this.Height = 270;
            this.Width = 600;
            erstelle_Rahmen(this.xRahmen, this.xRahmen + this.breiteRahmen, this.yRahmen, this.yRahmen);
            erstelle_Rahmen(this.xRahmen, this.xRahmen + this.breiteRahmen, this.yRahmen + this.hoeheRahmen, this.yRahmen + this.hoeheRahmen);
            erstelle_Rahmen(this.xRahmen, this.xRahmen, this.yRahmen - 6, this.yRahmen + this.hoeheRahmen + 6);
            erstelle_Rahmen(this.xRahmen + this.breiteRahmen, this.xRahmen + this.breiteRahmen, this.yRahmen - 6, this.yRahmen + this.hoeheRahmen + 6);

            //KranSeite = new Seitenansicht();

            erstelle_Laufband();
            erstelle_Kiste(455.0, 123.0);
            
            Line schiene_links = new Line();
            this.Children.Add(schiene_links);
            Line schiene_rechts = new Line();
            this.Children.Add(schiene_rechts);
            this.kran = new Rectangle();
            this.Children.Add(kran);
           
            this.KranPic = new Kran(this.startX, this.startX, 14, 256, this.breiteRahmen, this.hoeheRahmen, this.xRahmen, this.yRahmen,kran, schiene_links, schiene_rechts);
            
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
           

            erstelle_Lager(180.0, 120.0);
            erstelle_Lager(225.0, 165.0);
            erstelle_Lager(290.0, 165.0);
            erstelle_Lager(355.0, 165.0);
            erstelle_Lager(30.0, 30.0);
            erstelle_Lager(90.0, 30.0);


            
            Rectangle laufband = new Rectangle();
            laufband.Fill = Brushes.Gray;
            laufband.Width = 275.0;
            laufband.Height = 45.0;
            laufband.SetValue(KranDarstellung.LeftProperty, 225.0);
            laufband.SetValue(KranDarstellung.TopProperty, 120.0);
            this.Children.Add(laufband);

            Rectangle Lagerturm = new Rectangle();
            Lagerturm.Stroke = Brushes.Yellow;
            Lagerturm.StrokeThickness = 5;
            Lagerturm.Width = 50.0;
            Lagerturm.Height = 50.0;
            Lagerturm.SetValue(KranDarstellung.LeftProperty, 450.0);
            Lagerturm.SetValue(KranDarstellung.TopProperty, 117.0);
            this.Children.Add(Lagerturm);

        }

        //Erstellen der Kiste
        //Übergabe der Start-Koordinaten
        public void erstelle_Kiste(double x, double y)
        {
            Rectangle kiste = new Rectangle();
            
            this.Children.Add(kiste);
            kistePic = new Kiste(x, y, kiste, false);  
        }

        public void erstelle_Lager(double x, double y)
        {
            Rectangle lager = new Rectangle();
            lager.Fill = Brushes.Red;
            lager.Width = 45.0;
            lager.Height = 45.0;
            lager.SetValue(Canvas.TopProperty, y);
            lager.SetValue(Canvas.LeftProperty, x);
            lager.Stroke = Brushes.Black;
            lager.StrokeThickness = 1;
            this.Children.Add(lager);
        }

        public void moveKranLeft()
        {
            if (this.Dispatcher.CheckAccess())
            {
                KranPic.bewegungXrichtungNegativ();
                this.sideView.kranarmPic.moveLinks();
                zeichner();
            }
            else if (this.KranPic.testLinks() == false)
            {
                MoveKranHandler handler =
                         new MoveKranHandler(this.moveKranLeft);   // TryMoveBall
                this.Dispatcher.BeginInvoke(handler);
            }
        }

        public void moveKranRechts()
        {
            if (this.Dispatcher.CheckAccess())
            {
                KranPic.bewegungXrichtungPositiv();
                this.sideView.kranarmPic.moveRechts();
                zeichner();
            }
            else if (this.KranPic.testRechts() == false)
            {
                MoveKranHandler handler =
                         new MoveKranHandler(this.moveKranRechts);   // TryMoveBall
                this.Dispatcher.BeginInvoke(handler);
            }
        }

        public void moveKranHoch()
        {
            if (this.Dispatcher.CheckAccess())
            {
                KranPic.bewegungYrichtungNegativ();
                zeichner();
            }
            else if (this.KranPic.testOben() == false)
            {
                MoveKranHandler handler =
                         new MoveKranHandler(this.moveKranHoch);   // TryMoveBall
                this.Dispatcher.BeginInvoke(handler);
            }
        }

        public void moveKranRunter()
        {
            if (this.Dispatcher.CheckAccess())
            {
                KranPic.bewegungYrichtungPositiv();
                zeichner();
            }
            else if(this.KranPic.testUnten() == false)
            {
                MoveKranHandler handler =
                         new MoveKranHandler(this.moveKranRunter);   // TryMoveBall
                this.Dispatcher.BeginInvoke(handler);
            }
        }

        private void zeichner()
        {
            /*KranPic.leftProperty += 1.0;
            KranPic.x1_left += 1.0;
            KranPic.x1_right += 1.0;*/

            KranPic.links.X1 = KranPic.x1_left;
            KranPic.links.X2 = KranPic.x2_left;
            KranPic.rechts.X1 = KranPic.x1_right;
            KranPic.rechts.X2 = KranPic.x2_right;
            this.sideView.kranarmPic.kranarm.X1 = this.sideView.kranarmPic.actX + this.sideView.kranarmPic.abstandArmX;
            this.sideView.kranarmPic.kranarm.X2 = this.sideView.kranarmPic.actX + this.sideView.kranarmPic.abstandArmX;
            this.sideView.kranarmPic.aufhaengung.X1 = this.sideView.kranarmPic.actX;
            this.sideView.kranarmPic.aufhaengung.X2 = this.sideView.kranarmPic.actX + 30;

            Console.WriteLine("{0}--{1}--{2}--{3}", this.sideView.kranarmPic.kranarm.X1, this.sideView.kranarmPic.kranarm.X2, this.sideView.kranarmPic.aufhaengung.X1, this.sideView.kranarmPic.aufhaengung.X2);

            KranPic.kran.SetValue(Canvas.LeftProperty, KranPic.leftProperty);
            KranPic.kran.SetValue(Canvas.TopProperty, KranPic.topProperty);
        }

        public void moveKisteTo(int pos)
        {
            if (this.Dispatcher.CheckAccess())
            {
                if (this.kistePic.xKoordinate == this.kistePic.getXfromPosition(pos))
                {
                    if (this.kistePic.yKoordinate > this.kistePic.getYfromPosition(pos))
                        this.kistePic.bewegungYrichtungNegativ();
                    if (this.kistePic.yKoordinate < this.kistePic.getYfromPosition(pos))
                        this.kistePic.bewegungYrichtungPositiv();
                }
                if (this.kistePic.xKoordinate > this.kistePic.getXfromPosition(pos))
                {
                    this.kistePic.bewegungXrichtungNegativ();
                    this.sideView.kiste_xNegativ();
                }
                if (this.kistePic.xKoordinate < this.kistePic.getXfromPosition(pos))
                {
                    this.kistePic.bewegungXrichtungPositiv();
                    this.sideView.kiste_xPositiv();
                }

                Console.WriteLine("{0} ... {1} ", this.kistePic.xKoordinate, this.kistePic.getXfromPosition(pos));
            }
            else if ((this.kistePic.yKoordinate != this.kistePic.getYfromPosition(pos)) || (this.kistePic.xKoordinate != this.kistePic.getXfromPosition(pos)))
            {
                MoveKisteHandler handler =
                         new MoveKisteHandler(this.moveKisteTo);   // TryMoveBall
                this.Dispatcher.BeginInvoke(handler, pos);
            }
        }
    }
}
