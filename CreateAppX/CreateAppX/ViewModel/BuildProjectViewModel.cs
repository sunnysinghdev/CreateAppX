using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
           // "msbuild " + filePath + @" /t:rebuild /p:configuration=release;platform=x64;outdir=new"
        }
        private string GetCommand(CommandType t)
        {
            if (t.Platform=="Surface")
            {

            }
            string cmd = WinManifestViewModel.BuildPath + @"\Appzillon\Appzillon." + t.OS + @"\Appzillon." + t.OS + ".jsproj";
            return cmd;
        }
    }
}
