using System;
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
            
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            string path = FileBrowser.GetPath();
            if (String.IsNullOrEmpty(path)) {
                MessageBox.Show("Please Select Path");
                return;
            }
            txtFolderPath.Text = path;
            path = path.Split(new[] { "build" }, StringSplitOptions.RemoveEmptyEntries)[0];
            path = path + @"build\Windows8.1";
            Console.WriteLine(path);
           // manifest = new WinManifestViewModel(path);
            //this.DataContext = manifest.phoneM;
            // new WinManifestViewModel(@"G:\DevArea\WindowsBridge\WindowsBridge\build\Windows8.1").Save();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            manifest.Save();
        }
    }
}
