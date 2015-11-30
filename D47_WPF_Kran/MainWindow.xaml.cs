﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KranDarstellung kran = new KranDarstellung();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void KranLinks_Click(object sender, RoutedEventArgs e)
        {
            kran.KranPic.bewegungXrichtungPositiv();
        }
    }
}
