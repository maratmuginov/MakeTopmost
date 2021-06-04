using MakeTopmost.Client.Wpf.Contracts;
using MakeTopmost.Client.Wpf.Services;
using MakeTopmost.Client.Wpf.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Windows;

namespace MakeTopmost.Client.Wpf
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (AnotherInstanceExists())
                Shutdown();

            var services = ConfigureServices();

            var notifyIcon = services.GetRequiredService<INotifyIcon>();
            notifyIcon.ExitRequested += Shutdown;

            base.OnStartup(e);
        }

        private static IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddScoped<IWindowPositioner, Win32WindowPositioner>()
                .AddScoped<IHotKeyService, Win32HotKeyService>()
                .AddScoped<INotifyIcon, NotifyIconView>()
                .BuildServiceProvider();
        }

        private static bool AnotherInstanceExists()
        {
            Mutex mutex = new(false, "e4c6073f-c97b-403c-abf3-95c7e56acc86", out bool createdNew);

            GC.KeepAlive(mutex);

            return createdNew is false;
        }
    }
}
