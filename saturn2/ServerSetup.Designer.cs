namespace saturn
{
    partial class ServerSetup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.jarVer = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.javaPath = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Corbel", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(351, 57);
            this.label1.TabIndex = 3;
            this.label1.Text = "serverName setup";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // jarVer
            // 
            this.jarVer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.jarVer.FormattingEnabled = true;
            this.jarVer.Location = new System.Drawing.Point(12, 66);
            this.jarVer.Name = "jarVer";
            this.jarVer.Size = new System.Drawing.Size(327, 21);
            this.jarVer.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(12, 221);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(327, 42);
            this.button1.TabIndex = 5;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // javaPath
            // 
            this.javaPath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.javaPath.FormattingEnabled = true;
            this.javaPath.Location = new System.Drawing.Point(12, 93);
            this.javaPath.Name = "javaPath";
            this.javaPath.Size = new System.Drawing.Size(327, 21);
            this.javaPath.TabIndex = 6;
            // 
            // ServerSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 275);
            this.Controls.Add(this.javaPath);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.jarVer);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ServerSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "saturn - server setup";
            this.Load += new System.EventHandler(this.ServerSetup_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox jarVer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox javaPath;
    }
}