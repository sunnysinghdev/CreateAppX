using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CreateAppX.Model;
using System.Windows.Forms;
namespace CreateAppX.ViewModel
{
    public class CommandType {
        private string platform;
        public string Platform
        {
            get
            {
                return platform;
            }
            set
            {
                platform = value;
            }
        }
        public string OS { get; set; }
        public Uri ImageUrl
        {
            get
            {
                return new Uri(@"pack://application:,,,/Assets/" + platform + @".png",UriKind.Absolute);
            }
        }
    }
    public class BuildProjectViewModel
    {
        public List<CommandType> GetPlatform()
        {
            List<CommandType> commands = new List<CommandType>();
            commands.Add(new CommandType { Platform = "All", OS="All" });
            commands.Add(new CommandType { Platform = "Phone", OS = "WindowsPhone" });
            commands.Add(new CommandType { Platform = "Surface", OS = "Windows" });
            commands.Add(new CommandType { Platform = "Desktop", OS = "Windows" });
            commands.Add(new CommandType { Platform = "PhoneSimulator", OS = "WindowsPhone" });
            return commands;
        }
     
        public void BuildProject(CommandType t, Action<string,string> callback)
        {
            while (!EncodingBom.Completed) 
            {
                Thread.Sleep(1000);
            }
            if (t.Platform == "Surface" || t.Platform == "Phone")
            {
                string cmd = "msbuild \"" + GetProjectPath(t.OS) +
                    @""" /t:rebuild /p:configuration=release;platform=arm;outdir=" +
                    WinManifestViewModel.BuildPath +
                    @"\CreateAppx\";
                //Console.WriteLine(cmd);

                string plf = t.Platform;
                //System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                Thread pT = new Thread(() => ExecuteCMD.Execute(cmd, callback, plf));
                pT.Start();
                // System.Windows.Input.Mouse.OverrideCursor = null;
            }
            else if (t.Platform == "Desktop" || t.Platform == "PhoneSimulator")
            {
                string cmd = "msbuild \"" + GetProjectPath(t.OS) +
                    @""" /t:rebuild /p:configuration=release;platform=x64;outdir=" +
                    WinManifestViewModel.BuildPath +
                    @"\CreateAppx_x64\";
                string plf = t.Platform;
              //  System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
               // var funC = (text as CreateAppX.View.BuildProject).DesktopLog;
                Thread pT = new Thread(() => ExecuteCMD.Execute(cmd, callback, plf));
                pT.Start();
                // pT.Join();

            }
            else
            {
                t.OS = "WindowsPhone";
                t.Platform = "Phone";
                BuildProject(t,callback);

                t.OS = "Windows";
                t.Platform = "Surface";
                BuildProject(t,callback);

                t.Platform = "Desktop";
                BuildProject(t,callback);
            }
        }
        
        
        public void GetPackage()
        {
            string phonePath = WinManifestViewModel.BuildPath +
                @"\CreateAppx\Appzillon.WindowsPhone\Appzillon.WindowsPhone_" +
                WinManifestViewModel.Version +
                @"_Bundle\Appzillon.WindowsPhone_" +
                WinManifestViewModel.Version + @"_arm.appx";
            string surfacePath = WinManifestViewModel.BuildPath +
                @"\CreateAppx\Appzillon.Windows\Appzillon.Windows_" +
                WinManifestViewModel.Version +
                @"_Bundle\Appzillon.Windows_" +
                WinManifestViewModel.Version + @"_arm.appx";
            string desktopPath = WinManifestViewModel.BuildPath +
                @"\CreateAppx_x64\Appzillon.Windows\Appzillon.Windows_" +
                WinManifestViewModel.Version +
                @"_Bundle\Appzillon.Windows_" +
                WinManifestViewModel.Version + @"_x64.appx";

            bool isPhone = File.Exists(phonePath);
            bool isSurface = File.Exists(surfacePath);
            bool isx64 = File.Exists(desktopPath);
            Console.WriteLine("Phone =" + isPhone);
            Console.WriteLine("Surface =" + isSurface);
            Console.WriteLine("Destop =" + isx64);
            Directory.CreateDirectory(WinManifestViewModel.BuildPath + @"\" + WinManifestViewModel.AppName);
            if (isPhone)
                File.Copy(phonePath, WinManifestViewModel.BuildPath + @"\" + WinManifestViewModel.AppName+@"\Phone.appx", true);
            if (isSurface)
                File.Copy(surfacePath, WinManifestViewModel.BuildPath + @"\" + WinManifestViewModel.AppName+@"\Surface.appx", true);
            if (isx64)
                File.Copy(desktopPath, WinManifestViewModel.BuildPath + @"\" + WinManifestViewModel.AppName + @"\Desktop.appx", true);
            ExecuteCMD.Execute("explorer " + WinManifestViewModel.BuildPath + @"\" + WinManifestViewModel.AppName);
        }
        public void DeployToDevice()
        {
            string phonePath = WinManifestViewModel.BuildPath +
                @"\CreateAppx\Appzillon.WindowsPhone\Appzillon.WindowsPhone_" +
                WinManifestViewModel.Version +
                @"_Bundle\Appzillon.WindowsPhone_" +
                WinManifestViewModel.Version + @"_arm.appx";
            if (File.Exists(phonePath))
            {
                string cmd = @"cd ""C:\Program Files (x86)\Microsoft SDKs\Windows Phone\v8.1\Tools\AppDeploy""
AppDeployCmd /installlaunch " + phonePath + @" /targetdevice:de";
                ExecuteCMD.Execute(cmd, DDcallback);
            }
        }
        private void DDcallback(string name, string status) 
        {
            //Console.WriteLine(name+ "  "+status);
        }
        private string GetProjectPath(string OS)
        {
            return WinManifestViewModel.BuildPath + @"\Appzillon\Appzillon." + OS + @"\Appzillon." + OS + ".jsproj";        
        }
    }
}
