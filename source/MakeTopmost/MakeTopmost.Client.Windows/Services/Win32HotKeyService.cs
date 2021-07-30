using MakeTopmost.Lib.Client.Contracts;
using Vanara.PInvoke;
using static Vanara.PInvoke.User32;

namespace MakeTopmost.Client.Windows.Services
{
    public class Win32HotKeyService : IHotKeyService
    {
        public bool RegisterHotKey(nint hWnd, int id, int fsModifiers, uint vk)
        {
            return User32.RegisterHotKey(hWnd, id, (HotKeyModifiers) fsModifiers, vk);
        }
    }
}
