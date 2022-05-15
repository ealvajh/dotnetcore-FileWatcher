using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderWatcher.Utilities
{
    internal static class Watcher
    {
        static string msg = string.Empty;

        static ListBox? lstBox;

        public static void Init(ListBox lstBox1, string folderPath)
        {            
            lstBox = lstBox1;
            OnWatcher(folderPath);
        }

        public static void OnWatcher(string path)
        {
            Console.WriteLine($"Watching: {path}");

            var watcher = new FileSystemWatcher(path);

            watcher.NotifyFilter = NotifyFilters.Attributes
                                | NotifyFilters.CreationTime
                                | NotifyFilters.DirectoryName
                                | NotifyFilters.FileName
                                | NotifyFilters.LastAccess
                                | NotifyFilters.LastWrite
                                | NotifyFilters.Security
                                | NotifyFilters.Size;

            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
            watcher.Error += OnError;

            watcher.Filter = "*.*";
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
        }


        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            Console.WriteLine($"Changed: {e.FullPath}");
            PrintAction($"Changed: {e.FullPath}");
        }


        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Created: {e.FullPath}";

            FolderAction.MoveFile(e.FullPath);

            PrintAction(value);
            Console.WriteLine(value);
        }


        private static void OnDeleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"Deleted: {e.FullPath}");
            PrintAction($"Deleted: {e.FullPath}");
        }


        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            string msg = $"Renamed:" +
                         $"    Old: {e.OldFullPath}" +
                         $"    New: {e.FullPath}";
            PrintAction(msg);
        }


        // Exceptions
        private static void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        private static void PrintException(Exception? ex)
        {
            if (ex != null)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Stacktrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                PrintException(ex.InnerException);
            }
        }

        // Printer
        private static void PrintAction(string action)
        {
            msg = action + Environment.NewLine;
            lstBox?.Invoke(() => lstBox.Items.Add(msg));
        }

    }
}
