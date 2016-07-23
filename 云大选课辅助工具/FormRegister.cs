using System;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace 云大选课辅助工具
{
    public partial class FormRegister : Form
    {
        public class RegisterInfo
        {
            public string MachineSerialNumber { get; set; }

            public string UserCode { get; set; }
        }

        /// <summary> The 注册类.</summary>
        public ClassRegister Register;
        public FormRegister(ClassRegister register, bool login)
        {
            if (login)
            {
                InitializeComponent();
                Register = register;
                //获取已注册学号信息
                if (String.IsNullOrEmpty(Register.CurrentUserId))
                {
                    _textBoxStuNo.Text = Register.Info.Sno;
                }
                else
                {
                    _textBoxStuNo.Text = Register.CurrentUserId;
                }

                //获取机器码
                if (String.IsNullOrEmpty(Register.Info.Register) == false)
                {
                    var plainStr = ClassEncrypt.Decrypt(Convert.FromBase64String(Register.Info.Register));
                    var jsonObject = (RegisterInfo)JsonConvert.DeserializeObject(plainStr, typeof(RegisterInfo));
                    if (jsonObject.UserCode.Equals(Register.Info.Sno))
                    {
                        _textBoxSerialNumber.Text = Register.Info.Register;
                        //获取激活码
                        if (Register.Info.IsRegistered)
                        {
                            _textBoxSinature.Text = Register.Info.SerialNumber;
                            _textBoxSinature.ReadOnly = true;
                            _labelTimeLeftValue.Text = Register.Info.TimeLeft + " 秒";
                        }
                    }
                    else
                    {
                        _buttonCancel.Enabled = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("请先完成网页登陆!");
                Dispose();
            }
        }
        private void _buttonGetSerialNumber_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(_textBoxStuNo.Text.Trim()) && !Register.Info.IsRegistered) && string.IsNullOrEmpty(_textBoxSerialNumber.Text))
                {
                    var info = new RegisterInfo
                    {
                        MachineSerialNumber =
                            Convert.ToBase64String(
                                ClassEncrypt.Encrypt(
                                    ClassRegister.GetCpu() + ClassRegister.GetHardWare() + ClassRegister.GetBaseBoard() +
                                    ClassTime.GetNetWorkTime()
                                    )
                                ),
                        UserCode = _textBoxStuNo.Text.Trim()
                    };
                    var encryptStr = JsonConvert.SerializeObject(info);
                    Register.Info.Register = Convert.ToBase64String(ClassEncrypt.Encrypt(encryptStr));
                    _textBoxSerialNumber.Text = Register.Info.Register;
                    Register.SetRegInfo();
                }
                else if (_textBoxStuNo.Text.Trim().Length == 0)
                {
                    MessageBox.Show("请先输入注册学号!");
                }
                else if (Register.Info.IsRegistered)
                {
                    MessageBox.Show("软件已注册，若试用期满请联系作者!");
                }
                else
                {
                    MessageBox.Show("机器码已存在!");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void _buttonSign_Click(object sender, EventArgs e)
        {
            try
            {
                if (Register.Info.IsRegistered)
                {
                    MessageBox.Show("软件已注册，若试用期满请联系作者!");
                }
                else if (!string.IsNullOrEmpty(_textBoxSinature.Text.Trim()))
                {
                    Register.Info.SerialNumber = _textBoxSinature.Text.Trim();
                    if (Register.CheckRegister())
                    {
                        Register.Info.IsRegistered = true;
                        Register.SetRegInfo();
                        MessageBox.Show("注册成功!");
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("注册码无效!");
                    }
                }
                else
                {
                    MessageBox.Show("请填写注册码!");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void _buttonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Register.Info.IsRegistered)
                {
                    if (MessageBox.Show("注销不可恢复!即无法用原来的注册码重新完成注册,确认继续?", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        Register.Info.Register = "";
                        Register.Info.IsRegistered = false;
                        Register.Info.SerialNumber = "";
                        Register.CurrentUserId = "";
                        Register.Info.TimeLeft = 0M;
                        Register.SetRegInfo();
                        _textBoxSinature.Text = "";
                        _textBoxSerialNumber.Text = "";
                        _textBoxStuNo.Text = Register.Info.Sno;
                        _labelTimeLeftValue.Text = Register.Info.TimeLeft + " 秒";
                        _textBoxSinature.ReadOnly = false;
                        MessageBox.Show("注销成功!");
                    }
                }
                else
                {
                    MessageBox.Show("尚未注册!");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void _textBoxStuNo_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.A))
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private void _textBoxSerialNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.A))
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private void _textBoxSinature_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.A))
            {
                ((TextBox)sender).SelectAll();
            }
        }
    }
}
