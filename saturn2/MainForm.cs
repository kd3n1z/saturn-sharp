using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            SaveProps();
        }

        void SaveProps()
        {
            // TODO
        }
    }
}
