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
	public delegate void UiUpdateHandler();
    
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public bool isRunning = false;
        
        bool? modeControlFlag = null;
        JsonObjectKranStatus kranSync = new JsonObjectKranStatus();


        private List<Kisten> ListKisten = new List<Kisten>();
        private double kisteStartX = 523.0;
        private double kisteStartY = 186.0;
        private double kisteStartZ = 228.0;
       // private int KisteID = 1;

        Laufband band;

        private bool init = true;
        private bool vorherBeladen = false;

        bool connection = false;


        Kran kranarm;

        HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();

            this.Kran.setSideView(this.AnsichtSeite);

            //this.client.BaseAddress = new Uri("http://10.8.0.135:53161/");
            this.client.BaseAddress = new Uri("http://localhost:53161/");

            client.Timeout = TimeSpan.FromSeconds(2);


            band = new Laufband();
            kranarm = new Kran(Kran, AnsichtSeite, 40.0, 70.0, 70.0);
            

            GetCranePositionOnce();

            this.kranarm.kranSeite.fertig += this.ArrivedAtBottom;

            //GetCranHightAsync();

            //Console.WriteLine("{0}", ablageplatz[0]);
        }

        // --------------- Usercontrol

        private void KranLinks_Click(object sender, RoutedEventArgs e)
        {
            PostCraneMoveLeft();
        }

        private void KranTop_Click(object sender, RoutedEventArgs e)
        {
            PostCraneMoveForward();
        }

        private void KranRechts_Click(object sender, RoutedEventArgs e)
        {
            PostCraneMoveRight();
        }

        private void KranBottom_Click(object sender, RoutedEventArgs e)
        {
            PostCraneMoveBackward();
        }

        private void KranStop_Click(object sender, MouseButtonEventArgs e)
        {
            this.Kran.isRunning = false;

            PostCraneMoveStop();
        }

        private void RadioButton_Changed(object sender, RoutedEventArgs e)
        {
            if (sender == Manuell)
            {
                KranTop.Visibility = System.Windows.Visibility.Visible;
                KranRechts.Visibility = System.Windows.Visibility.Visible;
                KranLinks.Visibility = System.Windows.Visibility.Visible;
                KranBottom.Visibility = System.Windows.Visibility.Visible;
                Kranarm_Down.Visibility = System.Windows.Visibility.Visible;
                Lager1.Visibility = System.Windows.Visibility.Visible;
                Lager2.Visibility = System.Windows.Visibility.Visible;
                Bandlager4.Visibility = System.Windows.Visibility.Visible;
                Bandlager3.Visibility = System.Windows.Visibility.Visible;
                Bandlager2.Visibility = System.Windows.Visibility.Visible;
                Bandlager1.Visibility = System.Windows.Visibility.Visible;
                Register.Visibility = System.Windows.Visibility.Visible;
                KisteZuLagerplatz.Visibility = System.Windows.Visibility.Visible;
                modeControlFlag = true;
                PostAutomatikAus();
            }
            if (sender == Beobachten)
            {
                KranTop.Visibility = System.Windows.Visibility.Hidden;
                KranRechts.Visibility = System.Windows.Visibility.Hidden;
                KranLinks.Visibility = System.Windows.Visibility.Hidden;
                KranBottom.Visibility = System.Windows.Visibility.Hidden;
                Kranarm_Down.Visibility = System.Windows.Visibility.Hidden;
                Lager1.Visibility = System.Windows.Visibility.Hidden;
                Lager2.Visibility = System.Windows.Visibility.Hidden;
                Bandlager4.Visibility = System.Windows.Visibility.Hidden;
                Bandlager3.Visibility = System.Windows.Visibility.Hidden;
                Bandlager2.Visibility = System.Windows.Visibility.Hidden;
                Bandlager1.Visibility = System.Windows.Visibility.Hidden;
                Register.Visibility = System.Windows.Visibility.Hidden;
                KisteZuLagerplatz.Visibility = System.Windows.Visibility.Hidden;
                modeControlFlag = false;
                PostAutomatikAn();
            }
        }

        private void KranarmStop_Click(object sender, MouseButtonEventArgs e)
        {
            this.Kran.isRunning = false;
        }

        private void Kranarm_Down_Click(object sender, RoutedEventArgs e)
        {
            if (kranarm.InPosition && kranarm.armMoving() == false)
            {
                PostCraneMoveBottom();
                
            }
        }


        // --------------- Logik

        public void ArrivedAtBottom()
        {
            if (kranarm.KisteKran == null)
            {
                if (kranarm.UeberLager == 0)
                {
                    Kisten kiste = new Kisten(this.Kran, this.AnsichtSeite, this.kranarm.XKoordinate, this.kranarm.YKoordinate, this.kranarm.ZKoordinate, 0);
                    this.kranarm.KisteKran = kiste;
                }
                else if (kranarm.UeberLager >= 1 && kranarm.UeberLager <= 4)
                {
                    kranarm.KisteKran = band.KistenAblageplatz[kranarm.UeberLager - 1];
                    band.LagerBelegt[kranarm.UeberLager - 1] = false; ;
                    band.KistenAblageplatz[kranarm.UeberLager - 1] = null;
                }
                else if (kranarm.UeberLager == 5 || kranarm.UeberLager == 6)
                {
                    kranarm.KisteKran = band.KistenLager[kranarm.UeberLager - 5];
                    band.ZwischenlagerBelegt[kranarm.UeberLager - 5] = false;
                    band.KistenLager[kranarm.UeberLager - 5] = null;
                }
            }
            else
            {
                if (kranarm.UeberLager == 0)
                {
                    removeKisteInDraufsicht(7);
                }
                else if (kranarm.UeberLager >= 1 && kranarm.UeberLager <= 4)
                {
                    band.KistenAblageplatz[kranarm.UeberLager - 1] = kranarm.KisteKran;
                    band.LagerBelegt[kranarm.UeberLager - 1] = true;
                    band.KistenAblageplatz[kranarm.UeberLager - 1].setKisteHoehe(this.kisteStartZ);
                    kranarm.KisteKran = null;
                }
                else if (kranarm.UeberLager == 5 || kranarm.UeberLager == 6)
                {
                    band.KistenLager[kranarm.UeberLager - 5] = kranarm.KisteKran;
                    band.ZwischenlagerBelegt[kranarm.UeberLager - 5] = true;
                    band.KistenLager[kranarm.UeberLager - 5].setKisteHoehe(this.kisteStartZ);
                    kranarm.KisteKran = null;
                }
            }

        }

        private void erstelleKiste()
        {
            Kisten kisten = new Kisten(this.Kran, this.AnsichtSeite, this.kisteStartX, this.kisteStartY, this.kisteStartZ, 0);
            this.ListKisten.Add(kisten);
        }

        private void GetCranePositionOnce()
        {
            
            connection =  GetCranPositionAsyncFirst().Wait(3000);

            if(connection == false)
            {
                GetCranePosition();
            }
            
           
        }

        private void GetCranePosition()
        {
            Thread t;

            ThreadStart ts = new ThreadStart(GetCranePositionThread);
            t = new Thread(ts);


            t.Start();
        }

        private void GetCranePositionThread()
        {
            while (connection)
            {
                try
                {
                    GetCraneStatus().Wait(3000);
                    GetBandStatusAsync().Wait(3000);
                    UpdatingUiElements();
                    Thread.Sleep(250);
                    
                }
                catch (AggregateException Ae)
                {
                    connection = false;
                }
                
            }
            while(!connection)
            {
                try
                {
                    
                    connection = GetCranPositionAsyncFirst().Wait(3000);
                    
                }
                catch (AggregateException Ae)
                {
                    UpdatingUiElements();
                }
                Thread.Sleep(1000);
            }
        }

        private double[] getCanvasCoord(double XPos, double YPos)
        {
            double[] coords = new double[2];

            coords[1] = 26 + 249.0 - (YPos * 1.3989);
            coords[0] = XPos * 1.3989 + 26;

            return coords;
        }

        public void removeKisteInDraufsicht(int Pos)
        {
            if (Kran.Dispatcher.CheckAccess())
            {
                Rectangle rec;
                Rectangle recS;

                try
                {
                    if (Pos >= 1 && Pos <= 4)
                    {
                        rec = this.band.KistenAblageplatz[Pos - 1].getRectangleDraufsicht();
                        recS = this.band.KistenAblageplatz[Pos - 1].getRectangleSeitsicht();
                    }
                    else if (Pos == 7)
                    {
                        rec = this.kranarm.KisteKran.getRectangleDraufsicht();
                        recS = this.kranarm.KisteKran.getRectangleSeitsicht();
                    }

                    else if (Pos >= 5 && Pos <= 6)
                    {
                        rec = this.band.KistenLager[Pos - 5].getRectangleDraufsicht();
                        recS = this.band.KistenLager[Pos - 5].getRectangleSeitsicht();
                    }
                    else
                    {
                        rec = null;
                        recS = null;
                    }
                }
                catch
                {
                    rec = null;
                    recS = null;
                }

                if (rec != null)
                {
                    Kran.Children.Remove(rec);
                    AnsichtSeite.Children.Remove(recS);
                    kranarm.KisteKran = null;
                    rec = null;
                    recS = null;
                }
            }
            else
            {
                removeKisteHandler handler =
                         new removeKisteHandler(this.removeKisteInDraufsicht);
                this.Kran.Dispatcher.BeginInvoke(handler, Pos);
            }
        }

        private void KisteZuLagerplatz_Click(object sender, RoutedEventArgs e)
        {
            if (!this.Kran.BewegteKiste)
                PostMoveKisteToPos();
        }

        private void MoveToPosClick(object sender, RoutedEventArgs e)
        {
		   if(kranarm.armMoving() == false)
		   { 
			  if (sender == Lager1)
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
        }

        private Kisten erstelleKisteInLager(int Lager, int id)
        {
            Kisten kisten;
            if (Lager == 1)
            {
                kisten = new Kisten(this.Kran, this.AnsichtSeite, 0, 0, 0, id);
                kisten.moveKistetoLager(Lager);
                return kisten;
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

        private Kisten erstelleKisteVorPuscher(int puscher)
        {
            Kisten kisten;

            if (puscher == 1)
            {
                kisten = new Kisten(this.Kran, this.AnsichtSeite, 0, 0, 0, 0);
                kisten.setKisteToPusher(puscher);
                return kisten;
            }
            if (puscher == 2)
            {
                kisten = new Kisten(this.Kran, this.AnsichtSeite, 0, 0, 0, 0);
                kisten.setKisteToPusher(puscher);
                return kisten;
            }
            if (puscher == 3)
            {
                kisten = new Kisten(this.Kran, this.AnsichtSeite, 0, 0, 0, 0);
                kisten.setKisteToPusher(puscher);
                return kisten;
            }
            return null;
        }


        // --------------- GET

        async Task GetBandStatusAsync()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = await client.GetAsync("api/Band/Status");
                if (response.IsSuccessStatusCode)
                {
                    JsonObjectBandStatus band = await response.Content.ReadAsAsync<JsonObjectBandStatus>();

                    for (int i = 0; i < 4; i++)
                    {

                        if (band.Ablageplatz[i] == true && this.band.LagerBelegt[i] == false && !this.kranarm.armMoving())
                        {
                            if (init)
                            {
                                this.band.LagerBelegt[i] = band.Ablageplatz[i];
                                Kisten kisteLager = erstelleKisteInLager(i + 1, 1);
                                this.band.KistenAblageplatz[i] = kisteLager;
                            }
                            else
                            {
                                if (this.band.Active == null)
                                {
                                    this.band.KisteTurm.KisteID = i + 1;
                                    this.band.KisteTurm.kisteToPos();
                                    this.band.LagerBelegt[i] = true;
                                    this.band.KistenAblageplatz[i] = this.band.KisteTurm;
                                    this.band.KisteTurm = null;
                                    this.band.TurmBelegt = false;
                                }
                                else
                                {
                                    this.band.Active.KisteID = i + 1;
                                    Kisten kisteToLager = this.band.Active;
                                    this.band.KistenAblageplatz[i] = kisteToLager;
                                    this.band.LagerBelegt[i] = true;
                                    this.band.Active = null;
                                    //Setzt Kiste in Lager no movement
                                    /*this.band.Active.moveKistetoLager(this.band.Active.KisteID);
                                    this.band.KistenAblageplatz[i] = this.band.Active;
                                    this.band.LagerBelegt[i] = true;
                                    this.band.Active = null;*/
                                }

                            }
                        }
                        if (i < 2)
                        {

                            if (band.Einlagerplatz[i] == true && this.band.ZwischenlagerBelegt[i] == false)
                            {

                                if (init)
                                {
                                    Kisten kisteLager = erstelleKisteInLager(i + 4, 1);
                                    this.band.KistenLager[i] = kisteLager;
                                    this.band.ZwischenlagerBelegt[i] = band.Einlagerplatz[i];
                                }
                                else
                                {
                                    /*this.band.KistenAblageplatz[i] = this.kranarm.KisteKran;
                                    this.kranarm.KisteKran = null;
                                    this.band.ZwischenlagerBelegt[i] = true;*/

                                    //Console.WriteLine("Troublemaker...................................");
                                }
                            }
                        }
                        if (i < 3)
                        {
                            if (band.Schieber[i] == true)
                            {
                                if (this.init)
                                {
                                    this.band.BeroPuscher[i] = band.Schieber[i];
                                    Kisten kisteToLager = this.erstelleKisteVorPuscher(i + 1);
                                    this.band.Active = kisteToLager;
                                }
                            }
                        }
                    }


                    if (band.Registerlager && !this.band.TurmBelegt)
                    {
                        this.band.TurmBelegt = band.Registerlager;
                        Kisten kistenTurm = erstelleKisteInRegiter(0);
                        this.band.KisteTurm = kistenTurm;
                    }

                    if (band.Schieber[0] == true && this.band.Active == null)
                    {
                        if (band.Ablageplatz[0] == true && this.band.LagerBelegt[0] == true && !band.An)
                        {
                            this.band.KisteTurm.KisteID = 1;
                            this.band.Active = this.band.KisteTurm;
                            this.band.TurmBelegt = false;
                            this.band.KisteTurm = null;
                            this.band.Active.setKisteToPusher(1);
                        }

                        if (band.Ablageplatz[0] == true && this.band.LagerBelegt[0] == false && band.WerkstueckID == 1)
                        {
                            this.band.KisteTurm.KisteID = 1;
                            this.band.Active = this.band.KisteTurm;
                            this.band.TurmBelegt = false;
                            this.band.KisteTurm = null;
                            this.band.Active.moveKisteToPos();
                        }

                        if (band.WerkstueckID != 1)
                        {
                            this.band.KisteTurm.KisteID = band.WerkstueckID;
                            this.band.KisteTurm.kisteToPos();
                            this.band.Active = this.band.KisteTurm;
                            this.band.TurmBelegt = false;
                            this.band.KisteTurm = null;
                        }

                        if (band.Registerlager)
                        {
                            Kisten kistenTurm = erstelleKisteInRegiter(0);
                            this.band.KisteTurm = kistenTurm;
                        }
                        //this.band.Active.moveLeftTillStop();

                    }
                    /*if(band.WerkstueckID != 0 && this.band.Active.KisteID != band.WerkstueckID)
                    {
                        this.band.Active.KisteID = band.WerkstueckID;
                        this.band.Active.kisteToPos();
                    }*/

                    this.band.BandAn = band.An;
                    this.init = false;

                }
                if (response.IsSuccessStatusCode == false)
                {
                    Console.WriteLine("No Connection");

                }
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Server nicht verfügbar. Bitte Starten sie die Anwendung neu und ihren Server neu");
            }
        }

        async Task GetCranPositionAsyncFirst()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



            try
            {

                
                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("api/Crane/GetPosition");
                if (response.IsSuccessStatusCode)
                {
                    JsonObjectXYPos coord = await response.Content.ReadAsAsync<JsonObjectXYPos>();

                    double[] coords = new double[2];
                    coords = getCanvasCoord(coord.X_pos, coord.Y_pos);
                    kranarm.setKranPosition(coords[0], coords[1]);

                    GetCranePosition();

                    connection = true;

                }
                if (response.IsSuccessStatusCode == false)
                {
                    Console.WriteLine("No Connection");

                }
            }
            catch(TimeoutException)
            {
                Console.WriteLine("Server nicht erreichbar");
            }
        }

        async Task GetCraneStatus()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

	
            // HTTP GET
            try
            {
                HttpResponseMessage response = await client.GetAsync("api/Crane/Status");
                if (response.IsSuccessStatusCode)
                {
                    JsonObjectKranStatus KranStatus = await response.Content.ReadAsAsync<JsonObjectKranStatus>();


                    double[] coords = new double[2];
                    coords = getCanvasCoord(KranStatus.X_Pos, KranStatus.Y_Pos);
                    kranarm.setKranPosition(coords[0], coords[1]);

                    if (KranStatus.Oben && !this.kranarm.armMoving())
                    {
                        this.kranarm.setKranarmOben();
                        vorherBeladen = false;
                    }

                    int KranUeberLager = kranarm.isUeberLager();

                    if (KranStatus.Beladen == true && kranarm.KisteKran == null)
                    {
                        //removeKisteInSeitAnsicht(KranUeberLager);
                        //removeKisteInDraufsicht(KranUeberLager);                  
                        //this.band.LagerBelegt[KranUeberLager - 2] = false ;
                        //this.kranarm.KisteKran = new Kisten(this.Kran, this.AnsichtSeite, 50.0,50.0,50.0,2);
                        //this.band.KistenAblageplatz[KranUeberLager - 2] = null;
                        //this.AnsichtSeite,

                    }
                    else
                    {
                        //kranarm.KisteKran 
                    }

                    if (!this.kranarm.armMoving())
                    {
                        if (!KranStatus.Oben)
                        {
                            kranarm.movekranarmUnten();
                        }
                        else
                        {
                            kranarm.setKranarmOben();
                        }
                    }

                    if (modeControlFlag == false)
                    {
                        if (KranStatus.Beladen == true && kranarm.KisteKran == null)
                        {
                            Kisten kiste = new Kisten(this.Kran, this.AnsichtSeite, kranarm.XKoordinate, kranarm.YKoordinate, kranarm.ZKoordinate + 150.0, 0);
                        }
                    }

                    /*if(kranarm.KisteKran != null)
                    {
                        kranarm.KisteKran.setKistenPosition(kranarm.XKoordinate - 10.0, kranarm.YKoordinate - 10.0);
                        kranarm.KisteKran.setKisteHoehe(kranarm.ZKoordinate);
                    }*/


                    /* if(KranStatus.Beladen && !KranStatus.Oben && !vorherBeladen)
                     {
                         this.kranarm.movekranarmOben();
                         vorherBeladen = true;
                     }
                     if(KranStatus.Oben && !this.kranarm.armMoving())
                     {
                         this.kranarm.setKranarmOben();
                         vorherBeladen = false;
                     }*/
                }
                else
                {
                    Console.WriteLine("PostAsJsonAsync Error: {0} [{1}]",
                         response.StatusCode.ToString(), (int)response.StatusCode);

                }
            }
            catch(HttpRequestException)
            {               
                Console.WriteLine("Server nicht verfügbar. Bitte Starten sie die Anwendung neu und ihren Server neu");
                connection = false;
            }

        }

        // --------------- POST

        async Task PostCraneMoveLeft()
        {
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

        async Task PostCraneMoveBottom()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync("api/Crane/KranRunter", true);

            if (response.IsSuccessStatusCode)
            {
                String result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
                Console.WriteLine("PostAsJsonAsync: {0}", response.StatusCode.ToString());
			 this.kranarm.movekranarmUnten();
            }
            else
            {
                Console.WriteLine("PostAsJsonAsync Error: {0} [{1}]",
                     response.StatusCode.ToString(), (int)response.StatusCode);
            }


        }

        async Task MoveToPos(int pos)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            JsonObjectPosition position = new JsonObjectPosition(pos);


            HttpResponseMessage response = await client.PostAsJsonAsync("api/Crane/MoveToPlatz", position);

            if (response.IsSuccessStatusCode)
            {
                String result = await response.Content.ReadAsStringAsync();
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

        async Task PostMoveKisteToPos()
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync("api/Band/SendOneCrate", true);

            if (response.IsSuccessStatusCode)
            {
                String result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
                Console.WriteLine("PostAsJsonAsync: {0}", response.StatusCode.ToString());

                //if (this.band.KisteTurm != null)
                //{
                //    //this.band.Active = this.band.KisteTurm;
                //    // this.band.Active.KisteID = 3;
                //    //this.band.Active.kisteToPos();
                //}

            }
            else
            {
                Console.WriteLine("PostAsJsonAsync Error: {0} [{1}]",
                     response.StatusCode.ToString(), (int)response.StatusCode);
            }

        }

        async Task PostAutomatikAn()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync("api/Crane/AutomatikAn", true);

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

        async Task PostAutomatikAus()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync("api/Crane/AutomatikAus", true);

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

	   private void UpdatingUiElements()
	   {
		   UpdateStatusBox();
		   UpdateLagerBelegungUi();
           UpdatePositionUI();
	   }

	    private void UpdateStatusBox()
	   {
		    if(this.Dispatcher.CheckAccess())
		    {
                if(connection == true)
                {
                    if (kranarm.armMoving())
                    {
                        Status.Text = "Kranarm bewegt sich";
                    }
                    else
                    {
                        Status.Text = "Ready for Input";
                    }
                }
                else
                {
                    Status.Text = "No Connection: Reconnect";
                }
			    
		    }
		    else
		    {
			    UiUpdateHandler handler =
				    new UiUpdateHandler(this.UpdateStatusBox);
			    this.Kran.Dispatcher.BeginInvoke(handler);
		    }
	   }

	    private void UpdateLagerBelegungUi()
	    {
		    
		    if (this.Dispatcher.CheckAccess())
		    {
			    if(this.band.ZwischenlagerBelegt[0] == true)
			    {
				    LP1.Text = "Ja";
			    }
			    else
			    {
				    LP1.Text = "Nein";
			    }
			    if (this.band.ZwischenlagerBelegt[1] == true)
			    {
				    LP2.Text = "Ja";
			    }
			    else
			    {
				    LP2.Text = "Nein";
			    }
			    if (this.band.LagerBelegt[0] == true)
			    {
				    BP1.Text = "Ja";
			    }
			    else
			    {
				    BP1.Text = "Nein";
			    }
			    if (this.band.LagerBelegt[1] == true)
			    {
				    BP2.Text = "Ja";
			    }
			    else
			    {
				    BP2.Text = "Nein";
			    }
			    if (this.band.LagerBelegt[2] == true)
			    {
				    BP3.Text = "Ja";
			    }
			    else
			    {
				    BP3.Text = "Nein";
			    }
			    if (this.band.LagerBelegt[3] == true)
			    {
				    BP4.Text = "Ja";
			    }
			    else
			    {
				    BP4.Text = "Nein";
			    }




		    }
		    else
		    {
			    UiUpdateHandler handler =
				    new UiUpdateHandler(this.UpdateLagerBelegungUi);
			    this.Kran.Dispatcher.BeginInvoke(handler);
		    }
	    }

        private void UpdatePositionUI()
        {
            if (this.Dispatcher.CheckAccess())
            {
                UIYPOS.Text = kranarm.YKoordinate.ToString();
                UIXPOS.Text = kranarm.XKoordinate.ToString();
            }
            else
            {
                UiUpdateHandler handler =
                    new UiUpdateHandler(this.UpdatePositionUI);
                this.Kran.Dispatcher.BeginInvoke(handler);
            }
        }
    }
}