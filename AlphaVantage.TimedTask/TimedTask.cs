using AlphaVantage.Common.Models;
using AlphaVantage.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AlphaVantage.TimedTask
{
    public class TimedTask : IAvTask<AvConfigFileUri, AvConfigFileObj, TimedTaskArgs>
    {

        public event EventHandler<TimedTaskArgs> BeginExecute;
        public event EventHandler<TimedTaskArgs> EndExecute;
        public event EventHandler<TimedTaskArgs> Info;

        public const int OneMinuteInterval = 60000;

        private string _configFileData;
        private AvConfigFileObj _configFileObj;

        public string ConfigFileData => _configFileData;

        public AvConfigFileObj ConfigFile => _configFileObj;

        public string ConfigFileLocation { get; set; }


        public async Task Execute(Action<AvConfigFileUri> actionToExecute, AvConfigFileObj configFile)
        {
            if(configFile != null)
            {
                _configFileObj = configFile;
            }

            EnsurePreConditionsAreMet();

            var tasks = new List<Task>();
            const int getLastIndex = 1;
            int batchSize = _configFileObj.ApiCallsPerMinuteAllowed;
            const int millisecondsPerBatch = OneMinuteInterval;   // equates to one minute.

            var executionGuid = Guid.NewGuid();
            BeginExecute?.Invoke(this, new TimedTaskArgs(executionGuid, 
                                    TimedTaskArgs.TimeTaskType.Info, 
                                    $"Beginning to execute task."));

            var watch = new Stopwatch();
            for(var i = 0; i < _configFileObj.Uris.Count; i += batchSize)
            {
                watch.Reset();
                watch.Start();

                var endRange = i >= (_configFileObj.Uris.Count - 1) ? getLastIndex : batchSize;
                endRange = (endRange > _configFileObj.Uris.Count) ? _configFileObj.Uris.Count : endRange;

                var valueRange = _configFileObj.Uris.GetRange(i, endRange);
                foreach (var uri in valueRange)
                {
                    Info?.Invoke(this, new TimedTaskArgs(executionGuid, 
                                    uri.Id,
                                    uri.Uri, 
                                    uri.Country, 
                                    TimedTaskArgs.TimeTaskType.Info, $"starting execution"));

                    var task = new Task(() => {
                        actionToExecute(uri);
                    });
                    task.Start();
                    tasks.Add(task);
                }

                watch.Stop();
                var ticksInSeconds = watch.ElapsedMilliseconds;

                // sleep at most for what should be milliSecondsPerBatch
                if (i != _configFileObj.Uris.Count - 1)
                {
                    Thread.Sleep((int)(millisecondsPerBatch - watch.ElapsedMilliseconds));
                    Info?.Invoke(this, new TimedTaskArgs(executionGuid, 
                                    TimedTaskArgs.TimeTaskType.Info, 
                                    $"index:{i}, sleep {millisecondsPerBatch - ticksInSeconds}, {DateTime.UtcNow.ToString("dddd, dd MMMM yyyy HH:mm:ss")}"));
                }
            }

            await Task.WhenAll(tasks);
            EndExecute?.Invoke(this, new TimedTaskArgs(executionGuid, 
                                TimedTaskArgs.TimeTaskType.Info, 
                                $"Task is running in the background."));

        }


        public string ReadConfigFile()
        {
            if(string.IsNullOrWhiteSpace(ConfigFileLocation))
            {
                throw new ArgumentNullException(nameof(ReadConfigFile), $"{nameof(ConfigFileData)} can't be null or empty");
            }

            _configFileData = File.ReadAllText(ConfigFileLocation);
            _configFileObj = JsonConvert.DeserializeObject<AvConfigFileObj>(ConfigFileData);
            return ConfigFileData;
        }

        private void EnsurePreConditionsAreMet()
        {
            if(_configFileObj == null)
            {
                throw new ArgumentNullException(nameof(EnsurePreConditionsAreMet), $"{nameof(ConfigFileData)} can't be null.");
            }

            if (string.IsNullOrWhiteSpace(_configFileData))
            {
                throw new ArgumentNullException(nameof(EnsurePreConditionsAreMet), $"{nameof(ConfigFileData)} can't be null or empty.");
            }
        }
    }
}
