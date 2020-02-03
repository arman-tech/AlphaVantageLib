using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.DataAccess.Interfaces
{
    public interface IRepositoryLogging<EventArgs>
    {
        event EventHandler<EventArgs> BeginExecute;
        event EventHandler<EventArgs> EndExecute;
        event EventHandler<EventArgs> Info;
    }
}
