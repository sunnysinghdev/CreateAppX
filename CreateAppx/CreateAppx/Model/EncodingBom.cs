using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAppX.ViewModel
{
    class EncodingBom
    {
        public static bool Completed = false;
        public static void Convert(string path)
        {
            var utf8WithBOM = new UTF8Encoding(true);
            foreach (string newPath in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
            {
                Completed = false;
                try
                {
                    if (newPath.Contains(".js") || newPath.Contains(".html") || newPath.Contains(".css") || newPath.Contains(".json"))
                    {
                        File.SetAttributes(newPath, FileAttributes.Normal);
                        var content = File.ReadAllLines(newPath);
                        Console.WriteLine("Converting the file '" + newPath + "'...");
                        File.WriteAllLines(newPath, content, utf8WithBOM);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Completed = true;
        }
    }
}
