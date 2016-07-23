using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using 云大选课辅助工具.Properties;
namespace 云大选课辅助工具
{
    internal class FormAbout : Form
    {
        private Button ButtonQQ;
        private Label labelCompanyName;
        private Label labelCopyright;
        private Label labelProductName;
        private Label labelVersion;
        private PictureBox logoPictureBox;
        private TableLayoutPanel tableLayoutPanel;
        private TextBox textBoxDescription;

        public FormAbout()
        {
            InitializeComponent();
            Text = String.Format("关于 {0}", AssemblyTitle);
            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = String.Format("版本 {0}",AssemblyVersion());
            labelCopyright.Text = String.Format("版权 {0}",AssemblyCopyright);
            labelCompanyName.Text = string.Format("来自 {0}",AssemblyCompany);
            textBoxDescription.Text = AssemblyDescription;
        }

        private void Button_Update_Click(object sender, EventArgs e)
        {
            Process.Start("http://jq.qq.com/?_wv=1027&k=dF2f4R");
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormAbout));
            tableLayoutPanel = new TableLayoutPanel();
            logoPictureBox = new PictureBox();
            labelProductName = new Label();
            labelVersion = new Label();
            labelCopyright = new Label();
            labelCompanyName = new Label();
            textBoxDescription = new TextBox();
            ButtonQQ = new Button();
            tableLayoutPanel.SuspendLayout();
            ((ISupportInitialize) logoPictureBox).BeginInit();
            SuspendLayout();
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33f));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67f));
            tableLayoutPanel.Controls.Add(logoPictureBox, 0, 0);
            tableLayoutPanel.Controls.Add(labelProductName, 1, 0);
            tableLayoutPanel.Controls.Add(labelVersion, 1, 1);
            tableLayoutPanel.Controls.Add(labelCopyright, 1, 2);
            tableLayoutPanel.Controls.Add(labelCompanyName, 1, 3);
            tableLayoutPanel.Controls.Add(textBoxDescription, 1, 4);
            tableLayoutPanel.Controls.Add(ButtonQQ, 1, 5);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(9, 8);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
            tableLayoutPanel.Size = new Size(0x1a1, 0xf5);
            tableLayoutPanel.TabIndex = 0;
            logoPictureBox.Dock = DockStyle.Fill;
            logoPictureBox.Image = (Image) manager.GetObject("logoPictureBox.Image");
            logoPictureBox.Location = new Point(3, 3);
            logoPictureBox.Name = "logoPictureBox";
            tableLayoutPanel.SetRowSpan(logoPictureBox, 6);
            logoPictureBox.Size = new Size(0x83, 0xef);
            logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            logoPictureBox.TabIndex = 12;
            logoPictureBox.TabStop = false;
            labelProductName.Dock = DockStyle.Fill;
            labelProductName.Location = new Point(0x8f, 0);
            labelProductName.Margin = new Padding(6, 0, 3, 0);
            labelProductName.MaximumSize = new Size(0, 0x10);
            labelProductName.Name = "labelProductName";
            labelProductName.Size = new Size(0x10f, 0x10);
            labelProductName.TabIndex = 0x13;
            labelProductName.Text = "产品名称";
            labelProductName.TextAlign = ContentAlignment.MiddleLeft;
            labelVersion.Dock = DockStyle.Fill;
            labelVersion.Location = new Point(0x8f, 0x18);
            labelVersion.Margin = new Padding(6, 0, 3, 0);
            labelVersion.MaximumSize = new Size(0, 0x10);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(0x10f, 0x10);
            labelVersion.TabIndex = 0;
            labelVersion.Text = "版本";
            labelVersion.TextAlign = ContentAlignment.MiddleLeft;
            labelCopyright.Dock = DockStyle.Fill;
            labelCopyright.Location = new Point(0x8f, 0x30);
            labelCopyright.Margin = new Padding(6, 0, 3, 0);
            labelCopyright.MaximumSize = new Size(0, 0x10);
            labelCopyright.Name = "labelCopyright";
            labelCopyright.Size = new Size(0x10f, 0x10);
            labelCopyright.TabIndex = 0x15;
            labelCopyright.Text = "版权";
            labelCopyright.TextAlign = ContentAlignment.MiddleLeft;
            labelCompanyName.Dock = DockStyle.Fill;
            labelCompanyName.Location = new Point(0x8f, 0x48);
            labelCompanyName.Margin = new Padding(6, 0, 3, 0);
            labelCompanyName.MaximumSize = new Size(0, 0x10);
            labelCompanyName.Name = "labelCompanyName";
            labelCompanyName.Size = new Size(0x10f, 0x10);
            labelCompanyName.TabIndex = 0x16;
            labelCompanyName.Text = "公司名称";
            labelCompanyName.TextAlign = ContentAlignment.MiddleLeft;
            textBoxDescription.Dock = DockStyle.Fill;
            textBoxDescription.Location = new Point(0x8f, 0x63);
            textBoxDescription.Margin = new Padding(6, 3, 3, 3);
            textBoxDescription.Multiline = true;
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.ReadOnly = true;
            textBoxDescription.ScrollBars = ScrollBars.Both;
            textBoxDescription.Size = new Size(0x10f, 0x74);
            textBoxDescription.TabIndex = 0x17;
            textBoxDescription.TabStop = false;
            textBoxDescription.Text = "说明";
            ButtonQQ.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            ButtonQQ.Location = new Point(0x153, 0xdd);
            ButtonQQ.Name = "ButtonQQ";
            ButtonQQ.Size = new Size(0x4b, 0x15);
            ButtonQQ.TabIndex = 0x18;
            ButtonQQ.Text = "提交反馈";
            ButtonQQ.Click += new EventHandler(Button_Update_Click);
            AcceptButton = ButtonQQ;
            AutoScaleDimensions = new SizeF(6f, 12f);
            ClientSize = new Size(0x1b3, 0x105);
            Controls.Add(tableLayoutPanel);
            Icon = Resources.school;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormAbout";
            Padding = new Padding(9, 8, 9, 8);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "FormAbout";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((ISupportInitialize) logoPictureBox).EndInit();
            ResumeLayout(false);
        }

        public string AssemblyCompany
        {
            get
            {
                object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (customAttributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute) customAttributes[0]).Company;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (customAttributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute) customAttributes[0]).Copyright;
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (customAttributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute) customAttributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (customAttributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute) customAttributes[0]).Product;
            }
        }

        public string AssemblyTitle
        {
            get
            {
                object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (customAttributes.Length > 0)
                {
                    AssemblyTitleAttribute attribute = (AssemblyTitleAttribute) customAttributes[0];
                    if (attribute.Title != "")
                    {
                        return attribute.Title;
                    }
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }   
    }
}

