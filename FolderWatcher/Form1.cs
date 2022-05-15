using FolderWatcher.Utilities;

namespace FolderWatcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            string folderPath = FolderBrowser.GetFolderPath();
            lbl1.Text = folderPath;
            Watcher.Init(lstBox, folderPath);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Root.CreateRootFolder();
        }
    }
}