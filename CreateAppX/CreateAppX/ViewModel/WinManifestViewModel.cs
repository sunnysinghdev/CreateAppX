using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using CreateAppX.Model;

namespace CreateAppX.ViewModel
{
    public class WinManifestViewModel
    {
        public PhoneManifest phoneM { get; set; }
        WindowsManifest winM;
        private string path;
        public static string BuildPath;
        public static string Version;
        public static string AppName;
        public WinManifestViewModel(string path)
        {
            phoneM = new PhoneManifest(path);
            this.path = path;
            BuildPath = path;
            phoneM.Load();
            Version = phoneM.Version;
            
        }
        public void Save() {
            Version = phoneM.Version;
            AppName = phoneM.AppDisplayName;
            winM = new WindowsManifest(path);
            PropertyInfo[] infos = winM.GetType().GetProperties();
            foreach (var info in infos)
            {
                PropertyInfo sourcePI = phoneM.GetType().GetProperty(info.Name);
                info.SetValue(winM, sourcePI.GetValue(phoneM, null), null);
            }
            Console.WriteLine(winM.ToString());
            winM.Save();
            phoneM.Save();
        }
        public static string GetPath(object txtPath)
        {
            string path = FileBrowser.GetPath();
            if (String.IsNullOrEmpty(path))
            {
                MessageBox.Show("Please Select Path");
                return null;
            }
            (txtPath as System.Windows.Controls.TextBox).Text = path;
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
                return null;
            }
            if (!File.Exists(path + @"\Appzillon.sln"))
            {
                MessageBox.Show("Folder path does not conatin project files");
                return null;
            }
            new Thread(() => EncodingBom.Convert(path)).Start();
            Console.WriteLine(path);
            return path;
        }
    }
}
