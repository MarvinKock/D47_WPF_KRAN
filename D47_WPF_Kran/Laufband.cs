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
    class Laufband
    {
        private bool[] lagerBelegt = new bool[4];
        private bool[] zwischenlagerBelegt = new bool[2];
        private bool turmBelegt;
        private bool bandBelegt;
        private bool bandAn;
        private bool[] puscherBetaetigt = new bool[3];


        public bool[] LagerBelegt
        {
            get { return lagerBelegt; }
            set { lagerBelegt = value; }
        }

        public bool TurmBelegt
        {
            get { return turmBelegt; }
            set { turmBelegt = value; }
        }

        public bool BandBelegt
        {
            get { return bandBelegt; }
            set { bandBelegt = value; }
        }

        public bool BandAn
        {
            get { return bandAn; }
            set { bandAn = value; }
        }
        public bool[] PuscherBetaetigt
        {
            get { return puscherBetaetigt; }
            set { puscherBetaetigt = value; }
        }

        public bool[] ZwischenlagerBelegt
        {
            get { return zwischenlagerBelegt; }
            set { zwischenlagerBelegt = value; }
        }

        public Laufband()
        {
            this.turmBelegt = false;
            this.bandBelegt = false;
            this.bandAn = false;
            for(int i = 0; i < 3; i++)
                this.puscherBetaetigt[i] = false;
            for (int i = 0; i < 4; i++)
                this.lagerBelegt[i] = false;
            for (int i = 0; i < 2; i++)
                this.zwischenlagerBelegt[i] = false;
        }
    }
}
