using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D47_WPF_Kran
{
    class JsonObjectBandStatus
    {
        public bool An;	
        public int	Werkstück_id;	
        public bool[] Schieber;
        public int[] Ablageplatz;	
        public int[] Einlagerplatz;
        public int[] Registerlager;	
    }

    class JsonObjectKranStatus
    {
        private KranDarstellung KranPic;

            private bool isRunning ;
            public bool IsRunning
            {
              get { return isRunning; }
              set { isRunning = value; }
            }private double x_Pos;
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
            private int x_Direction;

            public int X_Direction
            {
                get { return x_Direction; }
                set { x_Direction = value; }
            }
            private int y_Direction;

            public int Y_Direction
            {
                get { return y_Direction; }
                set { y_Direction = value; }
            }
            private bool oben;

            public bool Oben
            {
                get { return oben; }
                set { oben = value; }
            }
            private bool pickup;

            public bool Pickup
            {
                get { return pickup; }
                set { pickup = value; }
            }
            private int[] queue;

            public int[] Queue
            {
                get { return queue; }
                set { queue = value; }
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
            X_Pos = KranPic.GetXPos();
            Y_Pos = KranPic.GetYPos();
            isRunning = false;
            X_Direction = 0;
            
        }

        public JsonObjectKranStatus returnSelf()
        {
            return this;
        }
    }

    class JsonObjectMoveCrane
    {
        public bool left = false;
        public bool right = false;
        public bool forward = false;
        public bool backward = false;

        public JsonObjectMoveCrane(string direction)
        {
            switch(direction)
            {
                case "left": left = true; break;
                case "right": right = true; break;
                case "forward": forward = true; break;
                case "backward": backward = true; break;
                default: left = false; right = false; forward = false; backward = false; break;
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

    
}
