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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MongoDB.Driver;

namespace LISTR


{
    /// <summary>
    /// Interaction logic for myLISTR.xaml
    /// </summary>
    public partial class myLISTR : Page
    {

        private readonly MainWindow mainWindow;


        public myLISTR()
        {
            InitializeComponent();
        }
    }
}