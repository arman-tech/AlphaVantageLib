using System;
using System.Threading.Tasks;

namespace AlphaVantage.Core.Interfaces
{
    public interface IAvTask<Uri, ConfigFileObj, EventArgs>
    {
        event EventHandler<EventArgs> BeginExecute;
        event EventHandler<EventArgs> EndExecute;
        event EventHandler<EventArgs> Info;


        string ConfigFileData { get; }
        ConfigFileObj ConfigFile { get; }
        string ConfigFileLocation { get; set; }

        string ReadConfigFile();

        Task Execute(Action<Uri> actionToExecute, ConfigFileObj configFile);
    }
}
