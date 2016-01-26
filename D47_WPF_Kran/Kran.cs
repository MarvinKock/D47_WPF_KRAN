using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.kranSeite.setKranarmHoehe(z);
        }

  
    }
}
