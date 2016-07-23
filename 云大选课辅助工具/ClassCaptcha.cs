using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace 云大选课辅助工具
{
    public static class ClassCaptcha
    {
        /// <summary> 验证码类.</summary>
        /// <remarks> windawings, 11/30/2015.</remarks>
        private class Captcha
        {
            public int Id { get; set; }

            public string ImgGuid { get; set; }

            public string Value { get; set; }
        }

        /// <summary> Tesseract-OCR 配置文件.</summary>
        //private static readonly string TesseractConfig;
        /// <summary> 验证码字典.</summary>
        private static readonly Dictionary<string, string> CaptchaDic = new Dictionary<string, string>();


        /// <summary> 静态构造函数.</summary>
        /// <remarks> windawings, 11/30/2015.</remarks>
        static ClassCaptcha()
        {
            //获取字典文件
            var captchaJson =
                (Captcha[])
                    JsonConvert.DeserializeObject(ClassEncrypt.DecryptReturn(@"bin\captcha.bin"),
                        typeof (Captcha[]));
            foreach (var captcha in captchaJson)
            {
                if (CaptchaDic.ContainsKey(captcha.ImgGuid))
                {
                    CaptchaDic[captcha.ImgGuid] = captcha.Value;
                }
                else
                {
                    CaptchaDic.Add(captcha.ImgGuid, captcha.Value);
                }
            }
        }

        /// <summary> 通过字典获取验证码值.</summary>
        /// <remarks> windawings, 11/30/2015.</remarks>
        /// <param name="imgGuid"> 图像本地ID.</param>
        /// <returns> 验证码值.</returns>
        public static string GetValueByDict(string imgGuid)
        {
            try
            {
                return CaptchaDic[imgGuid];
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

