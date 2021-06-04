using MakeTopmost.Client.Wpf.Models;

namespace MakeTopmost.Client.Wpf.Contracts
{
    public interface IHotKeyService
    {
        bool RegisterHotKey(nint hWnd, int hotKeyId, FsModifier fsModifier, int key);
    }
}