using 云大选课辅助工具.Properties;

namespace 云大选课辅助工具
{
    partial class FormRegister
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
            this._labelTimeLeftValue = new System.Windows.Forms.Label();
            this._labelTimeLeft = new System.Windows.Forms.Label();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._buttonSign = new System.Windows.Forms.Button();
            this._textBoxSinature = new System.Windows.Forms.TextBox();
            this._labelSignature = new System.Windows.Forms.Label();
            this._buttonGetSerialNumber = new System.Windows.Forms.Button();
            this._textBoxStuNo = new System.Windows.Forms.TextBox();
            this._labelStuNo = new System.Windows.Forms.Label();
            this._textBoxSerialNumber = new System.Windows.Forms.TextBox();
            this._labelSerialNumber = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _labelTimeLeftValue
            // 
            this._labelTimeLeftValue.AutoSize = true;
            this._labelTimeLeftValue.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._labelTimeLeftValue.Location = new System.Drawing.Point(303, 336);
            this._labelTimeLeftValue.Name = "_labelTimeLeftValue";
            this._labelTimeLeftValue.Size = new System.Drawing.Size(49, 20);
            this._labelTimeLeftValue.TabIndex = 22;
            this._labelTimeLeftValue.Text = "0 次";
            // 
            // _labelTimeLeft
            // 
            this._labelTimeLeft.AutoSize = true;
            this._labelTimeLeft.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._labelTimeLeft.Location = new System.Drawing.Point(64, 336);
            this._labelTimeLeft.Name = "_labelTimeLeft";
            this._labelTimeLeft.Size = new System.Drawing.Size(129, 20);
            this._labelTimeLeft.TabIndex = 21;
            this._labelTimeLeft.Text = "剩余使用次数";
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Location = new System.Drawing.Point(239, 372);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(168, 28);
            this._buttonCancel.TabIndex = 20;
            this._buttonCancel.Text = "注销注册";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonCancel.Click += new System.EventHandler(this._buttonCancel_Click);
            // 
            // _buttonSign
            // 
            this._buttonSign.Location = new System.Drawing.Point(25, 372);
            this._buttonSign.Name = "_buttonSign";
            this._buttonSign.Size = new System.Drawing.Size(168, 28);
            this._buttonSign.TabIndex = 19;
            this._buttonSign.Text = "注册";
            this._buttonSign.UseVisualStyleBackColor = true;
            this._buttonSign.Click += new System.EventHandler(this._buttonSign_Click);
            // 
            // _textBoxSinature
            // 
            this._textBoxSinature.Location = new System.Drawing.Point(8, 230);
            this._textBoxSinature.Multiline = true;
            this._textBoxSinature.Name = "_textBoxSinature";
            this._textBoxSinature.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._textBoxSinature.Size = new System.Drawing.Size(433, 90);
            this._textBoxSinature.TabIndex = 18;
            this._textBoxSinature.KeyDown += new System.Windows.Forms.KeyEventHandler(this._textBoxSinature_KeyDown);
            // 
            // _labelSignature
            // 
            this._labelSignature.AutoSize = true;
            this._labelSignature.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._labelSignature.Location = new System.Drawing.Point(8, 207);
            this._labelSignature.Name = "_labelSignature";
            this._labelSignature.Size = new System.Drawing.Size(109, 20);
            this._labelSignature.TabIndex = 17;
            this._labelSignature.Text = "超长注册码";
            // 
            // _buttonGetSerialNumber
            // 
            this._buttonGetSerialNumber.Location = new System.Drawing.Point(332, 6);
            this._buttonGetSerialNumber.Name = "_buttonGetSerialNumber";
            this._buttonGetSerialNumber.Size = new System.Drawing.Size(109, 26);
            this._buttonGetSerialNumber.TabIndex = 16;
            this._buttonGetSerialNumber.Text = "获取机器码";
            this._buttonGetSerialNumber.UseVisualStyleBackColor = true;
            this._buttonGetSerialNumber.Click += new System.EventHandler(this._buttonGetSerialNumber_Click);
            // 
            // _textBoxStuNo
            // 
            this._textBoxStuNo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._textBoxStuNo.Location = new System.Drawing.Point(103, 6);
            this._textBoxStuNo.MaxLength = 12;
            this._textBoxStuNo.Name = "_textBoxStuNo";
            this._textBoxStuNo.ReadOnly = true;
            this._textBoxStuNo.Size = new System.Drawing.Size(214, 26);
            this._textBoxStuNo.TabIndex = 15;
            this._textBoxStuNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this._textBoxStuNo_KeyDown);
            // 
            // _labelStuNo
            // 
            this._labelStuNo.AutoSize = true;
            this._labelStuNo.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._labelStuNo.Location = new System.Drawing.Point(8, 8);
            this._labelStuNo.Name = "_labelStuNo";
            this._labelStuNo.Size = new System.Drawing.Size(89, 20);
            this._labelStuNo.TabIndex = 14;
            this._labelStuNo.Text = "注册学号";
            // 
            // _textBoxSerialNumber
            // 
            this._textBoxSerialNumber.Location = new System.Drawing.Point(12, 67);
            this._textBoxSerialNumber.Multiline = true;
            this._textBoxSerialNumber.Name = "_textBoxSerialNumber";
            this._textBoxSerialNumber.ReadOnly = true;
            this._textBoxSerialNumber.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._textBoxSerialNumber.Size = new System.Drawing.Size(433, 137);
            this._textBoxSerialNumber.TabIndex = 13;
            this._textBoxSerialNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this._textBoxSerialNumber_KeyDown);
            // 
            // _labelSerialNumber
            // 
            this._labelSerialNumber.AutoSize = true;
            this._labelSerialNumber.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._labelSerialNumber.Location = new System.Drawing.Point(8, 44);
            this._labelSerialNumber.Name = "_labelSerialNumber";
            this._labelSerialNumber.Size = new System.Drawing.Size(109, 20);
            this._labelSerialNumber.TabIndex = 12;
            this._labelSerialNumber.Text = "超长机器码";
            // 
            // FormRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 406);
            this.Controls.Add(this._labelTimeLeftValue);
            this.Controls.Add(this._labelTimeLeft);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonSign);
            this.Controls.Add(this._textBoxSinature);
            this.Controls.Add(this._labelSignature);
            this.Controls.Add(this._buttonGetSerialNumber);
            this.Controls.Add(this._textBoxStuNo);
            this.Controls.Add(this._labelStuNo);
            this.Controls.Add(this._textBoxSerialNumber);
            this.Controls.Add(this._labelSerialNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = global::云大选课辅助工具.Properties.Resources.school;
            this.MaximizeBox = false;
            this.Name = "FormRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "注册(联系方式请点击关于软件的提交反馈)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _labelTimeLeftValue;
        private System.Windows.Forms.Label _labelTimeLeft;
        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.Button _buttonSign;
        private System.Windows.Forms.TextBox _textBoxSinature;
        private System.Windows.Forms.Label _labelSignature;
        private System.Windows.Forms.Button _buttonGetSerialNumber;
        private System.Windows.Forms.TextBox _textBoxStuNo;
        private System.Windows.Forms.Label _labelStuNo;
        private System.Windows.Forms.TextBox _textBoxSerialNumber;
        private System.Windows.Forms.Label _labelSerialNumber;

    }
}