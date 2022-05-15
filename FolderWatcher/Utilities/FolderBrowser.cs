using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderWatcher.Utilities
{
    internal static class FolderBrowser
    {
        public static string GetFolderPath()
        {
            string selectedPath = String.Empty;

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Custom Description";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                selectedPath = fbd.SelectedPath;
            }
            else
            {
                selectedPath = "We could not get the path.";
            }

            return selectedPath;
        }
    }
}
