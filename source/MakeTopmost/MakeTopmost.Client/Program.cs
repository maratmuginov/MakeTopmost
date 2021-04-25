using MakeTopmost.Client.Contracts;
using MakeTopmost.Client.Forms;
using MakeTopmost.Client.Win32;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace MakeTopmost.Client
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var mutex = new Mutex(false, "e4c6073f-c97b-403c-abf3-95c7e56acc86", out bool createdNew);

            if (createdNew is false)
                return;

            GC.KeepAlive(mutex);
            RunApplication();
        }

        private static void RunApplication()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = ConfigureServices();
            var shellForm = services.GetRequiredService<ShellForm>();
            shellForm.Hide();

            Application.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddScoped<IHotKeyService, Win32HotKeyService>()
                .AddScoped<IWindowPositioner, Win32WindowPositioner>()
                .AddScoped<ITrayIcon, TrayIcon>()
                .AddScoped<ShellForm>()
                .AddScoped<Icon>(_ => CreateApplicationIcon())
                .BuildServiceProvider();
        }

        private static Icon CreateApplicationIcon()
        {
            var iconAssembly = Assembly.GetExecutingAssembly();
            var iconManifestResourceName = iconAssembly
                .GetManifestResourceNames()
                .FirstOrDefault(manifestResourceName => manifestResourceName.Contains("Icon.ico"));

            if (string.IsNullOrEmpty(iconManifestResourceName))
                throw new Exception();

            var iconManifestResourceStream = iconAssembly.GetManifestResourceStream(iconManifestResourceName);

            if (iconManifestResourceStream is null)
                throw new Exception();

            return new Icon(iconManifestResourceStream);
        }
    }
}
