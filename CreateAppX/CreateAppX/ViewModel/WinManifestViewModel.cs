using CreateAppX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CreateAppX.ViewModel
{
    public class WinManifestViewModel
    {
        public PhoneManifest phoneM { get; set; }
        WindowsManifest winM;
        private string path;
        public WinManifestViewModel(string path)
        {
            phoneM = new PhoneManifest(path);
            this.path = path;
            phoneM.Load();
          
            
        }
        public void Save() {
            winM = new WindowsManifest(path);
            PropertyInfo[] infos = winM.GetType().GetProperties();
            foreach (var info in infos)
            {
                PropertyInfo sourcePI = phoneM.GetType().GetProperty(info.Name);
                info.SetValue(winM, sourcePI.GetValue(phoneM, null), null);
            }
            Console.WriteLine(winM.ToString());
            //winM.Save();
            //phoneM.Save();
        }
    }
}
