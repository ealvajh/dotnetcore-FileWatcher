using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace FolderWatcher.Utilities
{
    internal static class ExcelFile
    {
        public static void Merge(string excelFile)
        {
            var app = new Excel.Application() { Visible = false };
            var master = app.Workbooks.Open(@"C:\ExcelFiles\Master.xlsx");

            app.Workbooks.Add(excelFile)
              .Worksheets.Cast<Worksheet>()
              .ToList()
              .ForEach(x => x.Move(master.Sheets[1]));

            master.Close(true);
            app.Quit();

            Dispose();
        }

        public static void Dispose()
        {
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
            foreach (System.Diagnostics.Process p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName) && p.StartTime.AddSeconds(+10) > DateTime.Now)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }
        }
    }
}
