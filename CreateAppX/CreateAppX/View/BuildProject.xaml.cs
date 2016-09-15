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
//Problem faced uri path format
// window icon path should be ../ICon.ico
// use item panel to display list horizontally
namespace CreateAppX.View
{
    /// <summary>
    /// Interaction logic for BuildProject.xaml
    /// </summary>
    public partial class BuildProject : Page
    {
        public BuildProject()
        {
            InitializeComponent();
            this.DataContext = this;
            List<CommandType> s = new BuildProjectViewModel().GetPlatform();
            PlatformList.ItemsSource = s;
           
          //  pack://aplication:,,,/Assets/Phone.png
            // imageP.Source = new BitmapImage(new Uri(@"pack://application:,,,/Assets/Phone.png",UriKind.Absolute));
            // imageP.Source = new BitmapImage(new Uri(@"pack://aplication:,,,/Assets/Phone.png",UriKind.Absolute));
        }

        private void PlatformList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CommandType c = (sender as ListView).SelectedItem as CommandType;
            if (c != null) {
                MessageBox.Show(c.Platform);
            }
        }
    }
}
