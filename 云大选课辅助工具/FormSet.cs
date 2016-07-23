using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace 云大选课辅助工具
{
    public class FormSet : Form
    {
        private Button buttonCancel;
        private Button buttonOK;
        private IContainer components;
        private GroupBox groupBoxCount;
        private GroupBox groupBoxCourseSet;
        public ClassRegister userInfo;
        private Label label3;
        private TextBox _textBoxUrl;
        private RadioButton _radioButtonAuto;
        private RadioButton _radioButtonByHand;
        private TextBox _textBoxPictureType;
        private Label label1;
        private ToolTip toolTip_picture;

        public FormSet()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            FormClosing += FormSet_FormClosing;
        }

        public FormSet(ClassRegister info)
        {
            InitializeComponent();
            userInfo = info;
            ValueToControl();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            FormClosing += FormSet_FormClosing;
        }

        /// <summary> 存储用户信息.</summary>
        /// <remarks> windawings, 12/21/2015.</remarks>
        private void ControlToValue()
        {
            userInfo.Info.ImgUrl = _textBoxUrl.Text.Trim();
            userInfo.Info.PictureType = _textBoxPictureType.Text.Trim();
            userInfo.Info.CaptchaType = ReturnCaptchaType();

            if (String.IsNullOrEmpty(userInfo.Info.ImgUrl) == false)
            {
                var url = userInfo.Info.ImgUrl[userInfo.Info.ImgUrl.Length - 1];
                if (url != '/' && url != '\\')
                {
                    userInfo.Info.ImgUrl += "/";
                }
            }

            userInfo.SetRegInfo();
        }

        /// <summary> 窗口关闭.</summary>
        /// <remarks> windawings, 12/21/2015.</remarks>
        /// <param name="sender"> Source of the event.</param>
        /// <param name="e">      Form closing event information.</param>
        private void FormSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            ControlToValue();
        }

        /// <summary> 显示用户信息.</summary>
        /// <remarks> windawings, 12/21/2015.</remarks>
        private void ValueToControl()
        {
            if (String.IsNullOrEmpty(userInfo.Info.ImgUrl))
            {
                _textBoxUrl.Text = "http://" + FormMain.UrpIp + "/vimgs/";
            }
            else
            {
                _textBoxUrl.Text = userInfo.Info.ImgUrl;
            }

            if (String.IsNullOrEmpty(userInfo.Info.PictureType))
            {
                _textBoxPictureType.Text = "png";
            }
            else
            {
                _textBoxPictureType.Text = userInfo.Info.PictureType;
            }
            
            SetCapctahType();
        }

        /// <summary> 返回打码平台.</summary>
        /// <remarks> windawings, 12/21/2015.</remarks>
        /// <returns> The captcha type.</returns>
        private string ReturnCaptchaType()
        {
            if (_radioButtonAuto.Checked)
            {
                return "auto";
            }
            if (_radioButtonByHand.Checked)
            {
                return "manual";
            }

            return "manual";
        }

        /// <summary> 设置打码平台.</summary>
        /// <remarks> windawings, 12/21/2015.</remarks>
        private void SetCapctahType()
        {
            switch (userInfo.Info.CaptchaType)
            {
                case "auto":
                {
                    _radioButtonAuto.Checked = true;
                    break;
                }
                case "manual":
                {
                    _radioButtonByHand.Checked = true;
                    break;
                }
                default:
                {
                    _radioButtonByHand.Checked = true;
                    break;
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxCount = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this._textBoxUrl = new System.Windows.Forms.TextBox();
            this.groupBoxCourseSet = new System.Windows.Forms.GroupBox();
            this._radioButtonByHand = new System.Windows.Forms.RadioButton();
            this._radioButtonAuto = new System.Windows.Forms.RadioButton();
            this.toolTip_picture = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this._textBoxPictureType = new System.Windows.Forms.TextBox();
            this.groupBoxCount.SuspendLayout();
            this.groupBoxCourseSet.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(12, 239);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(125, 25);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            buttonOK.DialogResult = DialogResult.OK;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(173, 239);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(125, 25);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.DialogResult = DialogResult.Cancel;
            // 
            // groupBoxCount
            // 
            this.groupBoxCount.Controls.Add(this._textBoxPictureType);
            this.groupBoxCount.Controls.Add(this.label1);
            this.groupBoxCount.Controls.Add(this.label3);
            this.groupBoxCount.Controls.Add(this._textBoxUrl);
            this.groupBoxCount.Location = new System.Drawing.Point(12, 12);
            this.groupBoxCount.Name = "groupBoxCount";
            this.groupBoxCount.Size = new System.Drawing.Size(293, 167);
            this.groupBoxCount.TabIndex = 9;
            this.groupBoxCount.TabStop = false;
            this.groupBoxCount.Text = "用户设置";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(99, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "图片URL";
            // 
            // _textBoxUrl
            // 
            this._textBoxUrl.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._textBoxUrl.Location = new System.Drawing.Point(10, 39);
            this._textBoxUrl.MaxLength = 65535;
            this._textBoxUrl.Multiline = true;
            this._textBoxUrl.Name = "_textBoxUrl";
            this._textBoxUrl.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._textBoxUrl.Size = new System.Drawing.Size(277, 65);
            this._textBoxUrl.TabIndex = 6;
            this._textBoxUrl.Text = "http://202.203.209.96/vimgs/";
            // 
            // groupBoxCourseSet
            // 
            this.groupBoxCourseSet.Controls.Add(this._radioButtonByHand);
            this.groupBoxCourseSet.Controls.Add(this._radioButtonAuto);
            this.groupBoxCourseSet.Location = new System.Drawing.Point(12, 185);
            this.groupBoxCourseSet.Name = "groupBoxCourseSet";
            this.groupBoxCourseSet.Size = new System.Drawing.Size(293, 48);
            this.groupBoxCourseSet.TabIndex = 10;
            this.groupBoxCourseSet.TabStop = false;
            this.groupBoxCourseSet.Text = "打码设置";
            // 
            // _radioButtonByHand
            // 
            this._radioButtonByHand.AutoSize = true;
            this._radioButtonByHand.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._radioButtonByHand.Location = new System.Drawing.Point(161, 20);
            this._radioButtonByHand.Name = "_radioButtonByHand";
            this._radioButtonByHand.Size = new System.Drawing.Size(103, 23);
            this._radioButtonByHand.TabIndex = 18;
            this._radioButtonByHand.Text = "手动打码";
            this._radioButtonByHand.UseVisualStyleBackColor = true;
            // 
            // _radioButtonAuto
            // 
            this._radioButtonAuto.AutoSize = true;
            this._radioButtonAuto.Checked = true;
            this._radioButtonAuto.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._radioButtonAuto.Location = new System.Drawing.Point(11, 20);
            this._radioButtonAuto.Name = "_radioButtonAuto";
            this._radioButtonAuto.Size = new System.Drawing.Size(103, 23);
            this._radioButtonAuto.TabIndex = 17;
            this._radioButtonAuto.TabStop = true;
            this._radioButtonAuto.Text = "自动打码";
            this._radioButtonAuto.UseVisualStyleBackColor = true;
            // 
            // toolTip_picture
            // 
            this.toolTip_picture.IsBalloon = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(99, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 19);
            this.label1.TabIndex = 8;
            this.label1.Text = "图片格式";
            // 
            // _textBoxPictureType
            // 
            this._textBoxPictureType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._textBoxPictureType.Location = new System.Drawing.Point(11, 129);
            this._textBoxPictureType.MaxLength = 20;
            this._textBoxPictureType.Name = "_textBoxPictureType";
            this._textBoxPictureType.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._textBoxPictureType.Size = new System.Drawing.Size(275, 26);
            this._textBoxPictureType.TabIndex = 9;
            this._textBoxPictureType.Text = "png";
            // 
            // FormSet
            // 
            this.AcceptButton = this.buttonOK;
            this.ClientSize = new System.Drawing.Size(316, 277);
            this.Controls.Add(this.groupBoxCourseSet);
            this.Controls.Add(this.groupBoxCount);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            this.groupBoxCount.ResumeLayout(false);
            this.groupBoxCount.PerformLayout();
            this.groupBoxCourseSet.ResumeLayout(false);
            this.groupBoxCourseSet.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}

