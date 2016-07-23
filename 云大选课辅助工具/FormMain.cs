using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using mshtml;
using Microsoft.Win32;
using 云大选课辅助工具.Properties;

namespace 云大选课辅助工具
{
    [ComVisible(true), PermissionSet(SecurityAction.Demand, Name = "FullTrust"), HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
    public class FormMain : Form
    {
        private IContainer components;
        private Button _buttonAbout;
        private Button _buttonEdit;
        private Button _buttonOneKey;
        private Button _buttonSelectCourse;
        private Button _buttonSet;
        private Button _buttonValid;
        private Button _buttonViewCourse;
        private ContextMenuStrip _cmsClearTextInf;
        private ContextMenuStrip _cmsClearTextMsg;
        private ColumnHeader _columnHeaderAdd;
        private ColumnHeader _columnHeaderId;
        private ColumnHeader _columnHeaderName;
        private ColumnHeader _columnHeaderStatus;
        private FlowLayoutPanel _flowLayoutPanelButtons;
        private GroupBox _groupBoxControl;
        private GroupBox _groupBoxCourse;
        private Panel _panelCourse;
        private Panel _panelTop;
        private SplitContainer _splitContainerBrower;
        private SplitContainer _splitContainerInf;
        private SplitContainer _splitContainerMain;
        private SplitContainer _splitContainerPanel;
        private SplitContainer _splitContainerTop;
        private StatusStrip _statusStripMain;
        private TextBox _textBoxInf;
        private TextBox _textBoxReturnMsg;
        private System.Windows.Forms.Timer _timerCp;
        private System.Windows.Forms.Timer _timerSelectCourse;
        private ToolStripMenuItem _toolStripMenuItem1;
        private ToolStripMenuItem _toolStripMenuItemClean;
        private ToolStripProgressBar _toolStripProgressBarProc;
        private ToolStripStatusLabel _toolStripStatusLabelInf;
        public ListView ListViewSelectCourse;
        private WebBrowser _webBrowserMain;
        private class SelectCode
        {
            public string captcha { get; set; }

            public string id { get; set; }

            [JsonIgnore]
            public string Name { get; set; }
        }

        public const string Title = "云大选课辅助工具";
        public const string UrpIp = "202.203.209.96";
        public const string ImgUrl = "http://" + UrpIp + "/vimgs/";
        public const string MainUrl = "http://" + UrpIp + "/v5";
        /// <summary> 错误JSON关键字.</summary>
        public const string ErrorDescription = "error_description";
        /// <summary> 是否登陆.</summary>
        public bool IsLogin;

        /// <summary> 相关URL.</summary>
        private const string LoginUrl = "http://" + UrpIp + "/v5api/OAuth/Token";
        private const string CourseUrl = "http://" + UrpIp + "/v5#/teachClassOverview";
        private const string LoginCaptchaUrl = "http://" + UrpIp + "/v5api/api/GetLoginCaptchaInfo/";
        public const string SelectCourseCaptchaUrl = "http://" + UrpIp + "/v5Api/api/xk/Captcha";
        private const string SelectCouseApi = "http://" + UrpIp + "/v5api/api/xk/add";
        public const string SelectUrl = "http://" + UrpIp + "/v5#/SelectCourse";

        /// <summary> 登陆JSON关键字.</summary>
        private const string AccessToken = "access_token";
        private const string TokenType = "token_type";

        /// <summary> 登陆token缓存.</summary>
        private string _token;

        /// <summary> 验证码JSON关键字.</summary>
        private const string TempGuid = "TempGuid";
        private const string ImgGuid = "ImgGuid";
        private const string CaptchaValue = "CaptchaValue";

        /// <summary> 个人信息JSON关键字.</summary>
        private const string UserData = "userData";
        private const string Id = "Id";
        private const string NameString = "Name";

        /// <summary> 特殊错误关键字.</summary>
        private const string LoginError = "用户名或密码不正确!";
        private const string CaptchaError = "验证码错误!";

        /// <summary> 验证码错误次数.</summary>
        private int _captchaError;

        /// <summary> 选课次数.</summary>
        private long _count;
        /// <summary> 省略号计数器.</summary>
        private int _cpct;

        /// <summary> JSON存储字典.</summary>
        private readonly ConcurrentDictionary<string, string> _dictionary = new ConcurrentDictionary<string, string>();
        /// <summary> 个人信息类.</summary>
        private ClassRegister _register = new ClassRegister();
        /// <summary> 选课类.</summary>
        private readonly SelectCode _selectCode = new SelectCode();
        /// <summary> 登陆线程.</summary>
        private Task _loginThread;
        /// <summary> 选课线程.</summary>
        private Task _selectThread;

        /// <summary> 已选课程堆栈.</summary>
        private readonly ArrayList _seleted = new ArrayList();

        private string _userAgent;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ListViewSelectCourse = new System.Windows.Forms.ListView();
            this._columnHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columnHeaderAdd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columnHeaderStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._webBrowserMain = new System.Windows.Forms.WebBrowser();
            this._splitContainerMain = new System.Windows.Forms.SplitContainer();
            this._splitContainerBrower = new System.Windows.Forms.SplitContainer();
            this._statusStripMain = new System.Windows.Forms.StatusStrip();
            this._toolStripStatusLabelInf = new System.Windows.Forms.ToolStripStatusLabel();
            this._toolStripProgressBarProc = new System.Windows.Forms.ToolStripProgressBar();
            this._splitContainerPanel = new System.Windows.Forms.SplitContainer();
            this._panelCourse = new System.Windows.Forms.Panel();
            this._panelTop = new System.Windows.Forms.Panel();
            this._splitContainerTop = new System.Windows.Forms.SplitContainer();
            this._groupBoxCourse = new System.Windows.Forms.GroupBox();
            this._groupBoxControl = new System.Windows.Forms.GroupBox();
            this._flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this._buttonSet = new System.Windows.Forms.Button();
            this._buttonOneKey = new System.Windows.Forms.Button();
            this._buttonEdit = new System.Windows.Forms.Button();
            this._buttonSelectCourse = new System.Windows.Forms.Button();
            this._buttonViewCourse = new System.Windows.Forms.Button();
            this._buttonValid = new System.Windows.Forms.Button();
            this._buttonAbout = new System.Windows.Forms.Button();
            this._splitContainerInf = new System.Windows.Forms.SplitContainer();
            this._textBoxInf = new System.Windows.Forms.TextBox();
            this._cmsClearTextInf = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._toolStripMenuItemClean = new System.Windows.Forms.ToolStripMenuItem();
            this._textBoxReturnMsg = new System.Windows.Forms.TextBox();
            this._cmsClearTextMsg = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this._timerSelectCourse = new System.Windows.Forms.Timer(this.components);
            this._timerCp = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainerMain)).BeginInit();
            this._splitContainerMain.Panel1.SuspendLayout();
            this._splitContainerMain.Panel2.SuspendLayout();
            this._splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainerBrower)).BeginInit();
            this._splitContainerBrower.Panel1.SuspendLayout();
            this._splitContainerBrower.Panel2.SuspendLayout();
            this._splitContainerBrower.SuspendLayout();
            this._statusStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainerPanel)).BeginInit();
            this._splitContainerPanel.Panel1.SuspendLayout();
            this._splitContainerPanel.Panel2.SuspendLayout();
            this._splitContainerPanel.SuspendLayout();
            this._panelCourse.SuspendLayout();
            this._panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainerTop)).BeginInit();
            this._splitContainerTop.Panel1.SuspendLayout();
            this._splitContainerTop.Panel2.SuspendLayout();
            this._splitContainerTop.SuspendLayout();
            this._groupBoxCourse.SuspendLayout();
            this._groupBoxControl.SuspendLayout();
            this._flowLayoutPanelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainerInf)).BeginInit();
            this._splitContainerInf.Panel1.SuspendLayout();
            this._splitContainerInf.Panel2.SuspendLayout();
            this._splitContainerInf.SuspendLayout();
            this._cmsClearTextInf.SuspendLayout();
            this._cmsClearTextMsg.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListViewSelectCourse
            // 
            this.ListViewSelectCourse.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._columnHeaderId,
            this._columnHeaderName,
            this._columnHeaderAdd,
            this._columnHeaderStatus});
            this.ListViewSelectCourse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewSelectCourse.FullRowSelect = true;
            this.ListViewSelectCourse.Location = new System.Drawing.Point(3, 17);
            this.ListViewSelectCourse.MultiSelect = false;
            this.ListViewSelectCourse.Name = "ListViewSelectCourse";
            this.ListViewSelectCourse.Size = new System.Drawing.Size(343, 234);
            this.ListViewSelectCourse.TabIndex = 0;
            this.ListViewSelectCourse.UseCompatibleStateImageBehavior = false;
            this.ListViewSelectCourse.View = System.Windows.Forms.View.Details;
            // 
            // _columnHeaderId
            // 
            this._columnHeaderId.Text = "课程代码";
            this._columnHeaderId.Width = 76;
            // 
            // _columnHeaderName
            // 
            this._columnHeaderName.Text = "课程名称";
            this._columnHeaderName.Width = 67;
            // 
            // _columnHeaderAdd
            // 
            this._columnHeaderAdd.Text = "备注";
            this._columnHeaderAdd.Width = 54;
            // 
            // _columnHeaderStatus
            // 
            this._columnHeaderStatus.Text = "状态";
            this._columnHeaderStatus.Width = 55;
            // 
            // _webBrowserMain
            // 
            this._webBrowserMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._webBrowserMain.IsWebBrowserContextMenuEnabled = false;
            this._webBrowserMain.Location = new System.Drawing.Point(0, 0);
            this._webBrowserMain.MinimumSize = new System.Drawing.Size(20, 20);
            this._webBrowserMain.Name = "_webBrowserMain";
            this._webBrowserMain.Size = new System.Drawing.Size(560, 578);
            this._webBrowserMain.TabIndex = 0;
            this._webBrowserMain.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this._webBrowserMain_DocumentCompleted);
            this._webBrowserMain.NewWindow += new System.ComponentModel.CancelEventHandler(this._webBrowserMain_NewWindow);
            this._webBrowserMain.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this._webBrowserMain_ProgressChanged);
            // 
            // _splitContainerMain
            // 
            this._splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this._splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this._splitContainerMain.Name = "_splitContainerMain";
            // 
            // _splitContainerMain.Panel1
            // 
            this._splitContainerMain.Panel1.Controls.Add(this._splitContainerBrower);
            // 
            // _splitContainerMain.Panel2
            // 
            this._splitContainerMain.Panel2.Controls.Add(this._splitContainerPanel);
            this._splitContainerMain.Size = new System.Drawing.Size(1012, 607);
            this._splitContainerMain.SplitterDistance = 560;
            this._splitContainerMain.TabIndex = 0;
            // 
            // _splitContainerBrower
            // 
            this._splitContainerBrower.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainerBrower.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this._splitContainerBrower.Location = new System.Drawing.Point(0, 0);
            this._splitContainerBrower.Name = "_splitContainerBrower";
            this._splitContainerBrower.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitContainerBrower.Panel1
            // 
            this._splitContainerBrower.Panel1.Controls.Add(this._webBrowserMain);
            // 
            // _splitContainerBrower.Panel2
            // 
            this._splitContainerBrower.Panel2.Controls.Add(this._statusStripMain);
            this._splitContainerBrower.Size = new System.Drawing.Size(560, 607);
            this._splitContainerBrower.SplitterDistance = 578;
            this._splitContainerBrower.TabIndex = 12;
            // 
            // _statusStripMain
            // 
            this._statusStripMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripStatusLabelInf,
            this._toolStripProgressBarProc});
            this._statusStripMain.Location = new System.Drawing.Point(0, 0);
            this._statusStripMain.Name = "_statusStripMain";
            this._statusStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this._statusStripMain.Size = new System.Drawing.Size(560, 25);
            this._statusStripMain.TabIndex = 11;
            this._statusStripMain.Text = "Test";
            // 
            // _toolStripStatusLabelInf
            // 
            this._toolStripStatusLabelInf.Name = "_toolStripStatusLabelInf";
            this._toolStripStatusLabelInf.Size = new System.Drawing.Size(443, 20);
            this._toolStripStatusLabelInf.Spring = true;
            this._toolStripStatusLabelInf.Text = "状态栏";
            this._toolStripStatusLabelInf.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _toolStripProgressBarProc
            // 
            this._toolStripProgressBarProc.Name = "_toolStripProgressBarProc";
            this._toolStripProgressBarProc.Size = new System.Drawing.Size(100, 19);
            // 
            // _splitContainerPanel
            // 
            this._splitContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainerPanel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._splitContainerPanel.Location = new System.Drawing.Point(0, 0);
            this._splitContainerPanel.Name = "_splitContainerPanel";
            this._splitContainerPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitContainerPanel.Panel1
            // 
            this._splitContainerPanel.Panel1.Controls.Add(this._panelCourse);
            // 
            // _splitContainerPanel.Panel2
            // 
            this._splitContainerPanel.Panel2.Controls.Add(this._splitContainerInf);
            this._splitContainerPanel.Size = new System.Drawing.Size(448, 607);
            this._splitContainerPanel.SplitterDistance = 256;
            this._splitContainerPanel.TabIndex = 0;
            // 
            // _panelCourse
            // 
            this._panelCourse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panelCourse.Controls.Add(this._panelTop);
            this._panelCourse.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelCourse.Location = new System.Drawing.Point(0, 0);
            this._panelCourse.Name = "_panelCourse";
            this._panelCourse.Size = new System.Drawing.Size(448, 256);
            this._panelCourse.TabIndex = 0;
            // 
            // _panelTop
            // 
            this._panelTop.Controls.Add(this._splitContainerTop);
            this._panelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelTop.Location = new System.Drawing.Point(0, 0);
            this._panelTop.Name = "_panelTop";
            this._panelTop.Size = new System.Drawing.Size(446, 254);
            this._panelTop.TabIndex = 4;
            // 
            // _splitContainerTop
            // 
            this._splitContainerTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainerTop.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this._splitContainerTop.Location = new System.Drawing.Point(0, 0);
            this._splitContainerTop.Name = "_splitContainerTop";
            // 
            // _splitContainerTop.Panel1
            // 
            this._splitContainerTop.Panel1.Controls.Add(this._groupBoxCourse);
            // 
            // _splitContainerTop.Panel2
            // 
            this._splitContainerTop.Panel2.Controls.Add(this._groupBoxControl);
            this._splitContainerTop.Size = new System.Drawing.Size(446, 254);
            this._splitContainerTop.SplitterDistance = 349;
            this._splitContainerTop.TabIndex = 3;
            // 
            // _groupBoxCourse
            // 
            this._groupBoxCourse.Controls.Add(this.ListViewSelectCourse);
            this._groupBoxCourse.Dock = System.Windows.Forms.DockStyle.Fill;
            this._groupBoxCourse.Location = new System.Drawing.Point(0, 0);
            this._groupBoxCourse.Name = "_groupBoxCourse";
            this._groupBoxCourse.Size = new System.Drawing.Size(349, 254);
            this._groupBoxCourse.TabIndex = 2;
            this._groupBoxCourse.TabStop = false;
            this._groupBoxCourse.Text = "选课表";
            // 
            // _groupBoxControl
            // 
            this._groupBoxControl.Controls.Add(this._flowLayoutPanelButtons);
            this._groupBoxControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._groupBoxControl.Location = new System.Drawing.Point(0, 0);
            this._groupBoxControl.Name = "_groupBoxControl";
            this._groupBoxControl.Size = new System.Drawing.Size(93, 254);
            this._groupBoxControl.TabIndex = 0;
            this._groupBoxControl.TabStop = false;
            this._groupBoxControl.Text = "操作栏";
            // 
            // _flowLayoutPanelButtons
            // 
            this._flowLayoutPanelButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._flowLayoutPanelButtons.Controls.Add(this._buttonSet);
            this._flowLayoutPanelButtons.Controls.Add(this._buttonOneKey);
            this._flowLayoutPanelButtons.Controls.Add(this._buttonEdit);
            this._flowLayoutPanelButtons.Controls.Add(this._buttonSelectCourse);
            this._flowLayoutPanelButtons.Controls.Add(this._buttonViewCourse);
            this._flowLayoutPanelButtons.Controls.Add(this._buttonValid);
            this._flowLayoutPanelButtons.Controls.Add(this._buttonAbout);
            this._flowLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flowLayoutPanelButtons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._flowLayoutPanelButtons.Location = new System.Drawing.Point(3, 17);
            this._flowLayoutPanelButtons.Name = "_flowLayoutPanelButtons";
            this._flowLayoutPanelButtons.Size = new System.Drawing.Size(87, 234);
            this._flowLayoutPanelButtons.TabIndex = 0;
            // 
            // _buttonSet
            // 
            this._buttonSet.Location = new System.Drawing.Point(3, 3);
            this._buttonSet.Name = "_buttonSet";
            this._buttonSet.Size = new System.Drawing.Size(75, 27);
            this._buttonSet.TabIndex = 18;
            this._buttonSet.Text = "用户设置";
            this._buttonSet.UseVisualStyleBackColor = true;
            this._buttonSet.Click += new System.EventHandler(this._buttonSet_Click);
            // 
            // _buttonOneKey
            // 
            this._buttonOneKey.ForeColor = System.Drawing.Color.Red;
            this._buttonOneKey.Location = new System.Drawing.Point(3, 36);
            this._buttonOneKey.Name = "_buttonOneKey";
            this._buttonOneKey.Size = new System.Drawing.Size(75, 27);
            this._buttonOneKey.TabIndex = 22;
            this._buttonOneKey.Text = "网页登陆";
            this._buttonOneKey.UseVisualStyleBackColor = true;
            this._buttonOneKey.Click += new System.EventHandler(this._buttonOneKey_Click);
            // 
            // _buttonEdit
            // 
            this._buttonEdit.Location = new System.Drawing.Point(3, 69);
            this._buttonEdit.Name = "_buttonEdit";
            this._buttonEdit.Size = new System.Drawing.Size(75, 27);
            this._buttonEdit.TabIndex = 13;
            this._buttonEdit.Text = "编辑选课";
            this._buttonEdit.UseVisualStyleBackColor = true;
            this._buttonEdit.Click += new System.EventHandler(this._buttonEdit_Click);
            // 
            // _buttonSelectCourse
            // 
            this._buttonSelectCourse.Enabled = false;
            this._buttonSelectCourse.Location = new System.Drawing.Point(3, 102);
            this._buttonSelectCourse.Name = "_buttonSelectCourse";
            this._buttonSelectCourse.Size = new System.Drawing.Size(75, 27);
            this._buttonSelectCourse.TabIndex = 16;
            this._buttonSelectCourse.Text = "开始选课";
            this._buttonSelectCourse.UseVisualStyleBackColor = true;
            this._buttonSelectCourse.Click += new System.EventHandler(this._buttonSelectCourse_Click);
            // 
            // _buttonViewCourse
            // 
            this._buttonViewCourse.Location = new System.Drawing.Point(3, 135);
            this._buttonViewCourse.Name = "_buttonViewCourse";
            this._buttonViewCourse.Size = new System.Drawing.Size(75, 27);
            this._buttonViewCourse.TabIndex = 20;
            this._buttonViewCourse.Text = "课表总览";
            this._buttonViewCourse.UseVisualStyleBackColor = true;
            this._buttonViewCourse.Click += new System.EventHandler(this._buttonViewCourse_Click);
            // 
            // _buttonValid
            // 
            this._buttonValid.ForeColor = System.Drawing.Color.Black;
            this._buttonValid.Location = new System.Drawing.Point(3, 168);
            this._buttonValid.Name = "_buttonValid";
            this._buttonValid.Size = new System.Drawing.Size(75, 27);
            this._buttonValid.TabIndex = 23;
            this._buttonValid.Text = "软件注册";
            this._buttonValid.UseVisualStyleBackColor = true;
            this._buttonValid.Click += new System.EventHandler(this._buttonValid_Click);
            // 
            // _buttonAbout
            // 
            this._buttonAbout.Location = new System.Drawing.Point(3, 201);
            this._buttonAbout.Name = "_buttonAbout";
            this._buttonAbout.Size = new System.Drawing.Size(75, 27);
            this._buttonAbout.TabIndex = 21;
            this._buttonAbout.Text = "关于软件";
            this._buttonAbout.UseVisualStyleBackColor = true;
            this._buttonAbout.Click += new System.EventHandler(this._buttonAbout_Click);
            // 
            // _splitContainerInf
            // 
            this._splitContainerInf.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainerInf.Location = new System.Drawing.Point(0, 0);
            this._splitContainerInf.Name = "_splitContainerInf";
            this._splitContainerInf.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitContainerInf.Panel1
            // 
            this._splitContainerInf.Panel1.Controls.Add(this._textBoxInf);
            // 
            // _splitContainerInf.Panel2
            // 
            this._splitContainerInf.Panel2.Controls.Add(this._textBoxReturnMsg);
            this._splitContainerInf.Size = new System.Drawing.Size(448, 347);
            this._splitContainerInf.SplitterDistance = 202;
            this._splitContainerInf.TabIndex = 0;
            // 
            // _textBoxInf
            // 
            this._textBoxInf.ContextMenuStrip = this._cmsClearTextInf;
            this._textBoxInf.Dock = System.Windows.Forms.DockStyle.Fill;
            this._textBoxInf.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._textBoxInf.ForeColor = System.Drawing.Color.Red;
            this._textBoxInf.Location = new System.Drawing.Point(0, 0);
            this._textBoxInf.MaxLength = 65535;
            this._textBoxInf.Multiline = true;
            this._textBoxInf.Name = "_textBoxInf";
            this._textBoxInf.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._textBoxInf.Size = new System.Drawing.Size(448, 202);
            this._textBoxInf.TabIndex = 0;
            this._textBoxInf.WordWrap = false;
            // 
            // _cmsClearTextInf
            // 
            this._cmsClearTextInf.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripMenuItemClean});
            this._cmsClearTextInf.Name = "_cmsClearTextInf";
            this._cmsClearTextInf.Size = new System.Drawing.Size(109, 28);
            this._cmsClearTextInf.Click += new System.EventHandler(this._cmsClearTextInf_Click);
            // 
            // _toolStripMenuItemClean
            // 
            this._toolStripMenuItemClean.Name = "_toolStripMenuItemClean";
            this._toolStripMenuItemClean.Size = new System.Drawing.Size(108, 24);
            this._toolStripMenuItemClean.Text = "清除";
            // 
            // _textBoxReturnMsg
            // 
            this._textBoxReturnMsg.ContextMenuStrip = this._cmsClearTextMsg;
            this._textBoxReturnMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this._textBoxReturnMsg.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._textBoxReturnMsg.Location = new System.Drawing.Point(0, 0);
            this._textBoxReturnMsg.MaxLength = 65535;
            this._textBoxReturnMsg.Multiline = true;
            this._textBoxReturnMsg.Name = "_textBoxReturnMsg";
            this._textBoxReturnMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._textBoxReturnMsg.Size = new System.Drawing.Size(448, 141);
            this._textBoxReturnMsg.TabIndex = 0;
            this._textBoxReturnMsg.WordWrap = false;
            // 
            // _cmsClearTextMsg
            // 
            this._cmsClearTextMsg.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripMenuItem1});
            this._cmsClearTextMsg.Name = "_cmsClearTextMsg";
            this._cmsClearTextMsg.Size = new System.Drawing.Size(109, 28);
            this._cmsClearTextMsg.Click += new System.EventHandler(this._cmsClearTextMsg_Click);
            // 
            // _toolStripMenuItem1
            // 
            this._toolStripMenuItem1.Name = "_toolStripMenuItem1";
            this._toolStripMenuItem1.Size = new System.Drawing.Size(108, 24);
            this._toolStripMenuItem1.Text = "清除";
            // 
            // _timerSelectCourse
            // 
            this._timerSelectCourse.Interval = 1000;
            this._timerSelectCourse.Tick += new System.EventHandler(this._timerSelectCourse_Tick);
            // 
            // _timerCp
            // 
            this._timerCp.Interval = 1000;
            this._timerCp.Tick += new System.EventHandler(this._timerCp_Tick);
            // 
            // FormMain
            // 
            this.ClientSize = new System.Drawing.Size(1012, 607);
            this.Controls.Add(this._splitContainerMain);
            this.Icon = global::云大选课辅助工具.Properties.Resources.school;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "云大选课辅助工具";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this._splitContainerMain.Panel1.ResumeLayout(false);
            this._splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainerMain)).EndInit();
            this._splitContainerMain.ResumeLayout(false);
            this._splitContainerBrower.Panel1.ResumeLayout(false);
            this._splitContainerBrower.Panel2.ResumeLayout(false);
            this._splitContainerBrower.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainerBrower)).EndInit();
            this._splitContainerBrower.ResumeLayout(false);
            this._statusStripMain.ResumeLayout(false);
            this._statusStripMain.PerformLayout();
            this._splitContainerPanel.Panel1.ResumeLayout(false);
            this._splitContainerPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainerPanel)).EndInit();
            this._splitContainerPanel.ResumeLayout(false);
            this._panelCourse.ResumeLayout(false);
            this._panelTop.ResumeLayout(false);
            this._splitContainerTop.Panel1.ResumeLayout(false);
            this._splitContainerTop.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainerTop)).EndInit();
            this._splitContainerTop.ResumeLayout(false);
            this._groupBoxCourse.ResumeLayout(false);
            this._groupBoxControl.ResumeLayout(false);
            this._flowLayoutPanelButtons.ResumeLayout(false);
            this._splitContainerInf.Panel1.ResumeLayout(false);
            this._splitContainerInf.Panel1.PerformLayout();
            this._splitContainerInf.Panel2.ResumeLayout(false);
            this._splitContainerInf.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainerInf)).EndInit();
            this._splitContainerInf.ResumeLayout(false);
            this._cmsClearTextInf.ResumeLayout(false);
            this._cmsClearTextMsg.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public FormMain()
        {
            //线程可调用文本框
            CheckForIllegalCrossThreadCalls = false;

            //设置IE为IE10
            Turn2Ie10();

            // 初始化基本组件. 
            InitializeComponent();

            //禁止弹出JS警告
            _webBrowserMain.ScriptErrorsSuppressed = true;
            _webBrowserMain.Navigate(MainUrl);

            //获取验证码
            AddDictionary(ClassHttp.HttpGet("http://" + UrpIp + "/v5api/api/GetLoginCaptchaInfo/null", UrpIp,
                MainUrl, null, null, _userAgent));

            if (_register.Info.CaptchaType.Equals("manual"))
            {
                _buttonSelectCourse.Enabled = true;
            }

            CoreInfoBox("使用软件前请仔细阅读安装目录下的文本文件!");
        }

        private void _buttonAbout_Click(object sender, EventArgs e)
        {
            new FormAbout().ShowDialog();
        }

        private void _buttonEdit_Click(object sender, EventArgs e)
        {
            var items = new List<ClassCourse>();
            foreach (ListViewItem item in ListViewSelectCourse.Items)
            {
                items.Add(new ClassCourse(item.SubItems[0].Text, item.SubItems[1].Text, item.SubItems[2].Text));
            }
            var edit = new FormClassEdit(items);
            if (edit.ShowDialog(this) == DialogResult.OK)
            {
                ListViewSelectCourse.Items.Clear();
                foreach (var course in edit.GetClassCourses())
                {
                    var item2 = new ListViewItem(course.Id)
                    {
                        SubItems =
                        {
                            course.Name,
                            course.Remark,
                            "未选"
                        }
                    };
                    ListViewSelectCourse.Items.Add(item2);
                }
            }
        }

        private void _buttonOneKey_Click(object sender, EventArgs e)
        {
            try
            {
                var ngStorageCurrentUser = ReturnSessionStorage("ngStorage-currentUser");
                var ngStorageAccessToken = ReturnSessionStorage("ngStorage-accessToken");
                if (string.IsNullOrEmpty(ngStorageAccessToken))
                {
                    IsLogin = false;
                    CoreInfoBox("请在软件内的网页上完成登陆!");
                }
                else
                {
                    foreach (var pair in JObject.Parse(ngStorageCurrentUser))
                    {
                        if (_dictionary.TryAdd(pair.Key, (string) pair.Value) == false)
                        {
                            _dictionary[pair.Key] = (string) pair.Value;
                        }
                    }
                    CoreInfoBox(_dictionary["Name"] + "(" + _dictionary["Id"] + ")欢迎登陆!");
                    _dictionary["token_type"] = "bearer";
                    _token = "Authorization: " + _dictionary["token_type"] + " " + ngStorageAccessToken;
                    _register.Info.Sno = _dictionary["Id"];
                    IsLogin = true;
                    if (_register.Info.IsRegistered && _register.CheckRegisterAgain() &&
                        _register.Info.Sno.Equals(_register.CurrentUserId) && _register.Info.TimeLeft > 0)
                    {
                        _buttonSelectCourse.Enabled = true;
                    }
                    else
                    {
                        _register.Info.CaptchaType = "manual";
                        _buttonSelectCourse.Enabled = true;
                        CoreInfoBox("该学号未注册,仅可使用手动打码模式!");
                    }

                    if (_loginThread == null)
                    {
                        _loginThread = Task.Factory.StartNew(LoginAction);
                    }
                    else if (_loginThread.Status != TaskStatus.Running)
                    {
                        _loginThread.Wait();
                        _loginThread = Task.Factory.StartNew(LoginAction);
                    }
                }
            }
            catch (Exception ex)
            {
                IsLogin = false;
                CommonInfoBox(ex.Message);
            }
        }

        private void _buttonSelectCourse_Click(object sender, EventArgs e)
        {
            try
            {
                if (_register.Info.Sno != null && _register.Info.Sno.Equals(_register.CurrentUserId))
                {
                    if (_selectThread != null)
                    {
                        StopSelect();
                    }
                    else if (_buttonSelectCourse.Enabled)
                    {

                        StartSelect();
                    }
                }
                else if (_register.Info.CaptchaType == null || _register.Info.CaptchaType.Equals("manual"))
                {
                    CoreInfoBox("尚未未注册，仅可使用手动打码模式!");
                    _register.Info.CaptchaType = "manual";
                    if (_selectThread != null)
                    {
                        StopSelect();
                    }
                    else if (_buttonSelectCourse.Enabled)
                    {

                        StartSelect();
                    }
                }
                else
                {
                    CoreInfoBox("该学号未注册，仅可使用手动打码模式!");
                }
            }catch(Exception ex)
            {
                CommonInfoBox(ex.Message);
            }
            
        }

        private void _buttonSet_Click(object sender, EventArgs e)
        {
            try
            {
                var formSet = new FormSet(_register);
                if (formSet.ShowDialog() == DialogResult.OK)
                {
                    _register = formSet.userInfo;
                }
                if (_register.Info.CaptchaType.Equals("manual"))
                {
                    _buttonSelectCourse.Enabled = true;
                }
                else if (_register.Info.IsRegistered && _register.CheckRegisterAgain() &&
                         _register.Info.Sno.Equals(_register.CurrentUserId) && _register.Info.TimeLeft > 0)
                {
                    _buttonSelectCourse.Enabled = true;
                }
                else
                {
                    _buttonSelectCourse.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                CommonInfoBox(ex.Message);
            }
        }

        private void _buttonValid_Click(object sender, EventArgs e)
        {
            try
            {
                var register = new FormRegister(_register, IsLogin);
                if (register.IsDisposed == false)
                {
                    register.ShowDialog();
                    _register = register.Register;
                }

                if (_register.Info.IsRegistered && _register.Info.Sno.Equals(_register.CurrentUserId))
                {
                    _buttonSelectCourse.Enabled = true;
                }
                else if (_register.Info.IsRegistered == false)
                {
                    _buttonSelectCourse.Enabled = false;
                }

                if (_register.Info.CaptchaType.Equals("manual"))
                {
                    _buttonSelectCourse.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CommonInfoBox(ex.Message);
            }
        }

        private void _buttonViewCourse_Click(object sender, EventArgs e)
        {
            _webBrowserMain.Navigate(CourseUrl);
        }

        private void _cmsClearTextInf_Click(object sender, EventArgs e)
        {
            _textBoxInf.Text = "";
        }

        private void _cmsClearTextMsg_Click(object sender, EventArgs e)
        {
            _textBoxReturnMsg.Text = "";
        }

        /// <summary> 选课开始选课次数计数钟摆.</summary>
        /// <remarks> windawings, 11/28/2015.</remarks>
        /// <param name="sender"> Source of the event.</param>
        /// <param name="e">      Event information.</param>
        private void _timerCp_Tick(object sender, EventArgs e)
        {
            var str = string.Concat("云大选课辅助工具  向服务器发送选课请求", _count, "次(", _selectCode.id, ")");
            for (var i = 0; i < _cpct; i++)
            {
                str = str + ".";
            }
            _cpct++;
            if (_cpct > 5)
            {
                _cpct = 0;
            }
            Text = str;
        }

        /// <summary> 选课钟摆.</summary>
        /// <remarks> windawings, 11/28/2015.</remarks>
        /// <param name="sender"> Source of the event.</param>
        /// <param name="e">      Event information.</param>
        private void _timerSelectCourse_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_selectThread == null || _selectThread.Status != TaskStatus.Running)
                {
                    _selectThread = Task.Factory.StartNew(SelectCourseAction);
                }
            }
            catch (Exception ex)
            {
                CommonInfoBox(ex.Message);
            }
        }

        private void _webBrowserMain_NewWindow(object sender, CancelEventArgs e)
        {
            Process.Start(((WebBrowser)sender).StatusText);
            e.Cancel = true;
        }

        private void _webBrowserMain_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            try
            {
                WebBrowser browser = (WebBrowser)sender;
                if (browser == _webBrowserMain)
                {
                    int num = (int)((e.CurrentProgress * 100L) / Math.Max(e.MaximumProgress, 1L));
                    if ((num >= 100) || (num <= 0))
                    {
                        _toolStripProgressBarProc.Visible = false;
                    }
                    else
                    {
                        _toolStripProgressBarProc.Value = num;
                        _toolStripProgressBarProc.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonInfoBox(ex.Message);
            }
        }

        /// <summary> 把IE版本设置为10.</summary>
        /// <remarks> windawings, 11/28/2015.</remarks>
        /// <ex cref="Exception"> 抛出异常.</ex>
        private void Turn2Ie10()
        {
            try
            {
                var name = Process.GetCurrentProcess().ProcessName + ".exe";
                using (
                    var key =
                        Registry.CurrentUser.CreateSubKey(
                            @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION"))
                {
                    using (
                        var key2 =
                            Registry.CurrentUser.CreateSubKey(
                                @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_DOCUMENT_COMPATIBLE_MODE")
                        )
                    {
                        key.SetValue(name, 0x2710, RegistryValueKind.DWord);
                        key2.SetValue(name, 0xf4240, RegistryValueKind.DWord);
                        key.Close();
                        key2.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonInfoBox(ex.Message);
            }
        }

        /// <summary> 返回网页SessionStorage内容.</summary>
        /// <remarks> windawings, 11/28/2015.</remarks>
        /// <ex cref="Exception"> 抛出异常.</ex>
        /// <param name="keyword"> 获取项目名.</param>
        private string ReturnSessionStorage(string keyword)
        {
            try
            {
                if (_webBrowserMain.IsBusy == false)
                {
                    var domWindow = (IHTMLWindow2) _webBrowserMain.Document.Window.DomWindow;
                    var code = "function getSessionStorage() " +
                               "{" +
                               "    return sessionStorage.getItem(\"" + keyword + "\");" +
                               "}";
                    domWindow.execScript(code);
                    var ngStorageData = _webBrowserMain.Document.InvokeScript("getSessionStorage").ToString();
                    if (keyword.Equals("ngStorage-accessToken"))
                    {
                        ngStorageData = ngStorageData.Replace("\"", "");
                    }
                    return ngStorageData;
                }
                CoreInfoBox("请等待浏览界面登陆完成!");
            }
            catch (Exception ex)
            {
                CommonInfoBox(ex.Message);
            }
            return null;
        }

        /// <summary> 加入JSON字典.</summary>
        /// <remarks> windawings, 11/28/2015.</remarks>
        /// <ex cref="Exception"> Thrown when an ex error condition occurs.</ex>
        /// <param name="data"> The data.</param>
        private void AddDictionary(string data)
        {
            try
            {
                foreach (var pair in JObject.Parse(data))
                {
                    if (_dictionary.TryAdd(pair.Key, (string) pair.Value) == false)
                    {
                        _dictionary[pair.Key] = (string) pair.Value;
                    }
                }
                if (_dictionary.ContainsKey(ErrorDescription))
                {
                    var errorStr = "";
                    _dictionary.TryRemove(ErrorDescription, out errorStr);
                    if (errorStr.Equals(CaptchaError))
                    {
                        if (_captchaError == 5)
                        {
                            _captchaError = 0;
                            throw new Exception("验证码错误次数过多，请改用手动打码!");
                        }
                        _captchaError++;
                        CommonInfoBox(errorStr);
                        LoginAction();
                    }
                    else if (errorStr.Equals(LoginError))
                    {
                        CoreInfoBox("验证码通过!可使用自动打码!");
                    }
                    else
                    {
                        CoreInfoBox(errorStr);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonInfoBox(ex.Message);
            }
        }

        /// <summary> 向目标文本框添加内容.</summary>
        /// <remarks> windawings, 11/28/2015.</remarks>
        /// <param name="tb">  The terabytes.</param>
        /// <param name="txt"> The text.</param>
        private static void AddText(TextBoxBase tb, string txt)
        {
            try
            {
                var now = DateTime.Now;
                var str = now.Hour + ":" + now.Minute + ":" + now.Second;
                if ((tb.Text.Length + txt.Length) > tb.MaxLength)
                {
                    tb.Text = "";
                }
                tb.AppendText(string.Concat(str, ' ', txt, Environment.NewLine));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary> 下文本框内容显示.</summary>
        /// <remarks> windawings, 11/28/2015.</remarks>
        /// <param name="info"> The information.</param>
        private void CommonInfoBox(string info)
        {
            AddText(_textBoxReturnMsg, info);
        }

        /// <summary> 上文本框内容显示.</summary>
        /// <remarks> windawings, 11/28/2015.</remarks>
        /// <param name="info"> The information.</param>
        private void CoreInfoBox(string info)
        {
            AddText(_textBoxInf, info);
        }

        /// <summary> 登陆操作.</summary>
        /// <remarks> windawings, 11/28/2015.</remarks>
        /// <ex cref="ApplicationException"> Thrown when an Application error condition occurs.</ex>
        /// <ex cref="Exception">            Thrown when an ex error condition occurs.</ex>
        private void LoginAction()
        {
            try
            {
                AddDictionary(ClassHttp.HttpGet(LoginCaptchaUrl + _dictionary[TempGuid], "" + UrpIp + "", MainUrl, null,
                    null, _userAgent));
                var valueByCaptcha = "";
                
                valueByCaptcha = ClassCaptcha.GetValueByDict(_dictionary[ImgGuid]);

                if (valueByCaptcha == null)
                {
                    if (_captchaError == 5)
                    {
                        _captchaError = 0;
                        throw new ApplicationException("验证码错误次数过多，请改用手动打码!");
                    }
                    _captchaError++;
                    throw new Exception(CaptchaError);
                }
                if (_dictionary.TryAdd(CaptchaValue, valueByCaptcha) == false)
                {
                    _dictionary[CaptchaValue] = valueByCaptcha;
                }
                var postData =
                    string.Format(
                        "grant_type=password&username=20150000000&password=20150000000|{0}*{1}&client_id=ynumisSite",
                        _dictionary[CaptchaValue], _dictionary[TempGuid]);

                AddDictionary(ClassHttp.HttpPost(LoginUrl, MainUrl, postData, null, "application/x-www-form-urlencoded", null,_userAgent));
                if (string.IsNullOrEmpty(_register.CurrentUserId) == false)
                {
                    if (_register.Info.Sno.Equals(_register.CurrentUserId))
                    {
                        _buttonSelectCourse.Enabled = true;
                    }
                    else
                    {
                        _buttonSelectCourse.Enabled = false;
                    }
                }
                if (_register.Info.CaptchaType.Equals("manual"))
                {
                    _buttonSelectCourse.Enabled = true;
                }

            }
            catch (ApplicationException ex)
            {
                CoreInfoBox(ex.Message);
            }
            catch (Exception exception2)
            {
                CommonInfoBox(exception2.Message);
                LoginAction();
            }
        }

        /// <summary> 刷新选课状态.</summary>
        /// <remarks> windawings, 11/28/2015.</remarks>
        /// <returns> An int.</returns>
        private int RefreshStatus()
        {
            var num = 0;
            foreach (ListViewItem item in ListViewSelectCourse.Items)
            {
                if (_seleted.Contains(item.Text))
                {
                    item.SubItems[3].Text = "已选";
                    ListViewSelectCourse.Items.Remove(item);
                    _seleted.Remove(item.Text);
                }
                else
                {
                    item.SubItems[3].Text = "未选";
                    num++;
                }
            }
            return num;
        }

        /// <summary> 选课HTTP.</summary>
        /// <remarks> windawings, 11/28/2015.</remarks>
        private void SelectCourse()
        {
            try
            {
                var responseData =
                    ClassHttp.HttpGet(SelectCourseCaptchaUrl, UrpIp, SelectUrl, "application/json, text/plain, */*",
                        _token, _userAgent).Replace("\"", "").Trim();
                if (_dictionary.TryAdd(ImgGuid, responseData) == false)
                {
                    _dictionary[ImgGuid] = responseData;
                }
                if (_register.Info.CaptchaType == null || _register.Info.CaptchaType.Equals("auto"))
                {
                    _selectCode.captcha = ClassCaptcha.GetValueByDict(_dictionary[ImgGuid]);
                }
                else
                {
                    var formManual = new FormManual(ImgUrl + _dictionary[ImgGuid] + "." + _register.Info.PictureType,
                        _token, _userAgent, _register.Info.PictureType);
                    if (formManual.ShowDialog() == DialogResult.OK)
                    {
                        _selectCode.captcha = formManual.CaptchaValue;
                    }
                    else
                    {
                        CoreInfoBox("请先点停止选课，再关闭输入验证码的窗体即可!");
                        return;
                    }
                }
                if (_selectCode.captcha == null)
                {
                    throw new Exception(CaptchaError);
                }
                var postData = JsonConvert.SerializeObject(_selectCode);
                responseData = ClassHttp.HttpPost(SelectCouseApi, SelectUrl, postData,
                    "application/json, text/plain, */*",
                    "application/json;charset=utf-8", _token, _userAgent);

                var courseInfo = string.Format("[{0}({1})]", _selectCode.Name, _selectCode.id);
                responseData = responseData.Replace("\"", "").Trim();
                switch (responseData)
                {
                    case "OK":
                    {
                        CoreInfoBox("已选上" + courseInfo + "!");
                        _seleted.Add(_selectCode.id);
                        if (RefreshStatus() == 0)
                        {
                            CoreInfoBox("恭喜!课程已全部选完!");
                        }
                        return;
                    }
                    case "选课单中已有存在相同编号的教学班了!":
                    {
                        if (RefreshStatus() == 0)
                        {
                            CoreInfoBox("恭喜!课程已全部选完!");
                        }
                        CommonInfoBox(responseData);
                        return;
                    }
                    default:break;
                }
                CommonInfoBox(responseData);
            }
            catch (Exception ex)
            {
                CommonInfoBox(ex.Message);
            }
        }

        /// <summary> 选课操作.</summary>
        /// <remarks> windawings, 11/28/2015.</remarks>
        private void SelectCourseAction()
        {
            try
            {
                // 验证使用次数. 
                _count += 1L;
                if (_register.Info.CaptchaType.Equals("auto"))
                {
                    if (--_register.Info.TimeLeft > 0)
                    {
                        _register.SetRegInfo();
                    }
                    else
                    {
                        _timerCp.Stop();
                        _timerSelectCourse.Stop();

                        _register.Info.TimeLeft = 0;
                        _register.SetRegInfo();
                        _buttonSet.Enabled = true;
                        _buttonOneKey.Enabled = true;
                        _buttonValid.Enabled = true;
                        _buttonSelectCourse.Text = "开始选课";
                        Text = Title;
                        _selectThread = null;
                        throw new Exception("试用已到期!");
                    }
                }

                if (ListViewSelectCourse.Items.Count > 0)
                {
                    var index = 0;
                    if (ListViewSelectCourse.SelectedItems.Count > 0)
                    {
                        index = ListViewSelectCourse.SelectedItems[0].Index;
                    }
                    var item = ListViewSelectCourse.Items[index];
                    _selectCode.Name = item.SubItems[1].Text;
                    _selectCode.id = item.SubItems[0].Text;
                    SelectCourse();

                    if (ListViewSelectCourse.Items.Count > 0)
                    {
                        ListViewSelectCourse.Items[index].Selected = false;
                        index++;
                        if (index == ListViewSelectCourse.Items.Count)
                        {
                            index = 0;
                        }
                        ListViewSelectCourse.Items[index].Selected = true;
                    }
                    else
                    {
                        throw new Exception("选课表为空,请先添加课程!");
                    }
                }
                else
                {
                    throw new Exception("选课表为空,请先添加课程!");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("index") == false)
                {
                    CommonInfoBox(ex.Message);
                }
            }
        }

        /// <summary> 启动选课相关线程和钟摆.</summary>
        /// <remarks> windawings, 11/28/2015.</remarks>
        private void StartSelect()
        {
            if (IsLogin)
            {
                if (ListViewSelectCourse.Items.Count > 0)
                {
                    _count = 0L;
                    _buttonSet.Enabled = false;
                    _buttonOneKey.Enabled = false;
                    _buttonValid.Enabled = false;

                    //选课相关计时
                    _timerSelectCourse.Start();
                    _timerCp.Start();

                    CoreInfoBox("开始选课,每1秒选一次选课");

                    _buttonSelectCourse.Text = "停止选课";
                }
                else
                {
                    CoreInfoBox("选课表为空,请先添加课程!");
                    StopSelect();
                }
            }
            else
            {
                CoreInfoBox("请先登陆用户!");
                StopSelect();
            }
        }

        /// <summary> 停止选课.</summary>
        /// <remarks> windawings, 11/28/2015.</remarks>
        private void StopSelect()
        {
            try
            {
                _timerSelectCourse.Stop();
                _timerCp.Stop();
                if (_selectThread != null)
                {
                    if (_selectThread.Status == TaskStatus.RanToCompletion ||
                        _selectThread.Status == TaskStatus.Faulted ||
                        _selectThread.Status == TaskStatus.Canceled)
                    {
                        _selectThread.Dispose();
                    }
                    else
                    {
                        _selectThread.Wait();
                    }
                    
                    _selectThread = null;
                    _register.SetRegInfo();
                    _buttonSet.Enabled = true;
                    _buttonOneKey.Enabled = true;
                    _buttonValid.Enabled = true;

                    Text = Title;
                    _buttonSelectCourse.Text = "开始选课";
                }
            }
            catch (Exception ex)
            {
                CommonInfoBox(ex.Message);
            }
        }

        private void _webBrowserMain_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                if (IsLogin == false)
                {
                    var domWindow = _webBrowserMain.Document.Window.DomWindow;
                    var target = _webBrowserMain.Document.Window.DomWindow.GetType()
                        .InvokeMember("navigator", BindingFlags.GetProperty, null, domWindow, new object[0]);
                    var type = target.GetType();
                    _userAgent =
                        type.InvokeMember("userAgent", BindingFlags.GetProperty, null, target, new object[0]).ToString();
                }
            }
            catch (Exception ex)
            {
                CommonInfoBox(ex.Message);
            }
        }
    }
}