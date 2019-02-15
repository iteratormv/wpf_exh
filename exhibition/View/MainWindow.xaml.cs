﻿using EX.Service;
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

namespace exhibition
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VisitorExecutor visitorExecutor;

        public MainWindow()
        {
            InitializeComponent();
//            visitorExecutor = new VisitorExecutor("192.168.0.29");
 //           visitorExecutor.statusChanged += StatusChanged;
        }

        private void StatusChanged(string obj)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
     //       workgrid = new DataGrid();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(100);
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    DataMode = visitorExecutor.status;
        //    Task.Factory.StartNew(visitorExecutor.Start);
        //}
    }
}
