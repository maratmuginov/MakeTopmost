using MakeTopmost.Client.Windows.Forms;
using MakeTopmost.Client.Windows.Services;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace MakeTopmost.Client.Windows
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Stream iconStream = GetIconStream();
            Icon icon = new(iconStream);
            TrayIcon trayIcon = new(icon);
            Win32HotKeyService hotKeyService = new();
            Win32WindowPosService windowPosService = new();
            ShellForm shellForm = new(trayIcon, windowPosService, hotKeyService);
            
            Application.Run(shellForm);
        }

        private static Stream GetIconStream()
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream("MakeTopmost.Client.Windows.Icon.ico");
        }
    }
}
