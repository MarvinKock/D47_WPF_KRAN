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

        private kranDraufsicht kranDrauf;
        private kranSeitsicht kranSeite;

        public Kran(KranDarstellung drauf, Seitenansicht seit, double x, double y, double z)
        {
            this.draufsicht = drauf;
            this.seitsicht = seit;
            this.xKoordinate = x;
            this.yKoordinate = y;
            this.zKoordinate = z;

            this.kranDrauf = new kranDraufsicht(this.draufsicht, x, y);
            this.kranSeite = new kranSeitsicht(this.seitsicht, x, z);
        }

        public void setKranPosition(double x, double y)
        {
            this.kranDrauf.setKranPosition(x, y);
            this.kranSeite.setKranPosition(x);
        }

        public void setHoeheKran(double z)
        {
            this.kranSeite.setKranarmHoehe(z);
        }

  
    }
}
