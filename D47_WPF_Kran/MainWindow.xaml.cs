﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        //public bool isRunning = false;
        private button buttonClick;
        bool? modeControlFlag = null;
        JsonObjectKranStatus kranSync = new JsonObjectKranStatus();

        private List<Kisten> ListKisten = new List<Kisten>();
        private double kisteStartX = 455.0;
        private double kisteStartY = 123.0;
        private double kisteStartZ = 228.0;
        private int KisteID = 1;

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
            if (this.Kran.isRunning == false)
            {
                Thread t;

                // erster Start
                ThreadStart ts = new ThreadStart(MoveElevatorPara);
                t = new Thread(ts);
                this.Kran.isRunning = true;

                t.Start();
            }
           
        }


        private void KranTop_Click(object sender, RoutedEventArgs e)
        {
            buttonClick = button.Hoch;
            if (this.Kran.isRunning == false)
            {
                Thread t;

                // erster Start
                ThreadStart ts = new ThreadStart(MoveElevatorPara);
                t = new Thread(ts);
                this.Kran.isRunning = true;

                t.Start();
            }
        
        }

        private void KranRechts_Click(object sender, RoutedEventArgs e)
        {
            buttonClick = button.Rechts;
            if (this.Kran.isRunning == false)
            {
                Thread t;

                // erster Start
                ThreadStart ts = new ThreadStart(MoveElevatorPara);
                t = new Thread(ts);
                this.Kran.isRunning = true;

                t.Start();
            }
           
        }

        private void KranBottom_Click(object sender, RoutedEventArgs e)
        {
            buttonClick = button.Runter;
            if (this.Kran.isRunning == false)
            {
                Thread t;

                // erster Start
                ThreadStart ts = new ThreadStart(MoveElevatorPara);
                t = new Thread(ts);
                this.Kran.isRunning = true;

                t.Start();
            }
            
        }

        private void MoveElevatorPara()//(object o)
        {

            while (this.Kran.isRunning == true)
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
                PostCraneStatusAsync();

            }

            //this.ReachedFloor.Invoke(elevatorAtFloor);
        }

        private void KranStop_Click(object sender, MouseButtonEventArgs e)
        {
            this.Kran.isRunning = false;
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
               modeControlFlag = true;
           }
           if (sender == Beobachten)
           {
               KranTop.Visibility = System.Windows.Visibility.Hidden;
               KranRechts.Visibility = System.Windows.Visibility.Hidden;
               KranLinks.Visibility = System.Windows.Visibility.Hidden;
               KranBottom.Visibility = System.Windows.Visibility.Hidden;
               Kranarm_Down.Visibility = System.Windows.Visibility.Hidden;
               Kranarm_Up.Visibility = System.Windows.Visibility.Hidden;
               modeControlFlag = false;
           }
        }

        private void KranarmStop_Click(object sender, MouseButtonEventArgs e)
        {
            this.Kran.isRunning = false;
        }

        private void Kranarm_Up_Click(object sender, RoutedEventArgs e)
        {
            if (this.Kran.isRunning == false)
            {
                Thread t;

                // erster Start
                ThreadStart ts = new ThreadStart(MoveKranarm);
                t = new Thread(ts);
                this.Kran.isRunning = true;

                t.Start();
            }
            buttonClick = button.Arm_Hoch;
        }

        private void Kranarm_Down_Click(object sender, RoutedEventArgs e)
        {
            if (this.Kran.isRunning == false)
            {
                Thread t;

                // erster Start
                ThreadStart ts = new ThreadStart(MoveKranarm);
                t = new Thread(ts);
                this.Kran.isRunning = true;

                t.Start();
            }
            buttonClick = button.Arm_Runter;
        }

        private void MoveKranarm()//(object o)
        {

            while (this.Kran.isRunning == true)
            {
                //if (Kran.KranPic.testRand() == false)
                //{
                if (buttonClick == button.Arm_Runter)
                    //AnsichtSeite.moveKranarmUnten();
                if (buttonClick == button.Arm_Hoch)
                    //AnsichtSeite.moveKranarmHoch();
                   
                
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


        public delegate void CreateKiste(double x, double y);
        private void moveKiste(Object opos)
        {
            int pos = (int)opos;

            CreateKiste handlerKran = new CreateKiste(Kran.erstelle_Kiste);
            CreateKiste handlerSeite = new CreateKiste(AnsichtSeite.erstelleKiste);

            this.Dispatcher.Invoke(handlerKran, this.kisteStartX, this.kisteStartY);
            this.Dispatcher.Invoke(handlerSeite, this.kisteStartX, this.kisteStartZ);

            Kiste draufSicht = Kran.letzteKiste;
            Kiste seitSicht = AnsichtSeite.letzteKiste;

            //Kiste draufSicht = Kran.erstelle_Kiste(this.kisteStartX, this.kisteStartY);
            //Kiste seitSicht = AnsichtSeite.erstelleKiste(this.kisteStartX, this.kisteStartZ);

            Kisten newKisten = new Kisten(seitSicht, draufSicht, KisteID);

            this.ListKisten.Add(newKisten);

            this.KisteID++;

            while (this.Kran.isRunning)
            {
                this.Kran.moveKisteTo(pos, newKisten);
                Thread.Sleep(30);
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            this.Kran.isRunning = false;

            GetBandStatusAsync();
        }

        private void NummerLagerplatz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object pos = this.NummerLagerplatz.SelectedItem;

            if (this.Kran.isRunning == false)
            {
                Thread t;

                // erster Start
                ParameterizedThreadStart pts = new ParameterizedThreadStart(moveKiste);

                t = new Thread(pts);
                this.Kran.isRunning = true;

                t.Start(pos);
            }
        }

        private void KisteAnheben_Click(object sender, RoutedEventArgs e)
        {
            //this.Kran.kistePic.kisteAufnhemen();
        }

        async Task GetBandStatusAsync()
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://10.8.0.135:53161/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("api/Band/Status");          
                if (response.IsSuccessStatusCode)
                {
                    JsonObjectBandStatus band = await response.Content.ReadAsAsync<JsonObjectBandStatus>();
                    Console.WriteLine("{0}\t${1}\t{2}\t{3}", band.Ablageplatz[0], band.An, band.Einlagerplatz[0], band.Werkstück_id);
                }
                if(response.IsSuccessStatusCode == false)
                {
                    Console.WriteLine("No Connection");

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

        async Task PostCraneStatusAsync()
        {
            using (var client = new HttpClient())
            {
                Console.WriteLine("Starting Crane Status Update");
                client.BaseAddress = new Uri("http://10.8.0.135:53161/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                

                // HTTP POST
                //HttpResponseMessage response = await client.GetAsync("api/Band/Status");             
                JsonObjectKranStatus herbert = new JsonObjectKranStatus();
                herbert.setCran(this.Kran);
                herbert.getPropertiesOfCrane();
                var gizmo = herbert.returnSelf() ;
                HttpResponseMessage response = await client.PostAsJsonAsync("api/Crane/Status", gizmo);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Updatet Crane Status");
                }

                response = await client.GetAsync("api/Crane/Status");
                if (response.IsSuccessStatusCode)
                {
                    JsonObjectKranStatus roger = await response.Content.ReadAsAsync<JsonObjectKranStatus>();
                    Console.WriteLine("{0}\t${1}\t{2}", roger.X_Pos, roger.Y_Pos, roger.isRunning);
                }
                if (response.IsSuccessStatusCode == false)
                {
                    Console.WriteLine("No Connection");

                }

            }
        }

    }
}
