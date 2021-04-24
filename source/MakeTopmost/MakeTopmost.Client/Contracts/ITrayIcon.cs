namespace MakeTopmost.Client.Contracts
{
    public delegate void ExitRequested();
    public interface ITrayIcon
    {
        event ExitRequested ExitRequested;
    }
}