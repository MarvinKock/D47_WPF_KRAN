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


        Kran kran;

        HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 1; i <= 4; i++)
                this.NummerLagerplatz.Items.Add(i);

            this.Kran.setSideView(this.AnsichtSeite);

            this.client.BaseAddress = new Uri("http://193.0.0.153:53161/");

            kran = new Kran(Kran, AnsichtSeite, 40.0, 70.0, 70.0);

            GetCranePositionOnce();
           
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
                //this.Kran.moveKisteTo(pos, newKisten);
                Thread.Sleep(30);
            }
        }



        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            this.Kran.isRunning = false;

            GetBandStatusAsync();

            //drauf.setKranPosition(300.0, 170.0);
            //seit.setKranPosition(300.0);
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
                GetCranPositionAsync();
                Thread.Sleep(300);
            }

        }

        async Task GetCranPositionAsync()
        {
            Console.WriteLine("Getting Coords of Crane");
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
                kran.setKranPosition(coords[0], coords[1]);
                Console.WriteLine("{0}\t{1}", coords[0], coords[1]);
            }
            if (response.IsSuccessStatusCode == false)
            {
                Console.WriteLine("No Connection");

            }  
        }

        private double[] getCanvasCoord(double XPos, double YPos)
        {
            double[]  coords = new double[2];

            coords[1] =  272.0 - (YPos * 1.39) ;
            coords[0] = XPos * 1.45  + 26 ;



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
                kran.setKranPosition(coords[0], coords[1]);

                Console.WriteLine("{0}\t{1}", coords[0], coords[1]);

                GetCranePosition();

            }
            if (response.IsSuccessStatusCode == false)
            {
                Console.WriteLine("No Connection");

            }
        }

        async Task GetCraneStatus()
        {
            Console.WriteLine("Getting Coords of Crane for the First Time");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // HTTP GET
            HttpResponseMessage response = await client.GetAsync("api/Crane/GetPosition");
            if (response.IsSuccessStatusCode)
            {
                JsonObjectKranStatus KranStatus = await response.Content.ReadAsAsync<JsonObjectKranStatus>();

                //if(Kranstatus.Ablageplatz[i] belegt && Ablagepltz != belegt)
                /*belgen diesen Ablageplatz 
                 * */

                /*if(Kranstatus.Ablageplatz[i] != belegt && Ablageplatz[i] == belegt
                 * entleere (hard reset) diesen Ablageplatz
                 */
                //if(Kranstatus.Registerplatz == belegt && Registerplatz != belegt)
                /*belgen diesen Registerplatz
                 * */

                /*if(Kranstatus.Registerplatz != belegt && Registerplatz == belegt
                 * entleere (hard reset) Registerplatz
                 */



            }
            if (response.IsSuccessStatusCode == false)
            {
                Console.WriteLine("No Connection");

            }

        }


        async Task PostLeftBottomPos()
        {

            Console.WriteLine("Move Left Bottom Pos");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            JsonObjectXYPos leftbottom = new JsonObjectXYPos();
            leftbottom.X_pos = 0;
            leftbottom.Y_pos = 0;

            HttpResponseMessage response = await client.PostAsJsonAsync("api/Crane/MoveToCoordinates", leftbottom);

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

        private void LeftBottom_Click(object sender, RoutedEventArgs e)
        {
            PostLeftBottomPos();
        }
       
      


    }
}
