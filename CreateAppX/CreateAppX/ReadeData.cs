using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CreateAppX
{
    class ReadData
    {
        XmlDocument doc;
        public ReadData() {
            doc = new XmlDocument();
            doc.Load(@"F:\DevArea\Package.appxmanifest");
        }
        public void GetPackageName() {
            try
            {
                //Console.WriteLine(.ToString());
                XmlNode n = doc.GetElementsByTagName("Identity").Item(0);
                XmlAttribute Name = n.Attributes["Name"];
                XmlAttribute Publisher = n.Attributes["Publisher"];
                XmlAttribute Version = n.Attributes["Version"];

                Console.WriteLine("PackageName = "+Name.Value);
                Console.WriteLine("Publisher = " + Publisher.Value);
                Console.WriteLine("Version = " + Version.Value);

                XmlNode phone = doc.GetElementsByTagName("mp:PhoneIdentity").Item(0);
                XmlAttribute PhoneProductId = phone.Attributes["PhoneProductId"];
                Console.WriteLine("PhoneProductId = " + PhoneProductId.Value);

                XmlNode dis = doc.GetElementsByTagName("DisplayName").Item(0);
                Console.WriteLine("Display Name = " + dis.InnerText);

                XmlNode pubdis = doc.GetElementsByTagName("PublisherDisplayName").Item(0);
                Console.WriteLine("Display Name = " + pubdis.InnerText);

                XmlNode app = doc.GetElementsByTagName("m3:VisualElements").Item(0);
                XmlAttribute AppName = app.Attributes["DisplayName"];
                XmlAttribute Description = app.Attributes["Description"];

                Console.WriteLine("App Name = " + AppName.Value);
                Console.WriteLine("Description = " + AppName.Value);

               // doc.Save(@"F:\DevArea\Package.appxmanifest");


            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        public void UpdateManifest() {
            try
            {
                XmlNode n = doc.GetElementsByTagName("Identity").Item(0);
                XmlAttribute Name = n.Attributes["Name"];
                Name.Value = "Bazinga";
                doc.Save(@"F:\DevArea\Package.appxmanifest");
                GetPackageName();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
