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
        public bool isRunning = false;
        private button buttonClick;
        bool? modeControlFlag = null;
        KranSync kranSync = new KranSync();

        private List<Kisten> ListKisten = new List<Kisten>();
        private double kisteStartX = 455.0;
        private double kisteStartY = 123.0;
        private double kisteStartZ = 228.0;
        private int KisteID = 1;

        bool getCraneStatus = false;
        bool getBandStatus = false;
        bool postCraneStatus = false;
        bool postBandStatus = false;

        bool delivered = false;

        private HttpClient httpClient;

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 1; i <= 4; i++)
                this.NummerLagerplatz.Items.Add(i);

            this.Kran.setSideView(this.AnsichtSeite);

            this.httpClient = new HttpClient();
            // due to Fiddler: use machine name            
            this.httpClient.BaseAddress = new Uri("http://10.8.0.135:53161/");
           
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
                PostCraneStatus();
                
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

            if (delivered == true)
            {
                postCraneStatus = false;
            }
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

        private void PostCraneStatus()
        {
            if (postCraneStatus == false)
            {
                Thread t;

                // erster Start
                ThreadStart ts = new ThreadStart(PostCraneStatusThread);
                t = new Thread(ts);
                postCraneStatus = true;

                t.Start();
            }
        }

        private void PostCraneStatusThread()
        {
            while (postCraneStatus == true)
            {
                
                 PostCraneStatusAsync();

                Thread.Sleep(300);

                if(isRunning == false)
                {
                    postCraneStatus = false;
                }
            }

            //getCraneStatus = false;
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

            while (this.isRunning)
            {
                this.Kran.moveKisteTo(pos, newKisten);
                Thread.Sleep(30);
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            isRunning = false;

            GetBandStatusAsync();

            //PostCraneStatusAsync();
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
            //this.Kran.kistePic.kisteAufnhemen();
        }

        async Task GetBandStatusAsync()
        {
           

                //client.BaseAddress = new Uri("http://10.8.0.135:53161/");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await httpClient.GetAsync("api/Band/Status");          
                if (response.IsSuccessStatusCode)
                {
                    Band band = await response.Content.ReadAsAsync<Band>();
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

        async Task PostCraneStatusAsync()
        {
                delivered = false;
                Console.WriteLine("Starting Crane Status Update");
                //httpClient.BaseAddress = new Uri("http://10.8.0.135:53161/");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                

                // HTTP POST
                //HttpResponseMessage response = await client.GetAsync("api/Band/Status");             
                KranSync herbert = new KranSync();
                herbert.setCran(this.Kran);
                herbert.getPropertiesOfCrane();
                HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/Crane/Set", herbert);
                if (response.IsSuccessStatusCode)
                {
                    String result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(result);
                    Console.WriteLine("PostAsJsonAsync: {0}", response.StatusCode.ToString());
                }
                else
                {
                    Console.WriteLine("PostAsJsonAsync Error: {0} [{1}]",
                        response.StatusCode.ToString(), (int)response.StatusCode);
                }



                /*if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Updatet Crane Status");
                }*/

                

                response = await httpClient.GetAsync("api/Crane/Status");
                if (response.IsSuccessStatusCode)
                {
                    KranSync roger = await response.Content.ReadAsAsync<KranSync>();
                    Console.WriteLine("{0}\t${1}\t{2}", roger.X_Pos, roger.Y_Pos, roger.isRunning);
                }
                if (response.IsSuccessStatusCode == false)
                {
                    Console.WriteLine("No Connection");

                }


                delivered = true;
               

            
        }

    }
}
