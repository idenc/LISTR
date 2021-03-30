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

namespace LISTR
{
    /// <summary>
    /// Interaction logic for Filters.xaml
    /// </summary>
    public partial class Filters : Page
    {
        public Filters()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = Brushes.Gray;
            RentSwitch.Background = Brushes.LightGray;
        }
        private void Rent_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = Brushes.Gray;
            BuySwitch.Background = Brushes.LightGray;
        }
        private void Tag_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Background == Brushes.LightCyan) { btn.Background = Brushes.Gray; }
            else { btn.Background = Brushes.LightCyan; }
        }

    }
}
