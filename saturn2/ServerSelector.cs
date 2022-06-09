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
            foreach(string dir in Directory.GetDirectories(Path.Combine(Program.path, "servers")))
            {
                Button b = new Button();
                b.Width = flowLayoutPanel1.Width;
                b.Click += B_Click;
                b.Text = dir.Split(new string[] { "/", "\\" }, StringSplitOptions.RemoveEmptyEntries).Last();
                b.Parent = flowLayoutPanel1;
            }
        }

        private void B_Click(object sender, EventArgs e)
        {
            server = ((Button)sender).Text;
            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
