using ConfigurationLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigurationEditor
{
    public partial class MainForm : Form
    {
        private UserConfiguration mConfig;
        private ConfigurationLoader mConfigurationLoader;

        private DeployConfigurationLoader mDeployConfigLoader;

        public MainForm()
        {
            InitializeComponent();

            mConfigurationLoader = new ConfigurationLoader();
            mDeployConfigLoader = new DeployConfigurationLoader();
        }

        public void init()
        {
            mConfig = mConfigurationLoader.Load();

            // Server configuration
            if (mConfig.ServerInfo.Scheme.ToUpper() != "HTTP")
            {
                radioBtnHttp.Checked = false;
                radioBtnHttps.Checked = true;
            }

            edServerAddress.Text = mConfig.ServerInfo.Hostname;
            edServerPort.Text = mConfig.ServerInfo.Port + "";
            edSessionTimeout.Text = mConfig.SessionLifetime + "";

            // Database configuration
            edDbAddress.Text = mConfig.DatabaseInfo.Host;
            edDbPort.Text = mConfig.DatabaseInfo.Port + "";
            edDbUser.Text = mConfig.DatabaseInfo.Username;
            edDbPass.Text = mConfig.DatabaseInfo.Password;
            edDbName.Text = mConfig.DatabaseInfo.Database;
            edDbTimeout.Text = mConfig.DatabaseInfo.Timeout + "";
        }

        public void Save()
        {
            if (mConfig == null)
            {
                mConfig = new UserConfiguration();
            }

            string serverAddr = edServerAddress.Text;
            string serverPort = edServerPort.Text;
            string serverScheme = radioBtnHttp.Checked ? "HTTP" : "HTTPS";

            string dbAddr = edDbAddress.Text;
            string dbPort = edDbPort.Text;
            string dbUser = edDbUser.Text;
            string dbPass = edDbPass.Text;
            string dbName = edDbName.Text;
            string dbTimeout = edDbTimeout.Text;

            string sessionLifetime = edSessionTimeout.Text;

            // Invalid input
            if (CommonHelper.isEmpty(serverAddr, serverPort, serverScheme,
                dbAddr, dbPort, dbUser, dbPass, dbName, dbTimeout,
                sessionLifetime))
            {
                return;
            }

            if (! CommonHelper.inRange(serverPort, 0, 65535) ||
                ! CommonHelper.inRange(dbPort, 0, 65535) )
            {
                CommonHelper.ShowMessage(this, "无效端口范围！");
                return;
            }

            if (! CommonHelper.inRange(dbTimeout, 0, 600))
            {
                CommonHelper.ShowMessage(this, "数据库连接超时范围为 0-600s！");
                return;
            }

            if (! CommonHelper.inRange(sessionLifetime, 0, 1024000))
            {
                CommonHelper.ShowMessage(this, "会话过期时间范围为 0-1024000m！");
                return;
            }

            mConfig.ServerInfo.Scheme = serverScheme;
            mConfig.ServerInfo.Hostname = serverAddr;
            mConfig.ServerInfo.Port = (ushort) CommonHelper.StrToInt(serverPort);

            mConfig.DatabaseInfo.Host = dbAddr;
            mConfig.DatabaseInfo.Port = (ushort) CommonHelper.StrToInt(dbPort);
            mConfig.DatabaseInfo.Username = dbUser;
            mConfig.DatabaseInfo.Password = dbPass;
            mConfig.DatabaseInfo.Database = dbName;
            mConfig.DatabaseInfo.Timeout = CommonHelper.StrToInt(dbTimeout);

            bool bRet = mConfigurationLoader.Save(true, CommonHelper.StrToInt(sessionLifetime),
                mConfig.ServerInfo, mConfig.DatabaseInfo);

            CommonHelper.ShowMessage(this, bRet ?
                "配置已保存，需重启服务器才能生效！" :
                "配置保存失败，请重试！");
        }

        private void ExportDeployConfig()
        {
            bool bSave;
            string fn = "config.xml";

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "XML File (*.xml)|*.xm;";
            dialog.FileName = fn;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fn = dialog.FileName;

                bSave = mDeployConfigLoader.Export(mConfig.ServerInfo, fn);

                CommonHelper.ShowMessage(this, bSave ?
                    "用户部署配置文件导出成功！\n请将其复制至客户端根目录下" : 
                    "配置文件导出失败，请重试！");
            }
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            CommonHelper.ShowMessage(this, "CourseSystem Configuration Tool\n\nAuthor: Soxfmr@foxmail.com" +
                "\n\nCopyright 2016 All Right Reserved");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            init();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportDeployConfig();
        }
    }
}
