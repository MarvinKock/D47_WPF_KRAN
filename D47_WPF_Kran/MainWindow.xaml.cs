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
    enum button
    {
        Rechts,
        Links,
        Hoch,
        Runter,
        Arm_Runter,
        Arm_Hoch
    }

    
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool isRunning = false;
        private button buttonClick;

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 1; i <= 4; i++)
                this.NummerLagerplatz.Items.Add(i);

            this.Kran.setSideView(this.AnsichtSeite);
        }

        private void KranLinks_Click(object sender, RoutedEventArgs e)
        {
            buttonClick = button.Links;
            if (isRunning == false)
            {
                Thread t;

                // erster Start
                ThreadStart ts = new ThreadStart(MoveElevatorPara);
                t = new Thread(ts);
                isRunning = true;

                t.Start();
            }
           
        }


        private void KranTop_Click(object sender, RoutedEventArgs e)
        {
            buttonClick = button.Hoch;
            if (isRunning == false)
            {
                Thread t;

                // erster Start
                ThreadStart ts = new ThreadStart(MoveElevatorPara);
                t = new Thread(ts);
                isRunning = true;

                t.Start();
            }
        
        }

        private void KranRechts_Click(object sender, RoutedEventArgs e)
        {
            buttonClick = button.Rechts;
            if (isRunning == false)
            {
                Thread t;

                // erster Start
                ThreadStart ts = new ThreadStart(MoveElevatorPara);
                t = new Thread(ts);
                isRunning = true;

                t.Start();
            }
           
        }

        private void KranBottom_Click(object sender, RoutedEventArgs e)
        {
            buttonClick = button.Runter;
            if (isRunning == false)
            {
                Thread t;

                // erster Start
                ThreadStart ts = new ThreadStart(MoveElevatorPara);
                t = new Thread(ts);
                isRunning = true;

                t.Start();
            }
            
        }

        private void MoveElevatorPara()//(object o)
        {

            while (this.isRunning == true)
            {
                //if (Kran.KranPic.testRand() == false)
                //{
                if (buttonClick == button.Links)
                    Kran.moveKranLeft();
                if (buttonClick == button.Rechts)
                    Kran.moveKranRechts();
                if (buttonClick == button.Hoch)
                    Kran.moveKranHoch();
                if (buttonClick == button.Runter)
                    Kran.moveKranRunter();
                // Console.WriteLine("da");
                Thread.Sleep(30);
                //}
                //else
                //{
                //    return;
                //}

            }

            //this.ReachedFloor.Invoke(elevatorAtFloor);
        }

        private void KranStop_Click(object sender, MouseButtonEventArgs e)
        {
            isRunning = false;
        }

        private void RadioButton_Changed(object sender, RoutedEventArgs e)
        {
           if(sender == Manuell)
           {
               KranTop.Visibility = System.Windows.Visibility.Visible;
               KranRechts.Visibility = System.Windows.Visibility.Visible;
               KranLinks.Visibility = System.Windows.Visibility.Visible;
               KranBottom.Visibility = System.Windows.Visibility.Visible;
               Kranarm_Down.Visibility = System.Windows.Visibility.Visible;
               Kranarm_Up.Visibility = System.Windows.Visibility.Visible;
           }
           if (sender == Beobachten)
           {
               KranTop.Visibility = System.Windows.Visibility.Hidden;
               KranRechts.Visibility = System.Windows.Visibility.Hidden;
               KranLinks.Visibility = System.Windows.Visibility.Hidden;
               KranBottom.Visibility = System.Windows.Visibility.Hidden;
               Kranarm_Down.Visibility = System.Windows.Visibility.Hidden;
               Kranarm_Up.Visibility = System.Windows.Visibility.Hidden;
           }
        }

        private void KranarmStop_Click(object sender, MouseButtonEventArgs e)
        {
            isRunning = false;
        }

        private void Kranarm_Up_Click(object sender, RoutedEventArgs e)
        {
            if (isRunning == false)
            {
                Thread t;

                // erster Start
                ThreadStart ts = new ThreadStart(MoveKranarm);
                t = new Thread(ts);
                isRunning = true;

                t.Start();
            }
            buttonClick = button.Arm_Hoch;
        }

        private void Kranarm_Down_Click(object sender, RoutedEventArgs e)
        {
            if (isRunning == false)
            {
                Thread t;

                // erster Start
                ThreadStart ts = new ThreadStart(MoveKranarm);
                t = new Thread(ts);
                isRunning = true;

                t.Start();
            }
            buttonClick = button.Arm_Runter;
        }

        private void MoveKranarm()//(object o)
        {

            while (this.isRunning == true)
            {
                //if (Kran.KranPic.testRand() == false)
                //{
                if (buttonClick == button.Arm_Runter)
                    AnsichtSeite.moveKranarmUnten();
                if (buttonClick == button.Arm_Hoch)
                    AnsichtSeite.moveKranarmHoch();
                   
             
                //Console.WriteLine("da");
                Thread.Sleep(30);
                //}
                //else
                //{
                //    return;
                //}

            }

            //this.ReachedFloor.Invoke(elevatorAtFloor);
        }

       
        private void moveKiste(Object opos)
        {
            int pos = (int)opos;
            while(this.isRunning)
            {
                this.Kran.moveKisteTo(pos);
                Thread.Sleep(30);
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            isRunning = false;
        }

        private void NummerLagerplatz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object pos = this.NummerLagerplatz.SelectedItem;

            if (isRunning == false)
            {
                Thread t;

                // erster Start
                ParameterizedThreadStart pts = new ParameterizedThreadStart(moveKiste);

                t = new Thread(pts);
                isRunning = true;

                t.Start(pos);
            }
        }

        private void KisteAnheben_Click(object sender, RoutedEventArgs e)
        {
            this.Kran.kistePic.kisteAufnhemen();
        }

        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://10.8.0.203:53161/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("api/Band/Status");
                if (response.IsSuccessStatusCode)
                {
                    Band band = await response.Content.ReadAsAsync<Band>();
                    Console.WriteLine("{0}\t${1}\t{2}\t{3}", band.Ablageplatz, band.An, band.Einlagerplatz, band.Werkstück_id);
                }

                /* // HTTP POST
                 //var gizmo = new Product() { Name = "Gizmo", Price = 100, Category = "Widget" };
                 response = await client.PostAsJsonAsync("api/products", gizmo);
                 if (response.IsSuccessStatusCode)
                 {
                     Uri gizmoUrl = response.Headers.Location;

                     // HTTP PUT
                     gizmo.Price = 80;   // Update price
                     response = await client.PutAsJsonAsync(gizmoUrl, gizmo);

                     // HTTP DELETE
                     response = await client.DeleteAsync(gizmoUrl);
                 }*/
            }

        }

    }
}
