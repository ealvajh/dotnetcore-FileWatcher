using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace FolderWatcher.Utilities
{
    internal static class Root
    {
        public static void CreateRootFolder()
        {
            string[] folders = { @"C:\ExcelFiles", @"C:\ExcelFiles\Processed", @"C:\ExcelFiles\Not applicable" } ;

            foreach (var folder in folders)
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                
            }
           
            CreateExcelFile();
        }

        public static void CreateExcelFile()
        {
            var masterPath = @"C:\ExcelFiles\Master.xlsx";

            if (!File.Exists(masterPath))
            {
                var app = new Excel.Application() { Visible = false };
                var master = app.Workbooks.Add();

                master.SaveAs2(masterPath);
                master.Close();
            }
        }
    }
}
