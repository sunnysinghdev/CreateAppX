using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAppX
{
    class BuildCommand
    {
        static int line = 0;
        static string filePath = @"F:\DevArea\SamStoreApp\SamStoreApp\SamStoreApp.Windows\SamStoreApp.Windows.jsproj";
        public static void Execute()
        {
            try
            {
                
                using (Process p = new Process())
                {
                    // set start info
                    p.StartInfo = new ProcessStartInfo("cmd.exe");

                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.WorkingDirectory = @"C:\Program Files (x86)\MSBuild\12.0\Bin";
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    // event handlers for output & error
                    p.OutputDataReceived += P_OutputDataReceived;
                    p.ErrorDataReceived += P_ErrorDataReceived;
                   // p.StartInfo.Arguments = "/C msbuild";
                    // start process
                    p.Start();
                    
                    p.BeginOutputReadLine();
                    p.BeginErrorReadLine();

                    p.StandardInput.WriteLine("msbuild " + filePath + @" /t:rebuild /p:configuration=release;platform=x64;outdir=new");
                    p.StandardInput.WriteLine("msbuild " + filePath + @" /t:rebuild /p:configuration=release;platform=arm;outdir=new");

                    p.StandardInput.WriteLine("exit");

                    // Console.WriteLine(p.StandardOutput.ReadToEnd());
                    // send command to its input

                    //p.BeginOutputReadLine();
                    //wait
                    while (!p.HasExited) {

                    }
                    //p.WaitForExit();
                    Console.WriteLine("No of Line = "+line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void P_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Process p = sender as Process;
            if (p == null)
                return;
            Console.WriteLine("Erorr = " + e.Data);
        }

        private static void P_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Process p = sender as Process;
            if (p == null)
                return;
            if (!String.IsNullOrEmpty(e.Data))
            {
                Console.WriteLine("Ouput = " + e.Data);
                line++;
            }
        }

        
    }

}
    

