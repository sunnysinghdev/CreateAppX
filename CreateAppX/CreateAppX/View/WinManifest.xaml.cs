using System;
using System.Collections.Generic;
using System.IO;
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

using CreateAppX.ViewModel;

namespace CreateAppX.View
{
    /// <summary>
    /// Interaction logic for WinManifest.xaml
    /// </summary>
    public partial class WinManifest : Page
    {
        public WinManifestViewModel manifest { get; set; }
        public WinManifest()
        {
            InitializeComponent();
        }
        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            string path = WinManifestViewModel.GetPath(txtFolderPath);
            manifest = new WinManifestViewModel(path);
            ManifestGrid.Visibility = Visibility.Visible;
            this.DataContext = manifest.phoneM;
            savebtn.IsEnabled = true;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            manifest.Save();
            BuildProject pa = new BuildProject();
            // NavigationService navService = NavigationService.GetNavigationService(this);
            // navService.Navigate(pa);
           this.NavigationService.Navigate(pa);
        }

    }
}
