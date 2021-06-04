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
        private readonly IWindowPositioner _windowPositioner;
        private readonly IHotKeyService _hotKeyService;

        public NotifyIconView(IWindowPositioner windowPositioner, IHotKeyService hotKeyService)
        {
            _windowPositioner = windowPositioner;
            _hotKeyService = hotKeyService;

            RegisterHotKeys();

            InitializeComponent();
        }

        private void RegisterHotKeys()
        {
            WindowInteropHelper windowInteropHelper = new(this);
            nint hWnd = windowInteropHelper.EnsureHandle();

            var hWndSource = HwndSource.FromHwnd(hWnd);
            hWndSource?.AddHook(WndProc);

            int tKey = KeyInterop.VirtualKeyFromKey(Key.T);

            _hotKeyService.RegisterHotKey(hWnd, 1, FsModifier.Alt, tKey);
            _hotKeyService.RegisterHotKey(hWnd, 2, FsModifier.Alt | FsModifier.Shift, tKey);
        }

        private IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg is 0x0312)
                RaiseHotKey(wParam.ToInt32());

            return IntPtr.Zero;
        }

        private void RaiseHotKey(int hotKeyId)
        {
            InsertAfter insertAfter = hotKeyId switch
            {
                1 => InsertAfter.TopMost,
                _ => InsertAfter.NoTopMost
            };

            _windowPositioner.SetForegroundWindowPosition(insertAfter);
        }

        private void OnExitButtonClick(object sender, RoutedEventArgs e)
        {
            if (TaskbarIcon.IsVisible)
                TaskbarIcon.Visibility = Visibility.Hidden;

            ExitRequested?.Invoke();
        }
    }
}
