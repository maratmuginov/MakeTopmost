using MakeTopmost.Client.Wpf.Contracts;
using Vanara.PInvoke;

namespace MakeTopmost.Client.Wpf.Services
{
    public class Win32HotKeyService : IHotKeyService
    {
        public bool RegisterHotKey(nint hWnd, int id, int fsModifiers, uint vk)
        {
            return User32.RegisterHotKey(hWnd, id, (User32.HotKeyModifiers) fsModifiers, vk);
        }
    }
}
