using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CreateAppX.Model;
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
        public void BuildProject(CommandType t)
        {
            if (t.Platform == "Surface" || t.Platform == "Phone")
            {
                string cmd = "msbuild \"" + GetProjectPath(t.OS) + @""" /t:rebuild /p:configuration=release;platform=arm;outdir=CreateAppx";
                Console.WriteLine(cmd);
                ExecuteCMD.Execute(cmd);
             //   new Thread(() => ExecuteCMD.Execute(cmd)).Start();
            }
            else if (t.Platform=="Desktop" || t.Platform == "PhoneSimulator")
            {
                string cmd = "msbuild \"" + GetProjectPath(t.OS) + @""" /t:rebuild /p:configuration=release;platform=x64;outdir=CreateAppx";
              //  new Thread(() => ExecuteCMD.Execute(cmd)).Start();
            }
            else
            {

            }
        }
        private string GetProjectPath(string OS)
        {
            string cmd = WinManifestViewModel.BuildPath + @"\Appzillon\Appzillon." + OS + @"\Appzillon." + OS + ".jsproj";
            return cmd;
        }
    }
}
