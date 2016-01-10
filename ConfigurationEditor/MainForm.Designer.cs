namespace ConfigurationEditor
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabConfig = new System.Windows.Forms.TabControl();
            this.tabServerConfig = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.edDbName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.edDbAddress = new System.Windows.Forms.TextBox();
            this.edDbTimeout = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.edDbPass = new System.Windows.Forms.TextBox();
            this.edDbUser = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.edDbPort = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.edServerAddress = new System.Windows.Forms.TextBox();
            this.edSessionTimeout = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.edServerPort = new System.Windows.Forms.MaskedTextBox();
            this.radioBtnHttps = new System.Windows.Forms.RadioButton();
            this.radioBtnHttp = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabDeployConfig = new System.Windows.Forms.TabPage();
            this.edDeployDetail = new System.Windows.Forms.TextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.关于AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tabConfig.SuspendLayout();
            this.tabServerConfig.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabDeployConfig.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabConfig
            // 
            this.tabConfig.Controls.Add(this.tabServerConfig);
            this.tabConfig.Controls.Add(this.tabDeployConfig);
            this.tabConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabConfig.Location = new System.Drawing.Point(0, 25);
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.SelectedIndex = 0;
            this.tabConfig.Size = new System.Drawing.Size(779, 440);
            this.tabConfig.TabIndex = 0;
            // 
            // tabServerConfig
            // 
            this.tabServerConfig.Controls.Add(this.btnSave);
            this.tabServerConfig.Controls.Add(this.groupBox2);
            this.tabServerConfig.Controls.Add(this.groupBox1);
            this.tabServerConfig.Location = new System.Drawing.Point(4, 22);
            this.tabServerConfig.Name = "tabServerConfig";
            this.tabServerConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabServerConfig.Size = new System.Drawing.Size(771, 414);
            this.tabServerConfig.TabIndex = 0;
            this.tabServerConfig.Text = "服务器配置";
            this.tabServerConfig.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(282, 346);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(204, 44);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存配置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.edDbName);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.edDbAddress);
            this.groupBox2.Controls.Add(this.edDbTimeout);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.edDbPass);
            this.groupBox2.Controls.Add(this.edDbUser);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.edDbPort);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(282, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(472, 303);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据库服务器";
            // 
            // edDbName
            // 
            this.edDbName.Location = new System.Drawing.Point(19, 161);
            this.edDbName.Name = "edDbName";
            this.edDbName.Size = new System.Drawing.Size(211, 21);
            this.edDbName.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 141);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 18;
            this.label10.Text = "数据库名称：";
            // 
            // edDbAddress
            // 
            this.edDbAddress.Location = new System.Drawing.Point(19, 51);
            this.edDbAddress.Name = "edDbAddress";
            this.edDbAddress.Size = new System.Drawing.Size(211, 21);
            this.edDbAddress.TabIndex = 17;
            // 
            // edDbTimeout
            // 
            this.edDbTimeout.Location = new System.Drawing.Point(252, 161);
            this.edDbTimeout.Mask = "99999";
            this.edDbTimeout.Name = "edDbTimeout";
            this.edDbTimeout.Size = new System.Drawing.Size(210, 21);
            this.edDbTimeout.TabIndex = 16;
            this.edDbTimeout.ValidatingType = typeof(int);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(250, 141);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "连接超时：";
            // 
            // edDbPass
            // 
            this.edDbPass.Location = new System.Drawing.Point(251, 103);
            this.edDbPass.Name = "edDbPass";
            this.edDbPass.PasswordChar = '*';
            this.edDbPass.Size = new System.Drawing.Size(211, 21);
            this.edDbPass.TabIndex = 14;
            // 
            // edDbUser
            // 
            this.edDbUser.Location = new System.Drawing.Point(251, 51);
            this.edDbUser.Name = "edDbUser";
            this.edDbUser.Size = new System.Drawing.Size(211, 21);
            this.edDbUser.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(250, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "密码：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(250, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "用户名：";
            // 
            // edDbPort
            // 
            this.edDbPort.Location = new System.Drawing.Point(19, 104);
            this.edDbPort.Mask = "99999";
            this.edDbPort.Name = "edDbPort";
            this.edDbPort.Size = new System.Drawing.Size(210, 21);
            this.edDbPort.TabIndex = 10;
            this.edDbPort.ValidatingType = typeof(int);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "数据库端口：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "数据库服务器IP：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.edServerAddress);
            this.groupBox1.Controls.Add(this.edSessionTimeout);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.edServerPort);
            this.groupBox1.Controls.Add(this.radioBtnHttps);
            this.groupBox1.Controls.Add(this.radioBtnHttp);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 303);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务器设置";
            // 
            // edServerAddress
            // 
            this.edServerAddress.Location = new System.Drawing.Point(19, 104);
            this.edServerAddress.Name = "edServerAddress";
            this.edServerAddress.Size = new System.Drawing.Size(211, 21);
            this.edServerAddress.TabIndex = 14;
            // 
            // edSessionTimeout
            // 
            this.edSessionTimeout.Location = new System.Drawing.Point(19, 216);
            this.edSessionTimeout.Mask = "99999";
            this.edSessionTimeout.Name = "edSessionTimeout";
            this.edSessionTimeout.Size = new System.Drawing.Size(210, 21);
            this.edSessionTimeout.TabIndex = 8;
            this.edSessionTimeout.ValidatingType = typeof(int);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 196);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(155, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "Session 过期时间 (分钟)：";
            // 
            // edServerPort
            // 
            this.edServerPort.Location = new System.Drawing.Point(19, 161);
            this.edServerPort.Mask = "99999";
            this.edServerPort.Name = "edServerPort";
            this.edServerPort.Size = new System.Drawing.Size(210, 21);
            this.edServerPort.TabIndex = 6;
            this.edServerPort.ValidatingType = typeof(int);
            // 
            // radioBtnHttps
            // 
            this.radioBtnHttps.AutoSize = true;
            this.radioBtnHttps.Location = new System.Drawing.Point(117, 51);
            this.radioBtnHttps.Name = "radioBtnHttps";
            this.radioBtnHttps.Size = new System.Drawing.Size(53, 16);
            this.radioBtnHttps.TabIndex = 4;
            this.radioBtnHttps.Text = "HTTPS";
            this.radioBtnHttps.UseVisualStyleBackColor = true;
            // 
            // radioBtnHttp
            // 
            this.radioBtnHttp.AutoSize = true;
            this.radioBtnHttp.Checked = true;
            this.radioBtnHttp.Location = new System.Drawing.Point(19, 51);
            this.radioBtnHttp.Name = "radioBtnHttp";
            this.radioBtnHttp.Size = new System.Drawing.Size(47, 16);
            this.radioBtnHttp.TabIndex = 3;
            this.radioBtnHttp.TabStop = true;
            this.radioBtnHttp.Text = "HTTP";
            this.radioBtnHttp.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "监听端口：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "服务器IP：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "模式：";
            // 
            // tabDeployConfig
            // 
            this.tabDeployConfig.Controls.Add(this.edDeployDetail);
            this.tabDeployConfig.Controls.Add(this.btnExport);
            this.tabDeployConfig.Location = new System.Drawing.Point(4, 22);
            this.tabDeployConfig.Name = "tabDeployConfig";
            this.tabDeployConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabDeployConfig.Size = new System.Drawing.Size(771, 414);
            this.tabDeployConfig.TabIndex = 1;
            this.tabDeployConfig.Text = "用户部署配置";
            this.tabDeployConfig.UseVisualStyleBackColor = true;
            // 
            // edDeployDetail
            // 
            this.edDeployDetail.Location = new System.Drawing.Point(178, 68);
            this.edDeployDetail.Multiline = true;
            this.edDeployDetail.Name = "edDeployDetail";
            this.edDeployDetail.Size = new System.Drawing.Size(422, 180);
            this.edDeployDetail.TabIndex = 1;
            this.edDeployDetail.Text = "用户部署配置是客户端连接至服务器时的必要配置，该配置包含远程服务器的 IP 地址和端口信息。服务器配置完毕后，请导出用户部署配置文件 config.xml ，并将" +
    "其覆盖至客户端根目录下。";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(302, 292);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(170, 58);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "导出用户部署配置文件";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.关于AToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(779, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuExit});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(115, 22);
            this.menuExit.Text = "退出(&E)";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // 关于AToolStripMenuItem
            // 
            this.关于AToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAbout});
            this.关于AToolStripMenuItem.Name = "关于AToolStripMenuItem";
            this.关于AToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.关于AToolStripMenuItem.Text = "帮助(&H)";
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(116, 22);
            this.menuAbout.Text = "关于(&A)";
            this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 465);
            this.Controls.Add(this.tabConfig);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配置工具";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabConfig.ResumeLayout(false);
            this.tabServerConfig.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabDeployConfig.ResumeLayout(false);
            this.tabDeployConfig.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabConfig;
        private System.Windows.Forms.TabPage tabServerConfig;
        private System.Windows.Forms.TabPage tabDeployConfig;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem 关于AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioBtnHttps;
        private System.Windows.Forms.RadioButton radioBtnHttp;
        private System.Windows.Forms.MaskedTextBox edServerPort;
        private System.Windows.Forms.MaskedTextBox edDbPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox edDbUser;
        private System.Windows.Forms.TextBox edDbPass;
        private System.Windows.Forms.MaskedTextBox edDbTimeout;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox edSessionTimeout;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TextBox edDeployDetail;
        private System.Windows.Forms.TextBox edDbAddress;
        private System.Windows.Forms.TextBox edServerAddress;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox edDbName;
    }
}