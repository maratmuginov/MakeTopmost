namespace MakeTopmost.Client.Wpf.Contracts
{
    public delegate void ExitRequested();

    public interface INotifyIcon
    {
        event ExitRequested ExitRequested;
    }
}