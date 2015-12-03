using System;
using System.Collections.Generic;
using System.Linq;
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
        Runter
    }
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool isRunning = false;
        private button buttonClick;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void KranLinks_Click(object sender, RoutedEventArgs e)
        {
            Thread t;
                    
            // erster Start
            ThreadStart ts = new ThreadStart(MoveElevatorPara);
            t = new Thread(ts);
            isRunning = true;
                
            t.Start();
            buttonClick = button.Links;  
        }


        private void KranTop_Click(object sender, RoutedEventArgs e)
        {
            Thread t;

            // erster Start
            ThreadStart ts = new ThreadStart(MoveElevatorPara);
            t = new Thread(ts);
            isRunning = true;

            t.Start();
            buttonClick = button.Hoch; 
        }

        private void KranRechts_Click(object sender, RoutedEventArgs e)
        {
            Thread t;

            // erster Start
            ThreadStart ts = new ThreadStart(MoveElevatorPara);
            t = new Thread(ts);
            isRunning = true;

            t.Start();
            buttonClick = button.Rechts; 
        }

        private void KranBottom_Click(object sender, RoutedEventArgs e)
        {
            Thread t;

            // erster Start
            ThreadStart ts = new ThreadStart(MoveElevatorPara);
            t = new Thread(ts);
            isRunning = true;

            t.Start();
            buttonClick = button.Runter; 
        }

        private void MoveElevatorPara()//(object o)
        {

            while (this.isRunning == true)
            {
                if (buttonClick == button.Links)
                    Kran.moveKranLeft();
                if (buttonClick == button.Rechts)
                    Kran.moveKranRechts();
                if (buttonClick == button.Hoch)
                    Kran.moveKranHoch();
                if (buttonClick == button.Runter)
                    Kran.moveKranRunter();
                // Console.WriteLine("da");
                Thread.Sleep(30);

            }

            //this.ReachedFloor.Invoke(elevatorAtFloor);
        }
    }
}
