using MakeTopmost.Client.Wpf.Contracts;
using MakeTopmost.Client.Wpf.Models;
using System;
using System.Runtime.InteropServices;

namespace MakeTopmost.Client.Wpf.Services
{
    public class Win32WindowPositioner : IWindowPositioner
    {
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(nint hWnd, nint hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        private static extern nint GetForegroundWindow();

        public bool SetForegroundWindowPosition(InsertAfter insertAfter = InsertAfter.NoTopMost)
        {
            nint hWndInsertAfter = new IntPtr((int)insertAfter);
            nint activeWindowPointer = GetForegroundWindow();

            const uint uFlags = (uint)(Swp.NoSize | Swp.NoMove | Swp.ShowWindow);

            return SetWindowPos(activeWindowPointer, hWndInsertAfter, 0, 0, 0, 0, uFlags);
        }
    }
}
