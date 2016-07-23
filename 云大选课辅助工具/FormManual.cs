using System;
using System.Windows.Forms;

namespace 云大选课辅助工具
{
    public partial class FormManual : Form
    {
        public string CaptchaValue;
        private readonly string _token;
        private readonly string _userAgent;
        private readonly string _type;

        public FormManual(string url, string token, string userAgent, string type)
        {
            InitializeComponent();
            _pictureBoxCaptcha.Load(url);
            _token = token;
            _userAgent = userAgent;
            _type = type;
        }

        private void _buttonOK_Click(object sender, EventArgs e)
        {
            CaptchaValue = _textBoxCaptcha.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FormManual_KeyDown(object sender, KeyPressEventArgs e)
        {
            //如果输入的是回车键  
            if (e.KeyChar == (char) Keys.Enter)
            {
                e.Handled = true;
                _buttonOK_Click(null, null);
            }
            //如果输入的是空格键
            else if (e.KeyChar == (char) Keys.Space)
            {
                e.Handled = true;
                _pictureBoxCaptcha_Click(null, null);
            }
        }

        private void _pictureBoxCaptcha_Click(object sender, EventArgs e)
        {
            try
            {
                var responseData =
                    ClassHttp.HttpGet(FormMain.SelectCourseCaptchaUrl, FormMain.UrpIp, FormMain.SelectUrl,
                        "application/json, text/plain, */*",
                        _token, _userAgent).Replace("\"", "").Trim();
                _pictureBoxCaptcha.Load(FormMain.ImgUrl + responseData + "." + _type);
            }
            catch (Exception ex)
            {

            }
        }

        private void _textBoxCaptcha_KeyDown(object sender, KeyPressEventArgs e)
        {
            //如果输入的是回车键  
            if (e.KeyChar == (char) Keys.Enter)
            {
                e.Handled = true;
                _buttonOK_Click(null, null);
            }
            //如果输入的是空格键
            else if (e.KeyChar == (char) Keys.Space)
            {
                e.Handled = true;
                _pictureBoxCaptcha_Click(null, null);
            }
        }
    }
}
