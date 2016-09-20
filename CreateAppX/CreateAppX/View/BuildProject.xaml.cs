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
        BuildProjectViewModel buildProject { get; set; }
        CommandType cmdType { get; set; }
        public BuildProject()
        {
            InitializeComponent();
            this.DataContext = this;
            buildProject = new BuildProjectViewModel();
            List<CommandType> s = buildProject.GetPlatform();
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
                cmdType = c;
                BuildBtn.IsEnabled = true;// Visibility.Visible;
            }
        }

        private void ButtonBuild_Click(object sender, RoutedEventArgs e)
        {
            //var a =this.Cursor ;//= Mouse.OverrideCursor;
            BuildLog.Visibility = Visibility.Visible;
            DeployBtn.IsEnabled = false;
            FinishBtn.IsEnabled = false;
            buildProject.BuildProject(cmdType,PhoneLog);
        }
        private void ButtonFinish_Click(object sender, RoutedEventArgs e)
        {
            buildProject.GetPackage();
            Application.Current.Shutdown();
        }
        private void ButtonDeploy_Click(object sender, RoutedEventArgs e)
        {
            buildProject.DeployToDevice();
        }
        public void PhoneLog(string ptf, string log)
        {
            if (log.Length > 80)
            {
               log = log.Substring(0,35)+"..."+log.Substring(log.Length - 40);
            }
            Application.Current.Dispatcher.Invoke(() => {
                if(log=="Completed")
                    FinishBtn.IsEnabled = true;
                if (ptf == "Phone")
                {
                    phoneLog.Content = "Phone   = " + log;//.Substring(log.Length - 30);
                    if (log == "Completed")
                    {
                        DeployBtn.IsEnabled = true;
                        
                    }
                }
                else if (ptf == "Surface")
                {
                    surfaceLog.Content = "Surface = " + log;//.Substring(log.Length - 30);
                }
                else
                {
                    desktopLog.Content = "Desktop = " + log;//.Substring(log.Length - 25);
                }
            });
            

        }
        public void SurfaceLog(string log)
        {


        }
        public void DesktopLog(string log)
        {


        }
    }
}
