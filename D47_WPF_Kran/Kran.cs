using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace D47_WPF_Kran
{
    public class Kran
    {
        KranDarstellung draufsicht;
        Seitenansicht seitsicht;
        double xKoordinate;
        double yKoordinate;


        double zKoordinate;

       
        int ueberLager;

        private kranDraufsicht kranDrauf;
        private kranSeitsicht kranSeite;
        private bool inPosition;

        private Kisten kisteKran;

        public Kisten KisteKran
        {
            get { return kisteKran; }
            set { kisteKran = value; }
        }

        public bool InPosition
        {
            get { return inPosition; }
            set { inPosition = value; }
        }

        public int UeberLager
        {
            get { return ueberLager; }
            set { ueberLager = value; }
        }

          public double XKoordinate
        {
            get { return xKoordinate; }
            set { xKoordinate = value; }
        }

          public double YKoordinate
          {
              get { return yKoordinate; }
              set { yKoordinate = value; }
          }

          public double ZKoordinate
          {
              get { return zKoordinate; }
              set { zKoordinate = value; }
          }

        public Kran(KranDarstellung drauf, Seitenansicht seit, double x, double y, double z)
        {
            this.draufsicht = drauf;
            this.seitsicht = seit;
            this.xKoordinate = x;
            this.yKoordinate = y;
            this.zKoordinate = z;

            this.kranDrauf = new kranDraufsicht(this.draufsicht, x, y);
            this.kranSeite = new kranSeitsicht(this.seitsicht, x, z);
            this.kisteKran = null;
        }

        public void setKranPosition(double x, double y)
        {
            this.xKoordinate = x;
            this.yKoordinate = y;
            this.kranDrauf.setKranPosition(x, y);
            this.kranSeite.setKranPosition(x);
            if(this.kisteKran != null)
            {
                this.kisteKran.setKistenPosition(x, y);
            }
        }

        public void setKranarmOben()
        {
            this.kranSeite.setKranarmOben();
        }

        public void setKranarmUnten()
        {
            this.kranSeite.setKranarmUnten();
        }

        public void setKranHoehe(double z)
        {
            this.zKoordinate = z;
            this.kranSeite.setKranarmHoehe(z);
        }

        public int isUeberLager()
        {
            if (Math.Round(xKoordinate,4) == 163.0922 && Math.Round(yKoordinate, 4) == 52.5749)
           {
               return 5;
           }
            else if (Math.Round(xKoordinate, 4) == 219.0482 && Math.Round(yKoordinate, 4) == 53.9738)
           {
               return 6;
           }
            else if (Math.Round(xKoordinate, 4) == 219.0482 && Math.Round(yKoordinate, 4) == 186.8693)
           {
               return 4;
           }
            else if (Math.Round(xKoordinate, 4) == 273.6053 && Math.Round(yKoordinate, 4) == 241.4264)
           {
               return 3;
           }
            else if (Math.Round(xKoordinate, 4) == 353.3426 && Math.Round(yKoordinate, 4) == 241.4264)
           {
               return 2;
           }
            else if (Math.Round(xKoordinate, 4) == 435.8777 && Math.Round(yKoordinate, 4) == 241.4264)
           {
               return 1;
           }
            else if (Math.Round(xKoordinate, 4) == 526.8062 && Math.Round(yKoordinate, 4) == 186.8693)
           {
               return 7;
           }
           else
           {
               return 0;
           }
        }

        public void movekranarmOben()
        {
            ThreadStart ts = new ThreadStart(this.kranSeite.moveKranarmOben);
            Thread t = new Thread(ts);
            t.Start();
        }

        private void moveKranarmOben()
        {
            ThreadStart ts = new ThreadStart(this.kranSeite.moveKranarmUnten);
            Thread t = new Thread(ts);
            t.Start();

        }
  
    }
}
