using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateAppX.ViewModel
{
    class FileBrowser
    {
        public static string FolderPath = null;
        public static string GetPath()
        {
            var dialog = new FolderBrowserDialog();

            // dialog.SelectedPath = FolderPath;
            // CurrentDirectory;// FolderPath;
            if (FolderPath != null) {
               // dialog.SelectedPath = FolderPath;
            }
            dialog.ShowDialog();
            FolderPath = dialog.SelectedPath;
            return FolderPath;
        }
    }
}
