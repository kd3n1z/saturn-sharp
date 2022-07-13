using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace saturn
{
    public partial class ServerSetup : Form
    {
        public string serverName = "";

        public ServerSetup()
        {
            InitializeComponent();
        }

        private void ServerSetup_Load(object sender, EventArgs e)
        {
            label1.Text = serverName + " setup";
            foreach(string ver in Program.versions)
            {
                jarVer.Items.Add(ver);
            }
            foreach(string path in Program.javas)
            {
                javaPath.Items.Add(path);
            }
            jarVer.Text = jarVer.Items[0].ToString();
            javaPath.Text = javaPath.Items[0].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string serverDir = Path.Combine(Program.path, "servers", serverName);

            Directory.CreateDirectory(serverDir);

            SettingsFile sf = new SettingsFile();

            sf["core"] = jarVer.Text + ".jar";
            sf["startArgs"] = "-Xmx%memM -Xms%memM -jar %core nogui";
            sf["mem"] = "1024";

            string javabin = javaPath.Text.Remove(0, javaPath.Text.IndexOf("(") + 1);
            javabin = javabin.Remove(javabin.Length - 1, 1);

            sf["java"] = javabin;

            sf["ngrokToken"] = "YOUR_NGROK_TOKEN_HERE";
            sf["ngrokEnabled"] = "false";
            sf["ngrokArgs"] = "--log stdout --region eu --authtoken %token tcp %port";

            File.WriteAllText(Path.Combine(serverDir, "saturn-config.txt"), sf.ToString());

            if (!File.Exists(Path.Combine(Program.path, "server-jars", jarVer.Text + ".jar")))
            {
                label1.Text = "downloading server.jar...";
                DownloadJar(jarVer.Text);
            }

            label1.Text = "first start...";
            Process p = new Process();
            p.StartInfo.FileName = sf["java"];
            p.StartInfo.Arguments = "-Xmx1024M -Xms1024M -jar " + Path.Combine(Program.path, "server-jars", sf["core"]) + " nogui";
            p.StartInfo.WorkingDirectory = serverDir;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();

            if(p.ExitCode == 1)
            {
                MessageBox.Show("Java exited with code 1. It looks like you're using incorrect Java version for this server version (for example, server v1.19 requires Java v17.0)", "saturn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            File.WriteAllText(Path.Combine(serverDir, "eula.txt"), "eula=true");
            Close();
        }

        void DownloadJar(string ver)
        {
            new WebClient().DownloadFile(GetJarUrl(ver), Path.Combine(Program.path, "server-jars", ver + ".jar"));
            Invoke(new MethodInvoker(() =>
            {
                Close();
            }));
        }

        private string GetJarUrl(string ver)
        {
            string html = new WebClient().DownloadString("https://mcversions.net/download/" + ver);
            html = html.Substring(0, html.IndexOf("server.jar") + "server.jar".Length);
            html = html.Remove(0, html.LastIndexOf("href=\"") + "href=\"".Length);
            return html;
        }
    }
}
