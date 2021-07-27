using MakeTopmost.Client.Wpf.Contracts;
using MakeTopmost.Client.Wpf.Models;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace MakeTopmost.Client.Wpf.Views
{
    public partial class NotifyIconView : INotifyIcon
    {
        public event ExitRequested ExitRequested;
        private readonly IWindowPosService _windowPosService;
        private readonly IHotKeyService _hotKeyService;

        public NotifyIconView(IWindowPosService windowPosService, IHotKeyService hotKeyService)
        {
            _windowPosService = windowPosService;
            _hotKeyService = hotKeyService;

            RegisterHotKeys();

            InitializeComponent();
        }

        private void RegisterHotKeys()
        {
            WindowInteropHelper windowInteropHelper = new(this);
            nint hWnd = windowInteropHelper.EnsureHandle();

            HwndSource hWndSource = HwndSource.FromHwnd(hWnd);
            hWndSource?.AddHook(WndProc);

            var tKey = Convert.ToUInt32(KeyInterop.VirtualKeyFromKey(Key.T));

            _hotKeyService.RegisterHotKey(hWnd, 1, 1, tKey);
            _hotKeyService.RegisterHotKey(hWnd, 2, 1 | 4, tKey);
        }

        private IntPtr WndProc(nint hWnd, int msg, nint wParam, nint lParam, ref bool handled)
        {
            if (msg is 0x0312)
                RaiseHotKey(Convert.ToInt32(wParam));

            return IntPtr.Zero;
        }

        private void RaiseHotKey(int id)
        {
            InsertAfter insertAfter = id switch
            {
                1 => InsertAfter.TopMost,
                _ => InsertAfter.NoTopMost
            };

            _windowPosService.SetForegroundWindowPosition(insertAfter);
        }

        private void OnExitButtonClick(object sender, RoutedEventArgs e)
        {
            if (TaskbarIcon.IsVisible)
                TaskbarIcon.Visibility = Visibility.Hidden;

            ExitRequested?.Invoke();
        }
    }
}
