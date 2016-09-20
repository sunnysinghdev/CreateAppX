using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CreateAppX.Model
{
    public class ExecuteCMD
    {
        public static void Execute(string cmd) 
        {
            Execute(cmd, null, "TYPE1");
        }
        public static void Execute(string cmd, Action<string, string> _callback)
        {
            Execute(cmd, _callback, "TYPE2");
        }
        public static void Execute(string cmd, Action<string, string> callback, string processName)
        {
            try
            {
                
                Console.WriteLine(cmd);
                using (Process p = new Process())
                {
                    Action<string, string> _callback = callback;
                    string platform = processName;
                    p.StartInfo = new ProcessStartInfo("cmd.exe");
                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.WorkingDirectory = @"C:\Program Files (x86)\MSBuild\12.0\Bin";
                   // p.StartInfo.WorkingDirectory = @"C:\Program Files (x86)\Microsoft SDKs\Windows Phone\v8.1\Tools\AppDeploy";
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardOutput = true;
                   
                    p.Exited += (sender, e) => { p_Exited(sender,e,platform,_callback); };
                   // p.StartInfo.RedirectStandardError = true;
                    // event handlers for output & error
                    if (_callback != null)
                    {
                        p.OutputDataReceived += (sender, e) => { P_OutputDataReceived(sender, e, platform, _callback); };
                        p.ErrorDataReceived += P_ErrorDataReceived;
                    }
                    // p.StartInfo.Arguments = "/C msbuild";
                    // start process
                    p.Start();

                    if (_callback != null)
                    {
                        p.BeginOutputReadLine();
                       // p.BeginErrorReadLine();
                    }
                   // Thread.Sleep(5000);
                   // p.StandardInput.WriteLine("ipconfig");
                    p.StandardInput.WriteLine(cmd);
                  
                    p.StandardInput.WriteLine("exit");
                   
                    // Console.WriteLine(p.StandardOutput.ReadToEnd());
               
                    p.WaitForExit();
                    Console.WriteLine("--------------------" + platform+" Proccess ended.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void p_Exited(object sender, EventArgs e, string plt, Action<string,string> callback)
        {
            //callback.Invoke(plt, "Completed");
        }

        private static void P_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Process p = sender as Process;
            if (p == null)
                return;
            Console.WriteLine("Erorr = " + e.Data);
        }

        private static void P_OutputDataReceived(object sender, DataReceivedEventArgs e, string plt, Action<string,string> callback)
        {
            Process p = sender as Process;
            if (p == null)
                return;
            if (!String.IsNullOrEmpty(e.Data))
            {
                Console.WriteLine("Ouput = " + e.Data);
                callback.Invoke(plt, e.Data);
                if (e.Data.IndexOf(@"C:\Program Files (x86)\MSBuild\12.0\Bin>exit") >= 0)
                {
                    callback.Invoke(plt, "Completed");
                }

            }
        }

    }
}
