using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;

namespace saturn
{
    internal static class Program
    {
        public static int build = 3;

        public static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "saturn");
        public static List<string> versions = new List<string>();
        public static List<string> javas = new List<string>();
        public static string ngrokPath = Path.Combine(path, "ngrok", "ngrok.exe");

        public static string generalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "kd3n1z-general");

        static bool updating = false;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!Directory.Exists(generalPath))
            {
                Directory.CreateDirectory(generalPath);
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(Path.Combine(path, "servers"));
                Directory.CreateDirectory(Path.Combine(path, "server-jars"));
                new FirstStart().ShowDialog();
            }

            CheckForUpdates();

            if (updating)
            {
                return;
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

            try
            {
                foreach (string java in Directory.GetDirectories(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Java")).Concat(Directory.GetDirectories(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).Replace(" (x86)", ""), "Java"))))
                {
                    string javabin = Path.Combine(java, "bin", "java.exe");
                    if (File.Exists(javabin))
                    {
                        string ver = "unknown";
                        try
                        {
                            Process p = new Process();
                            p.StartInfo.FileName = javabin;
                            p.StartInfo.Arguments = "-version";
                            p.StartInfo.RedirectStandardError = true;
                            p.StartInfo.CreateNoWindow = true;
                            p.StartInfo.UseShellExecute = false;
                            p.Start();
                            p.WaitForExit();
                            ver = "Java v" + p.StandardError.ReadLine().Split('"')[1];
                        }
                        catch { }
                        javas.Add(ver + " (" + javabin + ")");
                    }
                }
            }
            catch { }

            ServerSelector selector = new ServerSelector();
            while (selector.ShowDialog() == DialogResult.OK)
            {
                MainForm mf = new MainForm();
                mf.server = selector.server;
                mf.ShowDialog();
            }
        }

        public static void CheckForUpdates()
        {
            int latest = GetLatestVersion();

            try
            {
                if (latest > build)
                {
                    AskForUpdate();
                }
            }
            catch { }
        }
        public static void AskForUpdate()
        {
            if (MessageBox.Show("Update available! Do you want to update saturn?", "saturn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                updating = true;
                Update();
            }
        }

        public static void Update()
        {
            string updater = Path.Combine(generalPath, "updater.exe");

            if (!File.Exists(updater))
            {
                new WebClient().DownloadFile("https://github.com/KD3n1z/updater/releases/download/main/updater.exe", updater);
            }

            if (latestRelease == null)
            {
                latestRelease = ParseJsonFromUrl("https://api.github.com/repos/KD3n1z/saturn/releases/latest");
            }

            Process.Start(updater, "\"path=" + Process.GetCurrentProcess().MainModule.FileName + "\" \"url=" + latestRelease.XPathSelectElement("//assets").FirstNode.XPathSelectElement("//browser_download_url").Value + "\" \"app=saturn\"");
            Application.Exit();
        }

        static XElement latestRelease = null;

        public static int GetLatestVersion()
        {
            try
            {
                latestRelease = ParseJsonFromUrl("https://api.github.com/repos/KD3n1z/saturn/releases/latest");
                return int.Parse(latestRelease.XPathSelectElement("/name").Value.ToLower().Split(' ').Last());
            }
            catch
            {
                return 0;
            }
        }

        static XElement ParseJsonFromUrl(string url)
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "request");
            var jsonReader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(wc.DownloadString(url)), new System.Xml.XmlDictionaryReaderQuotas());
            return XElement.Load(jsonReader);
        }
    }
}
