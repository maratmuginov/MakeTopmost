using System;
using System.Windows.Forms;
using MakeTopmost.Client.Forms;

namespace MakeTopmost.Client
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var shellForm = new ShellForm();
            shellForm.Hide();
            Application.Run();
        }
    }
}
