using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;

namespace 云大选课辅助工具
{
    internal static class ClassHttp
    {
        /// <summary> 获取验证码图片.</summary>
        /// <remarks> windawings, 11/29/2015.</remarks>
        /// <param name="url"> URL of the document.</param>
        /// <returns> The image.</returns>
        public static Bitmap GetImage(string url)
        {
            Bitmap bitmap;
            try
            {
                var request = WebRequest.Create(url) as HttpWebRequest;
                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        bitmap = new Bitmap(stream);
                    }
                }
            }
            catch (Exception)
            {
                bitmap = null;
            }
            return bitmap;
        }

        /// <summary> HTTP get.</summary>
        /// <remarks> windawings, 11/29/2015.</remarks>
        /// <exception cref="Exception"> Thrown when an exception error condition occurs.</exception>
        /// <param name="url">       URL of the document.</param>
        /// <param name="host">      The host.</param>
        /// <param name="referer">   The referer.</param>
        /// <param name="accept">    The accept.</param>
        /// <param name="token">     用户标识.</param>
        /// <param name="userAgent"> 用户代理.</param>
        /// <returns> 服务器返回消息.</returns>
        public static string HttpGet(string url, string host, string referer, string accept, string token,
            string userAgent)
        {
            string responseData = null;
            try
            {
                var request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                if (string.IsNullOrEmpty(userAgent) == false)
                {
                    request.UserAgent = userAgent;
                }
                if (string.IsNullOrEmpty(host) == false)
                {
                    request.Host = host;
                }
                if (string.IsNullOrEmpty(referer) == false)
                {
                    request.Referer = referer;
                }
                if (string.IsNullOrEmpty(accept) == false)
                {
                    request.Accept = accept;
                }
                if (string.IsNullOrEmpty(token) == false)
                {
                    request.Headers.Add(token);
                }
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            responseData = reader.ReadToEnd();
                        }
                    }
                    response.Close();
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    using (var response = ex.Response as HttpWebResponse)
                    {
                        using (var reader2 = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            responseData = reader2.ReadToEnd();
                        }
                        response.Close();
                        return responseData;
                    }
                }
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                return string.Format("{\"{0}\":\"{1}\"}", "error_description", ex.Message);
            }
            return responseData;
        }

        /// <summary> HTTP post.</summary>
        /// <remarks> windawings, 11/29/2015.</remarks>
        /// <exception cref="Exception"> Thrown when an exception error condition occurs.</exception>
        /// <param name="url">         目标地址.</param>
        /// <param name="host">        The host.</param>
        /// <param name="referer">     参考.</param>
        /// <param name="postData">    上传数据.</param>
        /// <param name="accept">      接收格式.</param>
        /// <param name="contentType"> 数据类型.</param>
        /// <param name="token">       认证标识.</param>
        /// <param name="userAgent">   用户代理.</param>
        /// <returns> 服务器返回消息.</returns>
        public static string HttpPost(string url, string referer, string postData, string accept, string contentType, string token, string userAgent)
        {
            string responseData = null;
            try
            {
                var request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = contentType;
                request.Host = FormMain.UrpIp;
                request.Referer = referer;
                request.Headers.Add("Pragma", "no-cache");
                request.Headers.Add("Accept-Language", "zh-CN");
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                if (string.IsNullOrEmpty(userAgent) == false)
                {
                    request.UserAgent = userAgent;
                }
                if (string.IsNullOrEmpty(accept)==false)
                {
                    request.Accept = accept;
                }
                if (string.IsNullOrEmpty(token) == false)
                {
                    request.Headers.Add(token);
                }
                var bytes = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if ((response.StatusCode == HttpStatusCode.OK) || (response.StatusCode == HttpStatusCode.BadRequest))
                    {
                        using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            responseData = reader.ReadToEnd();
                        }
                    }
                    response.Close();
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    using (var response = ex.Response as HttpWebResponse)
                    {
                        using (var reader2 = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            responseData = reader2.ReadToEnd();
                        }
                        response.Close();
                        return responseData;
                    }
                }
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                return string.Format("{\"{0}\":\"{1}\"}", "error_description", ex.Message);
            }
            return responseData;
        }
    }
}

