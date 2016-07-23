using System;
using System.IO;
using System.Management;
using System.Text;
using System.Windows.Forms;

using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace 云大选课辅助工具
{
    public class ClassRegister
    {
        /// <summary> 个人基本信息类.</summary>
        /// <remarks> windawings, 11/30/2015.</remarks>
        public class BasicInfo
        {
            public string CaptchaType { get; set; }

            public bool IsRegistered { get; set; }

            public string Register { get; set; }

            public string SerialNumber { get; set; }

            public string Sno { get; set; }

            public int Time { get; set; }

            public decimal TimeLeft { get; set; }
            public string FileDate { get; set; }
            public string ImgUrl { get; set; }
            public string PictureType { get; set; }
        }

        /// <summary> 激活码类.</summary>
        /// <remarks> windawings, 11/30/2015.</remarks>
        private class ActiveNumber
        {
            public string SerialNumber { get; set; }

            public int Speed { get; set; }

            public decimal TotalTime { get; set; }
        }

        /// <summary> 签名参数R与S.</summary>
        /// <remarks> windawings, 12/1/2015.</remarks>
        private class SignatureParameters
        {
            public string R { set; get; }
            public string S { set; get; }
        }

        private static readonly DateTime StartTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        /// <summary> 个人信息.</summary>
        public BasicInfo Info = new BasicInfo();

        /// <summary> 当前用户ID.</summary>
        public string CurrentUserId;

        /// <summary> 公钥字节流.</summary>
        private static readonly ECDsaSigner EcDsaSigner = new ECDsaSigner();

        /// <summary> 主要资料完整路径.</summary>
        private const string DataName = @"bin\data.bin";

        /// <summary> 公钥路径.</summary>
        private const string PubliKeyPath = @"bin\public.key";

        /// <summary> 主构造函数.</summary>
        /// <remarks> windawings, 11/30/2015.</remarks>
        public ClassRegister()
        {
            try
            {
                //若存在存储文件，读取
                if (File.Exists(DataName))
                {
                    var jsonStr = ClassEncrypt.DecryptReturn(DataName);
                    Info = (BasicInfo) JsonConvert.DeserializeObject(jsonStr, typeof (BasicInfo));
                }
                else
                {
                    Info.CaptchaType = "manual";
                    Info.PictureType = "png";
                    Info.ImgUrl = FormMain.ImgUrl;
                }
                //若存在公钥文件
                if (File.Exists(PubliKeyPath))
                {
                    var publicKeyBytes = ClassEncrypt.ReadKeyPair(PubliKeyPath);
                    var publicKeyParameters = (ECPublicKeyParameters)PublicKeyFactory.CreateKey(publicKeyBytes);
                    EcDsaSigner.Init(false, publicKeyParameters);
                }
                else
                {
                    MessageBox.Show("公钥导入失败,将无法进行注册验证!若key.public文件丢失请联系作者!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(0);
            }
        }

        /// <summary> 检查注册.</summary>
        /// <remarks> windawings, 11/30/2015.</remarks>
        /// <returns> 是否注册成功.</returns>
        public bool CheckRegister()
        {
            try
            {
                if (!string.IsNullOrEmpty(Info.Register) && !string.IsNullOrEmpty(Info.SerialNumber))
                {
                    var activeNumber =
                        (ActiveNumber)
                            JsonConvert.DeserializeObject(
                                ClassEncrypt.Decrypt(Convert.FromBase64String(Info.SerialNumber)), typeof(ActiveNumber));

                    //获取其他设置信息
                    Info.TimeLeft = activeNumber.TotalTime;
                    Info.SerialNumber = activeNumber.SerialNumber;
                    Info.Time = (activeNumber.Speed < 1) ? 10 : activeNumber.Speed;


                    var time =
                        Convert.ToInt64((new FileInfo(DataName).CreationTime - StartTime).TotalMilliseconds).ToString();
                    
                    //验证激活码
                    if (time.Equals(Info.FileDate) && VerifyData(Info.Register, Info.SerialNumber))
                    {
                        var info =
                            (FormRegister.RegisterInfo)
                                JsonConvert.DeserializeObject(
                                    ClassEncrypt.Decrypt(Convert.FromBase64String(Info.Register)),
                                    typeof (FormRegister.RegisterInfo));

                        CurrentUserId = info.UserCode;

                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary> 检查注册.</summary>
        /// <remarks> windawings, 11/30/2015.</remarks>
        /// <returns> 是否注册成功.</returns>
        public bool CheckRegisterAgain()
        {
            try
            {
                if (!string.IsNullOrEmpty(Info.Register) && !string.IsNullOrEmpty(Info.SerialNumber))
                {
                    var time =
                        Convert.ToInt64((new FileInfo(DataName).CreationTime - StartTime).TotalMilliseconds).ToString();
                    //验证激活码
                    if (time.Equals(Info.FileDate) && VerifyData(Info.Register, Info.SerialNumber))
                    {
                        var info =
                            (FormRegister.RegisterInfo)
                                JsonConvert.DeserializeObject(
                                    ClassEncrypt.Decrypt(Convert.FromBase64String(Info.Register)),
                                    typeof (FormRegister.RegisterInfo));

                        CurrentUserId = info.UserCode;

                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary> 获取CPU序列号.</summary>
        /// <remarks> windawings, 11/30/2015.</remarks>
        /// <returns> cpu序列号.</returns>
        public static string GetCpu()
        {
            var cpuStr = new StringBuilder();
            using (var managementClass = new ManagementClass("Win32_Processor"))
            {
                foreach (var managementObject in managementClass.GetInstances())
                {
                    cpuStr.Append(managementObject.Properties["ProcessorId"].Value);
                    managementObject.Dispose();
                }
            }
            return cpuStr.ToString();
        }

        /// <summary> 获取硬盘序列号.</summary>
        /// <remarks> windawings, 11/30/2015.</remarks>
        /// <returns> 硬盘序列号.</returns>
        public static string GetHardWare()
        {
            var hardWareStr = new StringBuilder();
            using (var managementClass = new ManagementClass("Win32_DiskDrive"))
            {
                foreach (var managementObject in managementClass.GetInstances())
                {
                    hardWareStr.Append(managementObject.Properties["Model"].Value);
                    managementObject.Dispose();
                }
            }
            return hardWareStr.ToString();
        }

        /// <summary> 获取主板序列号.</summary>
        /// <remarks> windawings, 11/30/2015.</remarks>
        /// <returns> 主板序列号.</returns>
        public static string GetBaseBoard()
        {
            var baseBoardStr = new StringBuilder();
            using (var managementClass = new ManagementClass("Win32_baseboard"))
            {
                foreach (var managementObject in managementClass.GetInstances())
                {
                    baseBoardStr.Append(managementObject.Properties["SerialNumber"].Value);
                    managementObject.Dispose();
                }
            }
            return baseBoardStr.ToString();
        }

        /// <summary> 使用公钥验证签名.</summary>
        /// <remarks> Windawings, 11/25/2015.</remarks>
        /// <param name="originalDataStr"> The signature string.</param>
        /// <param name="dataStr">         The serial number string.</param>
        /// <returns> true if it succeeds, false if it fails.</returns>
        public static bool VerifyData(string originalDataStr, string dataStr)
        {
            try
            {
                var orginalDataBytes = Encoding.UTF8.GetBytes(originalDataStr);
                var signJsonStr = ClassEncrypt.Decrypt(Convert.FromBase64String(dataStr));
                var signParam =
                    (SignatureParameters)JsonConvert.DeserializeObject(signJsonStr, typeof(SignatureParameters));


                return EcDsaSigner.VerifySignature(
                    orginalDataBytes,
                    new BigInteger(signParam.R),
                    new BigInteger(signParam.S)
                    );
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        /// <summary> 存储个人信息.</summary>
        /// <remarks> windawings, 11/30/2015.</remarks>
        public void SetRegInfo()
        {
            try
            {
                if (File.Exists(DataName) == false)
                {
                    using (var fileStream = new FileStream(DataName, FileMode.CreateNew))
                    {
                        fileStream.Close();
                        var file = new FileInfo(DataName);
                        var span = file.CreationTime - StartTime;
                        Info.FileDate = Convert.ToInt64(span.TotalMilliseconds).ToString();
                    }
                }
                var jsonStr = JsonConvert.SerializeObject(Info);
                ClassEncrypt.EncryptSave(jsonStr, DataName);
            }
            catch (Exception)
            {
                MessageBox.Show("数据参数出错!");
                Environment.Exit(0);
            }
        }
    }
}