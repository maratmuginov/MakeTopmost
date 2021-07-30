using MakeTopmost.Lib.Client.Contracts;
using MakeTopmost.Lib.Client.Models;
using Vanara.PInvoke;
using static Vanara.PInvoke.User32;

namespace MakeTopmost.Client.Windows.Services
{
    public class Win32WindowPosService : IWindowPosService
    {
        public bool SetForegroundWindowPos(InsertAfter insertAfter)
        {
            HWND hWndInsertAfter = (nint)insertAfter;
            HWND hWnd = GetForegroundWindow();
            const SetWindowPosFlags uFlags = SetWindowPosFlags.SWP_NOSIZE
                                             | SetWindowPosFlags.SWP_NOMOVE
                                             | SetWindowPosFlags.SWP_SHOWWINDOW;
            return SetWindowPos(hWnd, hWndInsertAfter, 0, 0, 0, 0, uFlags);
        }
    }
}
