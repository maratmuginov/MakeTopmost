namespace MakeTopmost.Client.Wpf.Contracts
{
    public interface IHotKeyService
    {
        bool RegisterHotKey(nint hWnd, int id, int fsModifiers, uint vk);
    }
}