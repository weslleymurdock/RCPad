using System;

namespace Pad.Interfaces
{
    public interface IPadTapped
    {
        event EventHandler<EventArgs> Tapped;
    }
}
