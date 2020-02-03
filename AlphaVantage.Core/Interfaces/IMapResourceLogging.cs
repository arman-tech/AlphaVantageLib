using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Core.Interfaces
{
    public interface IMapResourceLogging<EventArgs>
    {
        event EventHandler<EventArgs> BeginExecute;
        event EventHandler<EventArgs> EndExecute;
        event EventHandler<EventArgs> Info;
    }
}
