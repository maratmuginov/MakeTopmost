using System;

namespace MakeTopmost.Client.Wpf.Models
{
    [Flags]
    public enum Swp : uint
    {
        NoSize = 0x0001,
        NoMove = 0x0002,
        ShowWindow = 0x0040
    }
}