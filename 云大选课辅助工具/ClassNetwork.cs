using System;
using System.Net.NetworkInformation;
namespace 云大选课辅助工具
{
    internal static class ClassNetwork
    {
        public static bool IsConnectInternet()
        {
            try
            {
                var ping = new Ping();
                var i = 3;
                while (i > 0)
                {
                    if (ping.Send(FormMain.UrpIp).Status == IPStatus.Success)
                    {
                        return true;
                    }
                    i--;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

