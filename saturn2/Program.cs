using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace saturn2
{
    internal static class Program
    {
        public static int build = 1;

        public static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "saturn");
        public static List<string> versions = new List<string>();
        public static List<string> javas = new List<string>();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(Path.Combine(path, "servers"));
                Directory.CreateDirectory(Path.Combine(path, "server-jars"));
                new FirstStart().ShowDialog();
            }
            try
            {
                string mcversions = new WebClient().DownloadString("https://mcversions.net/");

                foreach (string html in mcversions.Split(new string[] { "<p class=\"text-xl leading-snug font-semibold\">" }, StringSplitOptions.None))
                {
                    if (html.Contains("<br>"))
                    {
                        string ver = html.Substring(0, html.IndexOf("<br>"));
                        if (ver.Contains("<span"))
                        {
                            ver = ver.Remove(0, ver.IndexOf("</span>") + "</span>".Length);
                        }
                        versions.Add(ver);
                    }
                }
            }
            catch
            {
                MessageBox.Show("unable to fetch versions from https://mcversions.net/", "saturn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            ServerSelector selector = new ServerSelector();
            if (selector.ShowDialog() == DialogResult.OK)
            {
                MainForm mf = new MainForm();
                mf.server = selector.server;
                mf.ShowDialog();
            }
        }
    }
}
