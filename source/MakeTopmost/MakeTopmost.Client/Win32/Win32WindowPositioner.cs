using System;
using System.Runtime.InteropServices;
using MakeTopmost.Client.Contracts;

namespace MakeTopmost.Client.Win32
{
    public class Win32WindowPositioner : IWindowPositioner
    {
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(nint hWnd, nint hWndInsertAfter,
            int x, int y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        private static extern nint GetForegroundWindow();

        public void SetForegroundWindowPos(InsertAfter insertAfter)
        {
            var hWndInsertAfter = new IntPtr((int) insertAfter);
            nint activeWindowPointer = GetForegroundWindow();
            uint uFlags = (uint)(SwpFlags.NoSize | SwpFlags.NoMove | SwpFlags.ShowWindow);
            SetWindowPos(activeWindowPointer, hWndInsertAfter, 0, 0, 0, 0, uFlags);
        }

        [Flags]
        private enum SwpFlags : uint
        {
            NoSize = 0x0001,
            NoMove = 0x0002,
            ShowWindow = 0x0040
        }
    }
}
