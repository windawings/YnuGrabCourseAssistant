using System.Windows.Forms;

namespace 云大选课辅助工具
{
    partial class FormManual
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
            this._pictureBoxCaptcha = new System.Windows.Forms.PictureBox();
            this._textBoxCaptcha = new System.Windows.Forms.TextBox();
            this._buttonOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBoxCaptcha)).BeginInit();
            this.SuspendLayout();
            // 
            // _pictureBoxCaptcha
            // 
            this._pictureBoxCaptcha.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this._pictureBoxCaptcha.Location = new System.Drawing.Point(12, 12);
            this._pictureBoxCaptcha.Name = "_pictureBoxCaptcha";
            this._pictureBoxCaptcha.Size = new System.Drawing.Size(150, 50);
            this._pictureBoxCaptcha.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._pictureBoxCaptcha.TabIndex = 0;
            this._pictureBoxCaptcha.TabStop = false;
            this._pictureBoxCaptcha.Click += new System.EventHandler(this._pictureBoxCaptcha_Click);
            // 
            // _textBoxCaptcha
            // 
            this._textBoxCaptcha.Location = new System.Drawing.Point(12, 70);
            this._textBoxCaptcha.Name = "_textBoxCaptcha";
            this._textBoxCaptcha.Size = new System.Drawing.Size(73, 21);
            this._textBoxCaptcha.TabIndex = 1;
            _textBoxCaptcha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._textBoxCaptcha_KeyDown);
            // 
            // _buttonOK
            // 
            this._buttonOK.Location = new System.Drawing.Point(91, 70);
            this._buttonOK.Name = "_buttonOK";
            this._buttonOK.Size = new System.Drawing.Size(71, 23);
            this._buttonOK.TabIndex = 2;
            this._buttonOK.Text = "确定";
            this._buttonOK.UseVisualStyleBackColor = true;
            this._buttonOK.Click += new System.EventHandler(this._buttonOK_Click);
            _buttonOK.DialogResult = DialogResult.OK;
            // 
            // FormManual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(173, 98);
            this.Controls.Add(this._buttonOK);
            this.Controls.Add(this._textBoxCaptcha);
            this.Controls.Add(this._pictureBoxCaptcha);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "FormManual";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "输入验证码";
            KeyPress+= new System.Windows.Forms.KeyPressEventHandler(this.FormManual_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this._pictureBoxCaptcha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox _pictureBoxCaptcha;
        private System.Windows.Forms.TextBox _textBoxCaptcha;
        private System.Windows.Forms.Button _buttonOK;
    }
}