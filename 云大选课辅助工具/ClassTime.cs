namespace 云大选课辅助工具
{
    using System;
    using System.Text.RegularExpressions;

    internal static class ClassTime
    {
        private static readonly DateTime StartTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        private static readonly string[] TimeHttpList =
        {
            "http://open.baidu.com/special/time/",
            "http://shijian.duoshitong.com/time.php", 
            "http://www.hko.gov.hk/cgi-bin/gts/time5a.pr?a=1"
        };

        public static string GetNetWorkTime()
        {
            try
            {
                foreach (var str in TimeHttpList)
                {
                    var input = ClassHttp.HttpGet(str, null, null, "text/plain, */*", null, null);
                    var match = new Regex(@"(?<timestamp>\d{13})").Match(input);
                    if (match.Success)
                    {
                        return match.Groups["timestamp"].Value;
                    }
                }
            }
            catch (Exception)
            {
                var span = DateTime.Now - StartTime;
                return Convert.ToInt64(span.TotalMilliseconds).ToString();
            }
            var span2 = DateTime.Now - StartTime;
            return Convert.ToInt64(span2.TotalMilliseconds).ToString();
        }
    }
}

