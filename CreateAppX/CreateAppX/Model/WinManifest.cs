using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CreateAppX.Model
{
    abstract class WinManifest:IEnumerable<string>
    {
        protected string manifestPath;
        protected XmlDocument xmlDoc;
        private string packageName;
        private string version;
        private string publisher;
        private string publisherDisplayName;
        private string displayName;
       // private string appDisplayName;
        //private string appDescription;

        public string PackageName
        {
            get { return packageName; }
            set
            {
                packageName = value;
                XmlNode n = xmlDoc.GetElementsByTagName("Identity").Item(0);
                n.Attributes["Name"].Value = packageName;
            }
        }
        
        public string Version
        {
            get { return version; }
            set
            {
                version = value;
                XmlNode n = xmlDoc.GetElementsByTagName("Identity").Item(0);
                n.Attributes["Version"].Value = version;
            }
        }
        public string Publisher
        {
            get { return publisher; }
            set
            {
                publisher = value;
                XmlNode n = xmlDoc.GetElementsByTagName("Identity").Item(0);
                n.Attributes["Publisher"].Value = publisher;
            }
        }
        public string PublisherDisplayName
        {
            get { return publisherDisplayName; }
            set
            {
                publisherDisplayName = value;
                xmlDoc.GetElementsByTagName("PublisherDisplayName").Item(0).InnerText = publisherDisplayName;
            }
        }
        public string DisplayName
        {
            get { return displayName; }
            set
            {
                displayName = value;
                xmlDoc.GetElementsByTagName("DisplayName").Item(0).InnerText = displayName;
            }
        }

        private string appDisplayName;
        private string appDescription;
        public string AppDisplayName
        {
            get { return appDisplayName; }
            set
            {
                appDisplayName = value;
                OnAppDisplayNameChanged(appDisplayName);
            }
        }

        protected abstract void OnAppDisplayNameChanged(string appDisplayName);

        public string AppDescription
        {
            get { return appDescription; }
            set
            {
                appDescription = value;
                OnAppDescriptionChanged(appDisplayName);
            }
        }

        protected abstract void OnAppDescriptionChanged(string appDisplayName);
        
        public bool Save()
        {
            try
            {
                xmlDoc.Save(manifestPath);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            List<string> li = new List<string>();
            li.Add(PackageName);
            li.Add(Version);
            li.Add(Publisher);
            li.Add(DisplayName);
            li.Add(PublisherDisplayName);
            li.Add(AppDisplayName);
            li.Add(AppDescription);
            return li.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            List<string> li = new List<string>();
            li.Add(PackageName);
            li.Add(Version);
            li.Add(Publisher);
            li.Add(DisplayName);
            li.Add(PublisherDisplayName);
            li.Add(AppDisplayName);
            li.Add(AppDescription);
            return li.GetEnumerator();
        }
        public override string ToString()
        {
            return "App Display Name = " + AppDisplayName + "\n" +
                "App Reserved Name = " + DisplayName + "\n" +
                "App Description = " + AppDescription + "\n" +
                "Package Name = " + PackageName + "\n" +
                "Version = " + Version + "\n" +
                "Publisher = " + Publisher + "\n" +
                "Publisher Display Name = " + PublisherDisplayName + "\n";
        }
    }
    class PhoneManifest : WinManifest
    {
        public string PhoneProductId { get; set; }

        public PhoneManifest(string path) 
        {
            manifestPath = path+ @"\Appzillon\Appzillon.WindowsPhone\Package.appxmanifest";
            xmlDoc = new XmlDocument();
            xmlDoc.Load(manifestPath);
        }

        protected override void OnAppDisplayNameChanged(string val)
        {
            XmlNode n = xmlDoc.GetElementsByTagName("m3:VisualElements").Item(0);
            n.Attributes["DisplayName"].Value = val;
        }

        protected override void OnAppDescriptionChanged(string val)
        {
            XmlNode n = xmlDoc.GetElementsByTagName("m3:VisualElements").Item(0);
            n.Attributes["Description"].Value = val;
        }
        public void Load() {
            try
            {
                //Console.WriteLine(.ToString());
                XmlNode n = xmlDoc.GetElementsByTagName("Identity").Item(0);
                PackageName = n.Attributes["Name"].Value;
                Publisher = n.Attributes["Publisher"].Value;
                Version= n.Attributes["Version"].Value;

               

                XmlNode phone = xmlDoc.GetElementsByTagName("mp:PhoneIdentity").Item(0);
                PhoneProductId = phone.Attributes["PhoneProductId"].Value;
                

                DisplayName = xmlDoc.GetElementsByTagName("DisplayName").Item(0).InnerText;
                

                PublisherDisplayName = xmlDoc.GetElementsByTagName("PublisherDisplayName").Item(0).InnerText;
                

                XmlNode app = xmlDoc.GetElementsByTagName("m3:VisualElements").Item(0);
                AppDisplayName = app.Attributes["DisplayName"].Value;
                AppDescription = app.Attributes["Description"].Value;

                //Console.WriteLine("App Name = " + AppDisplayName);
                //Console.WriteLine("Description = " + AppDescription);
                //Console.WriteLine("PackageName = " + PackageName);
                //Console.WriteLine("Publisher = " + Publisher);
                //Console.WriteLine("Version = " + Version);
                //Console.WriteLine("App Reserved Name = " + DisplayName);
                //Console.WriteLine("Publisher Display Name = " + PublisherDisplayName);
                Console.WriteLine("PhoneProductId = " + PhoneProductId);
                // doc.Save(@"F:\DevArea\Package.appxmanifest");


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
    class WindowsManifest : WinManifest
    {
        public WindowsManifest(string path) 
        {
            manifestPath = path + @"\Appzillon\Appzillon.Windows\Package.appxmanifest";
            xmlDoc = new XmlDocument();
            xmlDoc.Load(manifestPath);
        }

        protected override void OnAppDisplayNameChanged(string val)
        {
            XmlNode n = xmlDoc.GetElementsByTagName("m2:VisualElements").Item(0);
            n.Attributes["DisplayName"].Value = val;
        }

        protected override void OnAppDescriptionChanged(string val)
        {
            XmlNode n = xmlDoc.GetElementsByTagName("m2:VisualElements").Item(0);
            n.Attributes["Description"].Value = val;
        }
    }
}
