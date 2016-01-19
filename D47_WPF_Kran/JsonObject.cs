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
        public bool isRunning { get; set; }
        public double X_Pos { get; set; }
        public double Y_Pos { get; set; }
        public int X_Direction { get; set; }
        public int Y_Direction { get; set; }	
        public bool Oben { get; set; }
        public bool Pickup { get; set; }	
        public int[] Queue { get; set; }

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

    class JsonOBjectMoveCrane
    {
        bool left = false;
        bool right = false;
        bool forward =  false;
        bool backward = false;
    }

    
}
