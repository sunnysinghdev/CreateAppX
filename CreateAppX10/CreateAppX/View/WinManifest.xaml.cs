using CreateAppX.ViewModel;
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
            string path = FileBrowser.GetPath();
            if (String.IsNullOrEmpty(path))
            {
                MessageBox.Show("Please Select Path");
                return;
            }
            txtFolderPath.Text = path;
            if (path.IndexOf("build") >= 0)
            {
                path = path.Split(new[] { "build" }, StringSplitOptions.RemoveEmptyEntries)[0];
                path = path + @"build\Windows8.1";
            }
            else if (path.IndexOf("Windows8.1") >= 0)
            {
                path = path.Split(new[] { "Windows8.1" }, StringSplitOptions.RemoveEmptyEntries)[0];
                path = path + @"Windows8.1";
            }
            else
            {
                MessageBox.Show("Path does not contain build folder");
                return;
            }
            if (!File.Exists(path + @"\Appzillon.sln"))
            {
                MessageBox.Show("Folder path does not conatin project files");
                return;
            }
           // new Thread(() => EncodingBom.Convert(path)).Start();
            Console.WriteLine(path);
            manifest = new WinManifestViewModel(path);
            this.DataContext = manifest.phoneM;

        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //  manifest.Save();
            BuildProject pa = new BuildProject();
            // NavigationService navService = NavigationService.GetNavigationService(this);
            //  navService.Navigate(pa);
           this.NavigationService.Navigate(pa);
        }
    }
}
