using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace saturn2
{
    public partial class FirstStart : Form
    {
        public FirstStart()
        {
            InitializeComponent();
        }

        private void FirstStart_Load(object sender, EventArgs e)
        {
            new Thread(Install).Start();
        }

        public void Install()
        {
            if (Environment.Is64BitOperatingSystem)
            {
                Download("https://bin.equinox.io/c/bNyj1mQVY4c/ngrok-v3-stable-windows-amd64.zip", "ngrok", true);
            }
            else
            {
                Download("https://bin.equinox.io/c/bNyj1mQVY4c/ngrok-v3-stable-windows-386.zip", "ngrok", true);
            }

            Invoke(new MethodInvoker(() =>
            {
                Close();
            }));
        }

        public void Download(string url, string name, bool unzip = false)
        {
            label2.Invoke(new MethodInvoker(() =>
            {
                label2.Text = "downloading " + name + "...";
            }));

            if (unzip)
            {
                string dir = Path.Combine(Program.path, name);

                Directory.CreateDirectory(dir);

                string tempFile = Path.Combine(dir, "temp.zip");

                new WebClient().DownloadFile(url, tempFile);
                ZipFile.ExtractToDirectory(tempFile, dir);

                File.Delete(tempFile);
            }
            else
            {
                new WebClient().DownloadFile(url, name);
            }
        }
    }
}
