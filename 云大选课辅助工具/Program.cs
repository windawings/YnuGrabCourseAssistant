using System;
using System.Windows.Forms;
namespace 云大选课辅助工具
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (ClassNetwork.IsConnectInternet())
            {
                ClassSingleton.Run(new FormMain());
            }
            else if (MessageBox.Show("未能PING到URP,是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                     DialogResult.Yes)
            {

                ClassSingleton.Run(new FormMain());
            }
        }
    }
}

