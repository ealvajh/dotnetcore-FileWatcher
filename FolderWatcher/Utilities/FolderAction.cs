using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderWatcher.Utilities
{
    internal static class FolderAction
    {
        const string processed = @"C:\ExcelFiles\Processed\";
        const string notApplicable = @"C:\ExcelFiles\Not applicable\";

        public static void MoveFile(string path)
        {
            string extension = Path.GetExtension(path);

            try
            {

                if (extension == ".xlsx")
                {
                    string fileName = RenamedFile(processed, path);
                    string completePath = processed + fileName;
                  
                    File.Move(path, completePath);
                    ExcelFile.Merge(completePath);

                    MessageBox.Show("File " + fileName + " was moved to -Processed- file and merge.");
                }
                else
                {
                    string fileName = RenamedFile(notApplicable, path);
                    string completePath = notApplicable + fileName;

                    File.Move(path, completePath);
                    MessageBox.Show("File " + fileName + " was moved to -Not Applicable- file.");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }


        public static string RenamedFile(string folderPath, string path)
        {
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(path);
            string fileName = nameWithoutExtension + extension;

            if (File.Exists(folderPath + fileName))
            {
                string newFileName = nameWithoutExtension += "(" + DateTime.Now.Millisecond + ")" + extension;
                return newFileName;
            }
            else
            {
                return fileName;
            }
        }

    }
}
