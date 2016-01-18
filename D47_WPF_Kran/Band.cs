using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D47_WPF_Kran
{
    class Band
    {
        public bool An;	
        public int	Werkstück_id;	
        public bool[] Schieber;
        public int[] Ablageplatz;	
        public int[] Einlagerplatz;
        public int[] Registerlager;	
    }

    class KranSync
    {
        private KranDarstellung KranPic;
        public bool isRunning { get; set; }
        public int X_Pos { get; set; }
        public int Y_Pos { get; set; }
        public int X_Direction { get; set; }
        public int Y_Direction { get; set; }	
        public bool Oben { get; set; }
        public bool Pickup { get; set; }	
        public int[] Queue { get; set; }

        public KranSync()
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
            
        }

        public KranSync returnSelf()
        {
            return this;
        }
    }
}
