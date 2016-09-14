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
using CreateAppX.ViewModel;
namespace CreateAppX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WinManifestViewModel manifest { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            manifest = new WinManifestViewModel(@"G:\DevArea\WindowsBridge\WindowsBridge\build\Windows8.1");
            this.DataContext = manifest;
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
           // new WinManifestViewModel(@"G:\DevArea\WindowsBridge\WindowsBridge\build\Windows8.1").Save();
        }
    }
}