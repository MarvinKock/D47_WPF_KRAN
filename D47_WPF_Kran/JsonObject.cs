using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D47_WPF_Kran
{
    class JsonObjectBandStatus
    {
        bool an = false;
        bool[] ablageplatz = new bool[4];
        bool[] schieber = new bool[3];
        bool[] einlagerplatz = new bool[2];
        bool registerlager = false;
        int werkstueckID = 0;
        bool anfangssensor = false;

        public bool Anfangssensor
        {
            get { return anfangssensor; }
            set { anfangssensor = value; }
        }

        public int WerkstueckID
        {
            get { return werkstueckID; }
            set { werkstueckID = value; }
        }

        public bool Registerlager
        {
            get { return registerlager; }
            set { registerlager = value; }
        }
        public bool An
        {
            get { return an; }
            set { an = value; }
        }
       

       
       

        public bool[] Schieber
        {
            get { return schieber; }
            set { schieber = value; }
        }
        

        public bool[] Ablageplatz
        {
            get { return ablageplatz; }
            set { ablageplatz = value; }
        }
       

        public bool[] Einlagerplatz
        {
            get { return einlagerplatz; }
            set { einlagerplatz = value; }
        }
        

      
    }

    class JsonObjectKranStatus
    {
        private KranDarstellung KranPic;

           private double x_Pos;
            public double X_Pos
            {
              get { return x_Pos; }
              set { x_Pos = value; }
            }
            private double y_Pos;

            public double Y_Pos
            {
                get { return y_Pos; }
                set { y_Pos = value; }
            }
            //private int x_Direction;

            //public int X_Direction
            //{
            //    get { return x_Direction; }
            //    set { x_Direction = value; }
            //}
            //private int y_Direction;

            //public int Y_Direction
            //{
            //    get { return y_Direction; }
            //    set { y_Direction = value; }
            //}
            private bool oben;

            public bool Oben
            {
                get { return oben; }
                set { oben = value; }
            }
            private bool beladen;

            public bool Beladen
            {
                get { return beladen; }
                set { beladen = value; }
            }
            

        public JsonObjectKranStatus()
        {
           
        }
	
        public void setCran(KranDarstellung kran)
        {
            this.KranPic = kran;
        }
        public void getPropertiesOfCrane()
        {
        }

        public JsonObjectKranStatus returnSelf()
        {
            return this;
        }
    }

    class JsonObjectMoveCrane
    {
        public bool Left = false;
        public bool Right = false;
        public bool Forward = false;
        public bool Backward = false;

        public JsonObjectMoveCrane(string direction)
        {
            switch(direction)
            {
                case "left": Left = true; break;
                case "right": Right = true; break;
                case "forward": Forward = true; break;
                case "backward": Backward = true; break;
                default: Left = false; Right = false; Forward = false; Backward = false; break;
            }
        }
    }

    class JsonObjectXYPos
    {
        double x_pos;
        double y_pos;

        public double X_pos
        {
          get { return x_pos; }
          set { x_pos = value; }
        }
        

        public double Y_pos
        {
          get { return y_pos; }
          set { y_pos = value; }
        }

        public JsonObjectXYPos()
        {
            x_pos = 0;
            y_pos = 0;
        }
    }

    class JsonObjectStatusSensors
    {
        bool x_min_pos;
        bool x_max_pos;
        bool y_min_pos;
        bool y_max_pos;
        bool greifer_oben;
        bool greifer_unten;


        public bool X_min_pos
        {
          get { return x_min_pos; }
          set { x_min_pos = value; }
        }

        public bool X_max_pos
        {
          get { return x_max_pos; }
          set { x_max_pos = value; }
        }

        public bool Y_min_pos
        {
          get { return y_min_pos; }
          set { y_min_pos = value; }
        }

        public bool Y_max_pos
        {
          get { return y_max_pos; }
          set { y_max_pos = value; }
        }
     
        public bool Greifer_oben
        {
          get { return greifer_oben; }
          set { greifer_oben = value; }
        }
        
        public bool Greifer_unten
        {
          get { return greifer_unten; }
          set { greifer_unten = value; }
        }


        public JsonObjectStatusSensors()
        {
            this.greifer_oben = false;
            this.greifer_unten = false;
            this.x_max_pos = false;
            this.x_min_pos = false;
            this.y_max_pos = false;
            this.y_min_pos = false;
        }
    }

	class JsonObjectPosition
	{
		int position;

		public JsonObjectPosition(int position)
		{
			this.position = position;
		}

		public int Platz
		{
			get { return position; }
			set { position = value;  }
		}
	}

    
}
