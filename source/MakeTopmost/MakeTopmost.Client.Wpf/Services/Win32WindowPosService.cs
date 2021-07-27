using MakeTopmost.Client.Wpf.Contracts;
using MakeTopmost.Client.Wpf.Models;
using System;
using Vanara.PInvoke;
using static Vanara.PInvoke.User32;
using Swp = Vanara.PInvoke.User32.SetWindowPosFlags;

namespace MakeTopmost.Client.Wpf.Services
{
    public class Win32WindowPosService : IWindowPosService
    {
        public bool SetForegroundWindowPosition(InsertAfter insertAfter = InsertAfter.NoTopMost)
        {
            HWND hWndInsertAfter = new IntPtr((int)insertAfter);
            HWND activeWindowPointer = GetForegroundWindow();
            const Swp uFlags = Swp.SWP_NOSIZE | Swp.SWP_NOMOVE | Swp.SWP_SHOWWINDOW;
            return SetWindowPos(activeWindowPointer, hWndInsertAfter, 0, 0, 0, 0, uFlags);
        }
    }
}
