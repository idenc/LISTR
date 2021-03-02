﻿using System;
using System.Windows;

namespace LISTR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Media_Ended(object sender, RoutedEventArgs e)
        {
            backgroundVideo.Position = TimeSpan.FromMilliseconds(1);
        }
    }
}
