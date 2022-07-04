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
    public partial class ServerSelector : Form
    {
        public string server = "";
        public ServerSelector()
        {
            InitializeComponent();
        }

        private void FirstStart_Load(object sender, EventArgs e)
        {
            RefreshServerList();
        }

        private void RefreshServerList()
        {
            flowLayoutPanel1.Controls.Clear();

            foreach (string dir in Directory.GetDirectories(Path.Combine(Program.path, "servers")))
            {
                Button b = new Button();
                b.Width = flowLayoutPanel1.Width - 6;
                b.Click += B_Click;
                b.Text = dir.Split(new string[] { "/", "\\" }, StringSplitOptions.RemoveEmptyEntries).Last();
                b.Parent = flowLayoutPanel1;
            }


            int i = 1;
            while (Directory.Exists(Path.Combine(Program.path, "servers", textBox1.Text)))
            {
                textBox1.Text = "serverName" + i;
                i++;
            }
        }

        private void B_Click(object sender, EventArgs e)
        {
            server = ((Button)sender).Text;
            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServerSetup ss = new ServerSetup();
            ss.serverName = textBox1.Text;
            ss.ShowDialog();
            RefreshServerList();
        }
    }
}
