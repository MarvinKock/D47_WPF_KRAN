using System;
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

    public delegate void removeKisteHandler(int Lager);
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
        private double kisteStartX = 523.0;
        private double kisteStartY = 186.0;
        private double kisteStartZ = 228.0;
        private int KisteID = 1;

        //Kisten active;
        //Kisten[] kistenAblageplatz = new Kisten[4] { null, null, null, null };
        //Kisten[] kistenLager = new Kisten[2] { null, null };
        //bool[] ablageplatz = new bool[4] { false, false, false, false };
        //bool[] lager = new bool[2] { false, false };

        Laufband band;

        private bool init = true;
        private bool vorherBeladen = false;


        Kran kranarm;

        HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 1; i <= 4; i++)
                this.NummerLagerplatz.Items.Add(i);

            this.Kran.setSideView(this.AnsichtSeite);

            this.client.BaseAddress = new Uri("http://localhost:53161/");

            kranarm = new Kran(Kran, AnsichtSeite, 40.0, 70.0, 70.0);
            band = new Laufband();

            GetCranePositionOnce();
            //GetCranHightAsync();

            //Console.WriteLine("{0}", ablageplatz[0]);
        }

        private void KranLinks_Click(object sender, RoutedEventArgs e)
        {
            PostCraneMoveLeft();
            //buttonClick = button.Links;
            //if (this.Kran.isRunning == false)
            //{
            //    Thread t;

            //    // erster Start
            //    ThreadStart ts = new ThreadStart(MoveElevatorPara);
            //    t = new Thread(ts);
            //    this.Kran.isRunning = true;

            //    t.Start();
            //}
            this.band.Active.kisteToPos();
           
        }


        private void KranTop_Click(object sender, RoutedEventArgs e)
        {
            PostCraneMoveForward();

            //buttonClick = button.Hoch;
            //if (this.Kran.isRunning == false)
            //{
            //    Thread t;

            //    // erster Start
            //    ThreadStart ts = new ThreadStart(MoveElevatorPara);
            //    t = new Thread(ts);
            //    this.Kran.isRunning = true;

            //    t.Start();
            //}
        
        }

        private void KranRechts_Click(object sender, RoutedEventArgs e)
        {
            PostCraneMoveRight();

            //Kisten first = ListKisten.First();
            //first.setKistenPosition(100.0, 100.0);
            //buttonClick = button.Rechts;
            //if (this.Kran.isRunning == false)
            //{
            //    Thread t;

            //    // erster Start
            //    ThreadStart ts = new ThreadStart(MoveElevatorPara);
            //    t = new Thread(ts);
            //    this.Kran.isRunning = true;

            //    t.Start();
            //}
           
        }

        private void KranBottom_Click(object sender, RoutedEventArgs e)
        {
            PostCraneMoveBackward();
            //buttonClick = button.Runter;
            //if (this.Kran.isRunning == false)
            //{
            //    Thread t;

            //    // erster Start
            //    ThreadStart ts = new ThreadStart(MoveElevatorPara);
            //    t = new Thread(ts);
            //    this.Kran.isRunning = true;

            //    t.Start();
            //}
            
        }

        //private void MoveElevatorPara()//(object o)
        //{

        //    while (this.Kran.isRunning == true)
        //    {
        //        //if (Kran.KranPic.testRand() == false)
        //        //{
        //        if (buttonClick == button.Links)
        //        {
        //            Kran.moveKranLeft();
                    
        //        }
                    
        //        if (buttonClick == button.Rechts)
        //        {
        //            Kran.moveKranRechts();
                    
        //        }
                    
        //        if (buttonClick == button.Hoch)
        //        {
        //            Kran.moveKranHoch();
                    
        //        }
                    
        //        if (buttonClick == button.Runter)
        //        {
        //            Kran.moveKranRunter();
                    
        //        }
                   
        //        // Console.WriteLine("da");
        //        Thread.Sleep(30);
              

        //    }

            //this.ReachedFloor.Invoke(elevatorAtFloor);
        //}

        private void KranStop_Click(object sender, MouseButtonEventArgs e)
        {
            this.Kran.isRunning = false;

            PostCraneMoveStop();
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
            if (kranarm.InPosition)
            {
                PostCraneMoveBottom();
                this.kranarm.movekranarmUnten();
            }
            //if (this.Kran.isRunning == false)
            //{
            //    Thread t;

            //    // erster Start
            //    ThreadStart ts = new ThreadStart(MoveKranarm);
            //    t = new Thread(ts);
            //    this.Kran.isRunning = true;

            //    t.Start();
            //}
            //buttonClick = button.Arm_Runter;
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
    
        private void erstelleKiste()
        {
            Kisten kisten = new Kisten(this.Kran, this.AnsichtSeite, this.kisteStartX, this.kisteStartY, this.kisteStartZ, 0);
            this.ListKisten.Add(kisten);
        }

        //public delegate void CreateKiste(double x, double y);
        private void moveKiste(Object opos)
        {
            int pos = (int)opos;

            //CreateKiste handlerKran = new CreateKiste(Kran.erstelle_Kiste);
            //CreateKiste handlerSeite = new CreateKiste(AnsichtSeite.erstelleKiste);

            //this.Dispatcher.Invoke(handlerKran, this.kisteStartX, this.kisteStartY);
            //this.Dispatcher.Invoke(handlerSeite, this.kisteStartX, this.kisteStartZ);

            //Kiste draufSicht = Kran.letzteKiste;
            //Kiste seitSicht = AnsichtSeite.letzteKiste;

            //Kiste draufSicht = Kran.erstelle_Kiste(this.kisteStartX, this.kisteStartY);
            //Kiste seitSicht = AnsichtSeite.erstelleKiste(this.kisteStartX, this.kisteStartZ);

            //Kisten newKisten = new Kisten(seitSicht, draufSicht, KisteID);

            //this.ListKisten.Add(newKisten);

            //this.KisteID++;

            while (this.Kran.isRunning)
            {
                //this.Kran.moveKisteTo(pos, newKisten);
                Thread.Sleep(30);
            }
        }



        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            this.Kran.isRunning = false;

            //GetBandStatusAsync();

            Kisten k1 = new Kisten(this.Kran, this.AnsichtSeite, this.kisteStartX, this.kisteStartY, this.kisteStartZ, 3);
            k1.kisteToPos();

            //erstelleKiste();

            //drauf.setKranPosition(300.0, 170.0);
            //seit.setKranPosition(300.0);
        }

        private void NummerLagerplatz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //object pos = this.NummerLagerplatz.SelectedItem;

            //if (this.Kran.isRunning == false)
            //{
            //    Thread t;

            //    // erster Start
            //    ParameterizedThreadStart pts = new ParameterizedThreadStart(moveKiste);

            //    t = new Thread(pts);
            //    this.Kran.isRunning = true;

            //    t.Start(pos);
            //}
        }

        private void KisteAnheben_Click(object sender, RoutedEventArgs e)
        {
            //this.Kran.kistePic.kisteAufnhemen();
        }

        async Task GetBandStatusAsync()
        {
                           
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("api/Band/Status");          
                if (response.IsSuccessStatusCode)
                {
                    JsonObjectBandStatus band = await response.Content.ReadAsAsync<JsonObjectBandStatus>();
                    
                    for(int i = 0; i < 4; i++)
                    {
                        
                        if(band.Ablageplatz[i] == true && this.band.LagerBelegt[i] == false)
                        {
                            if(init)
                            {
                                this.band.LagerBelegt[i] = band.Ablageplatz[i];
                                Kisten kisteLager = erstelleKisteInLager(i + 1, 1);
                                this.band.KistenAblageplatz[i] = kisteLager;
                            }
                            else
                            {
                                this.band.Active.moveKistetoLager(this.band.Active.KisteID);
                                this.band.KistenAblageplatz[i] = this.band.Active;
                                this.band.LagerBelegt[i] = true;
                                this.band.Active = null;
                            }
                        }
                        if (i < 2)
                        {
                           
                            if (band.Einlagerplatz[i] == true && this.band.ZwischenlagerBelegt[i] == false)
                            {

                                if(init)
                                {
                                    Kisten kisteLager = erstelleKisteInLager(i + 4, 1); 
                                    this.band.KistenLager[i] = kisteLager;
                                    this.band.ZwischenlagerBelegt[i] = band.Einlagerplatz[i];
                                }
                                else
                                {
                                    this.band.KistenAblageplatz[i] = this.kranarm.KisteKran;
                                    this.kranarm.KisteKran = null;
                                    this.band.ZwischenlagerBelegt[i] = true;
                                }
                                
                               
                                
                            }
                        }
                        if(i < 3)
                        {
                            this.band.BeroPuscher[i] = band.Schieber[i];

                        }
                    }

                    this.band.TurmBelegt = band.Registerlager;
                    if(this.band.TurmBelegt)
                    {
                        Kisten kistenTurm = erstelleKisteInRegiter(0);
                        this.band.KisteTurm = kistenTurm;
                    }

                    if(band.Anfangssensor)
                    {
                        this.band.Active = this.band.KisteTurm;
                        this.band.Active.moveLeftTillStop();
                    }
                    if(band.WerkstueckID != 0 && this.band.Active.KisteID != band.WerkstueckID)
                    {
                        this.band.Active.KisteID = band.WerkstueckID;
                        this.band.Active.kisteToPos();
                    }
                        
                    this.band.BandAn = band.An;
                    this.init = false;
                    

                  //  Console.WriteLine("{0} {1} {2} {3} {4} {5}", band.Ablageplatz[0], band.Ablageplatz[1], band.Ablageplatz[2], band.Ablageplatz[3], band.Einlagerplatz[0], band.Einlagerplatz[1]);

                }
                if(response.IsSuccessStatusCode == false)
                {
                    Console.WriteLine("No Connection");

                }
        }

        private void GetCranePositionOnce()
        {
            GetCranPositionAsyncFirst();
        }

        private void GetCranePosition()
        {
            Thread t;

            // erster Start
            ThreadStart ts = new ThreadStart(GetCranePositionThread);
            t = new Thread(ts);
           

            t.Start();
        }

        private void GetCranePositionThread()
        {
           
            while (true)
            {
                GetCraneStatus().Wait();
                //GetCranPositionAsync(); ersetzt durch CraneStatus
                //GetCranHightAsync().Wait();
                GetBandStatusAsync().Wait();
                Thread.Sleep(300);
            }

        }

        async Task GetCranPositionAsync()
        {
            //Console.WriteLine("Getting Coords of Crane");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // HTTP GET
            HttpResponseMessage response = await client.GetAsync("api/Crane/GetPosition");
            if (response.IsSuccessStatusCode)
            {
                JsonObjectXYPos coord = await response.Content.ReadAsAsync<JsonObjectXYPos>();
                Console.WriteLine("{0}\t{1}", coord.X_pos, coord.Y_pos);
                double[] coords = new double[2];
                coords = getCanvasCoord(coord.X_pos, coord.Y_pos);
                kranarm.setKranPosition(coords[0], coords[1]);
                Console.WriteLine("{0}\t{1}", coords[0], coords[1]);
            }
            if (response.IsSuccessStatusCode == false)
            {
                Console.WriteLine("No Connection");

            }  
        }


        //async Task GetCranHightAsync()
        //{
        //    Console.WriteLine("Getting Hight of Cran");
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    // HTTP GET
        //    HttpResponseMessage response = await client.GetAsync("api/Crane/StatusSensors");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        JsonObjectStatusSensors status = await response.Content.ReadAsAsync<JsonObjectStatusSensors>();
        //      //  Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", status.X_min_pos, status.X_max_pos, status.Y_min_pos, status.Y_max_pos, status.Greifer_oben, status.Greifer_unten);

        //        if (status.Greifer_oben)
        //        {
        //            //kranarm.setKranarmOben();
        //            KranTop.Visibility = System.Windows.Visibility.Visible;
        //            KranRechts.Visibility = System.Windows.Visibility.Visible;
        //            KranLinks.Visibility = System.Windows.Visibility.Visible;
        //            KranBottom.Visibility = System.Windows.Visibility.Visible;
        //        }
        //        else if (status.Greifer_unten)
        //        {
        //           // kranarm.setKranarmUnten();
        //            KranTop.Visibility = System.Windows.Visibility.Hidden;
        //            KranRechts.Visibility = System.Windows.Visibility.Hidden;
        //            KranLinks.Visibility = System.Windows.Visibility.Hidden;
        //            KranBottom.Visibility = System.Windows.Visibility.Hidden;
        //            if (this.kranarm.KisteKran == null)
        //            {
        //                //this.kranarm.KisteKran = this.band.Active;
        //                if (this.kranarm.UeberLager > 0 && this.kranarm.UeberLager < 5)
        //                    this.kranarm.KisteKran = this.band.KistenAblageplatz[this.kranarm.UeberLager - 1];
        //                else if (this.kranarm.UeberLager > 4)
        //                    this.kranarm.KisteKran = this.band.KistenLager[this.kranarm.UeberLager - 5];
        //            }
        //            else
        //                this.kranarm.KisteKran = null;
        //        }
        //        //Console.WriteLine("{0}\t{1}", coords[0], coords[1]);
        //    }
        //    if (response.IsSuccessStatusCode == false)
        //    {
        //        Console.WriteLine("No Connection");

        //    }
        //}

        private double[] getCanvasCoord(double XPos, double YPos)
        {
            double[]  coords = new double[2];

            coords[1] = 26 + 249.0 - (YPos * 1.3989) ;
            coords[0] = XPos * 1.3989  + 26 ;



            return coords;
        }

        //async Task PostCraneStatusAsync()
        //{
            
        //        Console.WriteLine("Starting Crane Status Update");
               
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                

        //        // HTTP POST
        //        //HttpResponseMessage response = await client.GetAsync("api/Band/Status");             
        //        JsonObjectKranStatus herbert = new JsonObjectKranStatus();
        //        herbert.setCran(this.Kran);
        //        herbert.getPropertiesOfCrane();
        //        HttpResponseMessage response = await client.PostAsJsonAsync("api/Crane/Set", herbert);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            Console.WriteLine("Updatet Crane Status");
        //        }

        //        response = await client.GetAsync("api/Crane/Status");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            JsonObjectKranStatus roger = await response.Content.ReadAsAsync<JsonObjectKranStatus>();
        //            Console.WriteLine("{0}\t${1}\t{2}", roger.X_Pos, roger.Y_Pos, roger.IsRunning);
        //        }
        //        if (response.IsSuccessStatusCode == false)
        //        {
        //            Console.WriteLine("No Connection");

        //        }

            
        //}

        async Task PostCraneMoveLeft()
        {
                Console.WriteLine("Move Left");               
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                JsonObjectMoveCrane left = new JsonObjectMoveCrane("left");

                HttpResponseMessage response = await client.PostAsJsonAsync("api/Crane/Move", left);

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
            
        }



        async Task PostCraneMoveRight()
        {
            
                Console.WriteLine("Move Right");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                JsonObjectMoveCrane right = new JsonObjectMoveCrane("right");

                HttpResponseMessage response = await client.PostAsJsonAsync("api/Crane/Move", right);

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
            
        }

        async Task PostCraneMoveForward()
        {
            Console.WriteLine("Move Forward");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            JsonObjectMoveCrane forward = new JsonObjectMoveCrane("forward");

            HttpResponseMessage response = await client.PostAsJsonAsync("api/Crane/Move", forward);

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
        }

        async Task PostCraneMoveBackward()
        {
            Console.WriteLine("Move Backward");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            JsonObjectMoveCrane backward = new JsonObjectMoveCrane("backward");

            HttpResponseMessage response = await client.PostAsJsonAsync("api/Crane/Move", backward);

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
        }


        async Task PostCraneMoveStop()
        {
            Console.WriteLine("Move Stop");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            JsonObjectMoveCrane stop = new JsonObjectMoveCrane("stop");

            HttpResponseMessage response = await client.PostAsJsonAsync("api/Crane/Move", stop);

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
        }

        async Task GetCranPositionAsyncFirst()
        {
            Console.WriteLine("Getting Coords of Crane for the First Time");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // HTTP GET
            HttpResponseMessage response = await client.GetAsync("api/Crane/GetPosition");
            if (response.IsSuccessStatusCode)
            {
                JsonObjectXYPos coord = await response.Content.ReadAsAsync<JsonObjectXYPos>();
                
                double[] coords = new double[2];
                coords = getCanvasCoord(coord.X_pos, coord.Y_pos);
                kranarm.setKranPosition(coords[0], coords[1]);

                Console.WriteLine("{0}\t{1}", coords[0], coords[1]);

                GetCranePosition();

            }
            if (response.IsSuccessStatusCode == false)
            {
                Console.WriteLine("No Connection");

            }
        }

        async Task PostCraneMoveBottom()
        {
            Console.WriteLine("Move Bottom");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync("api/Crane/KranRunter", true);

            if (response.IsSuccessStatusCode)
            {
                String result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
                Console.WriteLine("PostAsJsonAsync: {0}", response.StatusCode.ToString());


                //KranTop.Visibility = System.Windows.Visibility.Hidden;
                //KranRechts.Visibility = System.Windows.Visibility.Hidden;
                //KranLinks.Visibility = System.Windows.Visibility.Hidden;
                //KranBottom.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                Console.WriteLine("PostAsJsonAsync Error: {0} [{1}]",
                     response.StatusCode.ToString(), (int)response.StatusCode);
            }


        }

        async Task GetCraneStatus()
        {
           // Console.WriteLine("Getting Coords of Crane ");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // HTTP GET
            HttpResponseMessage response = await client.GetAsync("api/Crane/Status");
            if (response.IsSuccessStatusCode)
            {
                JsonObjectKranStatus KranStatus = await response.Content.ReadAsAsync<JsonObjectKranStatus>();

                 
                double[] coords = new double[2];
                coords = getCanvasCoord(KranStatus.X_Pos, KranStatus.Y_Pos);                            
                kranarm.setKranPosition(coords[0], coords[1]);
               //Console.WriteLine("{0}\t{1}", coords[0], coords[1]);

                if(KranStatus.Oben)
                {
                    this.kranarm.setKranarmOben();
                    vorherBeladen = false;
                }

               int KranUeberLager = kranarm.isUeberLager();

                Console.WriteLine("{0}", KranUeberLager);

               if(KranStatus.Beladen == true && kranarm.KisteKran == null)
               {

                   //removeKisteInSeitAnsicht(KranUeberLager);
                   removeKisteInDraufsicht(KranUeberLager);                  
                   //this.band.LagerBelegt[KranUeberLager - 2] = false ;
                   this.kranarm.KisteKran = new Kisten(this.Kran, this.AnsichtSeite, 50.0,50.0,50.0,2);
                   //this.band.KistenAblageplatz[KranUeberLager - 2] = null;


                   
                   //this.AnsichtSeite,

               }
               else
               {
                   //kranarm.KisteKran 
               }

                if(KranStatus.Oben == false)
                {
                    kranarm.setKranarmUnten();
                }
                else
                {
                    kranarm.setKranarmOben();
                }

                //Console.WriteLine("HOehe:{0}", KranStatus.Oben);
                Console.WriteLine("Beladen:{0}", KranStatus.Beladen);

                if(kranarm.KisteKran != null)
                {
                    kranarm.KisteKran.setKistenPosition(kranarm.XKoordinate - 12.0, kranarm.YKoordinate - 12.0);
                    kranarm.KisteKran.setKisteHoehe(kranarm.ZKoordinate);
                }


               if(KranStatus.Beladen && !KranStatus.Oben && !vorherBeladen)
               {
                   this.kranarm.movekranarmOben();
                   vorherBeladen = true;
               }
               if(KranStatus.Oben)
               {
                   this.kranarm.setKranarmOben();
                   vorherBeladen = false;
               }



            }
            else
            {
                Console.WriteLine("PostAsJsonAsync Error: {0} [{1}]",
                     response.StatusCode.ToString(), (int)response.StatusCode);

            }

        }


        async Task MoveToPos(int pos)
        {

            Console.WriteLine("Move Left Bottom Pos");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            JsonObjectPosition position = new JsonObjectPosition(pos);

		  HttpResponseMessage response = await client.PostAsJsonAsync("api/Crane/MoveToPlatz", position);

            if (response.IsSuccessStatusCode)
            {
                String result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
                Console.WriteLine("PostAsJsonAsync: {0}", response.StatusCode.ToString());

                kranarm.InPosition = true;
                this.kranarm.UeberLager = pos;
            }
            else
            {
                Console.WriteLine("PostAsJsonAsync Error: {0} [{1}]",
                     response.StatusCode.ToString(), (int)response.StatusCode);
            }
        }

        private void MoveToPosClick(object sender, RoutedEventArgs e)
        {
		   if(sender == Lager1)
		   {
			   MoveToPos(5);
		   }
		   if (sender == Lager2)
		   {
			   MoveToPos(6);
		   }
		   if (sender == Bandlager1)
		   {
			   MoveToPos(1);
		   }
		   if (sender == Bandlager2)
		   {
			   MoveToPos(2);
		   }
		   if (sender == Bandlager3)
		   {
			   MoveToPos(3);
		   }
		   if (sender == Bandlager4)
		   {
			   MoveToPos(4);
		   }
		   if (sender == Register)
		   {
			   MoveToPos(0);
		   }
        }

        private Kisten erstelleKisteInLager(int Lager, int id)
        {
            Kisten kisten;
            if (Lager == 1)
            {
                kisten = new Kisten(this.Kran, this.AnsichtSeite, 0, 0, 0, id);
                kisten.moveKistetoLager(Lager);
                return kisten;
                //KistenAblageplatz[Lager] = kisten;               
            }
            if (Lager == 2)
            {
                kisten = new Kisten(this.Kran, this.AnsichtSeite, 0, 0, 0, id);
                kisten.moveKistetoLager(Lager);
                return kisten;
            }
            if (Lager == 3)
            {
                kisten = new Kisten(this.Kran, this.AnsichtSeite, 0, 0, 0, id);
                kisten.moveKistetoLager(Lager);
                return kisten;
            }
            if (Lager == 4)
            {
                kisten = new Kisten(this.Kran, this.AnsichtSeite, 0, 0, 0, id);
                kisten.moveKistetoLager(Lager);
                return kisten;
            }
            if (Lager == 5)
            {
                kisten = new Kisten(this.Kran, this.AnsichtSeite, 0, 0, 0, id);
                kisten.moveKistetoLager(Lager);
                return kisten;
            }
            if (Lager == 6)
            {
                kisten = new Kisten(this.Kran, this.AnsichtSeite, 0, 0, 0, id);
                kisten.moveKistetoLager(Lager);
                return kisten;
            }
            return null;
        }

        private Kisten erstelleKisteInRegiter(int id)
        {
            Kisten kisten = new Kisten(this.Kran, this.AnsichtSeite, this.kisteStartX, this.kisteStartY, this.kisteStartZ, 0);
            return kisten;
        }

        private void KisteZuLagerplatz_Click(object sender, RoutedEventArgs e)
        {
            PostMoveKisteToPos();
        }

        async Task PostMoveKisteToPos()
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync("api/Band/SendoneCrate", true);

            if (response.IsSuccessStatusCode)
            {
                String result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
                Console.WriteLine("PostAsJsonAsync: {0}", response.StatusCode.ToString());

                if(this.band.KisteTurm != null)
                {
                    this.band.Active = this.band.KisteTurm;
                    this.band.Active.KisteID = 3;
                    this.band.Active.kisteToPos();
                }
                
            }
            else
            {
                Console.WriteLine("PostAsJsonAsync Error: {0} [{1}]",
                     response.StatusCode.ToString(), (int)response.StatusCode);
            }

        }

        public void removeKisteInDraufsicht(int Pos)
        {
            if(Kran.Dispatcher.CheckAccess())
            {
                Console.WriteLine("try loeschen ......................................................");
                Rectangle rec;
                
                   try{
                        if(Pos >= 1 && Pos <= 4)
                            rec = this.band.KistenAblageplatz[Pos - 1].getRectangleDraufsicht();
                        else if (Pos == 7)
                            rec = this.kranarm.KisteKran.getRectangleDraufsicht();
                        else rec = null;

                       
                    }
                    catch 
                    {
                        rec = null;

                        Console.WriteLine("Loeschen ---------------------------------------------------------- No");
                    }

                    if (rec != null)
                    {
                        Kran.Children.Remove(rec);
                        Console.WriteLine("Loeschen ----------------------------------------------------------");
                    }
            }
            else
            {
                removeKisteHandler handler =
                         new removeKisteHandler(this.removeKisteInDraufsicht);
                this.Kran.Dispatcher.BeginInvoke(handler, Pos);
            }
            
            
         
        }

        public void removeKisteInSeitAnsicht(int Pos)
        {

            if (AnsichtSeite.Dispatcher.CheckAccess())
            {
                Console.WriteLine("try loeschen ......................................................");
                Rectangle rec;

                try
                {
                    if (Pos >= 1 && Pos <= 4)
                        rec = this.band.KistenAblageplatz[Pos - 1].getRectangleSeitsicht();
                    else if (Pos == 7)
                        rec = this.kranarm.KisteKran.getRectangleDraufsicht();
                    else rec = null;


                }
                catch
                {
                    rec = null;

                    Console.WriteLine("Loeschen ---------------------------------------------------------- No");
                }

                if (rec != null)
                {
                    Kran.Children.Remove(rec);
                    Console.WriteLine("Loeschen ----------------------------------------------------------");
                }
            }
            else
            {
                removeKisteHandler handler =
                         new removeKisteHandler(this.removeKisteInDraufsicht);
                this.Kran.Dispatcher.BeginInvoke(handler, Pos);
            }
        }

    }
}
