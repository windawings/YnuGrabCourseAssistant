namespace 云大选课辅助工具
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    public class ClassSingleton
    {
        private static Mutex _mutex;

        private static bool IsFirstInstance()
        {
            _mutex = new Mutex(false, "SingletonApp Mutext");
            return _mutex.WaitOne(TimeSpan.Zero, false);
        }

        private static void OnExit(object sender, EventArgs args)
        {
            _mutex.ReleaseMutex();
            _mutex.Close();
        }

        public static void Run()
        {
            if (IsFirstInstance())
            {
                Application.ApplicationExit += new EventHandler(ClassSingleton.OnExit);
                Application.Run();
            }
        }

        public static void Run(ApplicationContext context)
        {
            if (IsFirstInstance())
            {
                Application.ApplicationExit += new EventHandler(ClassSingleton.OnExit);
                Application.Run(context);
            }
        }

        public static void Run(Form mainForm)
        {
            if (IsFirstInstance())
            {
                Application.ApplicationExit += new EventHandler(ClassSingleton.OnExit);
                Application.Run(mainForm);
            }
            else
            {
                Application.Exit();
            }
        }
    }
}

