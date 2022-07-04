using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace saturn2
{
    public partial class MainForm : Form
    {
        Process p = null;

        public string server = "";

        string serverPath = "";

        SettingsFile sf;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            serverPath = Path.Combine(Program.path, "servers", server);
            label1.Text += server;

            sf = new SettingsFile(File.ReadAllText(Path.Combine(serverPath, "saturn-config.txt")));

            try
            {
                foreach (string line in File.ReadAllText(Path.Combine(serverPath, "server.properties")).Replace("\r", "").Split('\n'))
                {
                    if (line.StartsWith("#") || string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    string propName = line.Split('=')[0];
                    string propVal = line.Remove(0, propName.Length + 1);

                    TextBox tb = new TextBox();
                    tb.Tag = propName;
                    tb.Text = propVal;
                    tb.Parent = propsPanel;
                    tb.MouseEnter += Tb_MouseEnter;
                }
            }
            catch { }

            propsPanel.Dock = DockStyle.Fill;
            consolePanel.Dock = DockStyle.Fill;
            consolePanel.Visible = false;
        }

        private void Tb_MouseEnter(object sender, EventArgs e)
        {
            label2.Text = ((Control)sender).Tag.ToString();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (p != null && !p.HasExited)
            {
                p.Kill();
            }
            SaveProps();
        }

        void SaveProps()
        {
            // TODO
        }

        private void button1_Click(object sender, EventArgs e)
        {
            propsPanel.Visible = true;
            consolePanel.Visible = false;
        }

        bool mouseDown;
        Point mouseOffsetForm;

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point senderLoc = ((Control)sender).Location;
                mouseDown = true;
                mouseOffsetForm = new Point(e.Location.X + senderLoc.X, e.Location.Y + senderLoc.Y);
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = false;
            }
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Point mPos = Control.MousePosition;
                Location = new Point(mPos.X - mouseOffsetForm.X, mPos.Y - mouseOffsetForm.Y);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            propsPanel.Visible = false;
            consolePanel.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveProps();
            if (p == null || !p.HasExited)
            {
                p = new Process();
                p.StartInfo.FileName = sf["java"];
                p.StartInfo.Arguments = sf["startArgs"].Replace("%mem", sf["mem"]).Replace("%core", "\"" + Path.Combine(Program.path, "server-jars", sf["core"]) + "\"");
                p.StartInfo.WorkingDirectory = serverPath;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.ErrorDataReceived += P_DataReceived;
                p.OutputDataReceived += P_DataReceived;
                p.Exited += P_Exited;
                p.Start();
                p.BeginErrorReadLine();
                p.BeginOutputReadLine();
            }

        }

        private void P_Exited(object sender, EventArgs e)
        {
            richTextBox1.Invoke(new MethodInvoker(() =>
            {
                richTextBox1.AppendText("[saturn] java exited\n");
            }));
        }

        private void P_DataReceived(object sender, DataReceivedEventArgs e)
        {
            richTextBox1.Invoke(new MethodInvoker(() =>
            {
                richTextBox1.AppendText(e.Data + "\n");
            }));
        }
    }
}
