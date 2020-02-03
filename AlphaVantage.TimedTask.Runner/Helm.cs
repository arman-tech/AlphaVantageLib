using AlphaVantage.Common.Models;
using AlphaVantage.Core.Common;
using AlphaVantage.Core.Exceptions;
using AlphaVantage.Core.Interfaces;
using AlphaVantage.DataAccess.Common;
using AlphaVantage.DataAccess.EventArguments;
using AlphaVantage.DataAccess.Interfaces;
using AlphaVantage.TimedTask.Runner.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace AlphaVantage.TimedTask.Runner
{
    public partial class Helm : IHelm
    {
        //private readonly IAvProcessFactory _factoryMethod;
        private readonly IAvTask<AvConfigFileUri, AvConfigFileObj, TimedTaskArgs> _timedTask;
        private readonly ILogger<Helm> _logger;
        private readonly IDownloadWithRetry _downloader;
        private readonly IAvMapFactory _downloadFactory;
        private readonly IAvRepositoryFactory _repositoryFactory;

        public Helm(IAvTask<AvConfigFileUri, AvConfigFileObj, TimedTaskArgs> timedTask, 
            ILogger<Helm> logger, IDownloadWithRetry downloader,
            IAvMapFactory downloadFactory,
            IAvRepositoryFactory repositoryFactory)
        {
            _timedTask = timedTask;
            _logger = logger;
            _downloader = downloader;
            _downloadFactory = downloadFactory;
            _repositoryFactory = repositoryFactory;
        }

        public void SetConfigFileLocation(string fileLocation)
        {
            _timedTask.ConfigFileLocation = fileLocation;
        }

        public void BeginScheduledTasks(string configFileLocation)
        {
            _timedTask.ConfigFileLocation = configFileLocation;
            _timedTask.ReadConfigFile();
            _timedTask.BeginExecute += OnBegin;
            _timedTask.Info += OnInfo;
            _timedTask.EndExecute += OnEnd;

            _timedTask.Execute(Execute, _timedTask.ConfigFile);
        }

        public void EndScheduledTasks()
        {

        }

        private void Execute(AvConfigFileUri configSetup)
        {
            try
            {

                // determine the proper map resource.
                var mapper = CoreHelper.ConvertToMapResource(configSetup.Uri, _downloadFactory);

                // determine the proper repository resource.
                var repository = DataAccessHelper.ConvertToRepositoryResource(configSetup.Uri, _repositoryFactory);

                // download the remote resource, which should be in JSON format.
                var jObj = _downloader.DownloadWithRetries(configSetup.Uri,
                    _timedTask.ConfigFile.MaxDownloadRetries,
                    _timedTask.ConfigFile.RetryDelayInMilliSeconds);

                // map the downloaded resource.
                var data = MapInvocation((dynamic)mapper, jObj, configSetup.Uri);

                // process the downloaded resource by saving it to the database.
                SaveInvocation((dynamic)repository, (dynamic)data);
                
            }
            catch (AvDownloadRetryLimitReachedException ex)
            {
                _logger.LogError(Process.GetCurrentProcess().Id, $"ERROR {configSetup.Id}: {configSetup.Uri} >> {ex.Message}");
            }
            catch (Exception e)
            {
                _logger.LogCritical(Process.GetCurrentProcess().Id, $"ERROR {configSetup.Id}: {configSetup.Uri} >> {e.Message}");
            }
        }

        
        private void OnBegin(object sender, TimedTaskArgs args)
        {
            //_logger.LogInformation(Process.GetCurrentProcess().Id, $"BEGIN Process {args.Id}");
        }

        private void OnEnd(object sender, TimedTaskArgs args)
        {
            //_logger.LogInformation(Process.GetCurrentProcess().Id, $"END Process {args.Id}");
        }

        private void OnInfo(object sender, TimedTaskArgs args)
        {
            //_logger.LogInformation(Process.GetCurrentProcess().Id, $"INFO Process {args.Id}: {args.UriId}, {args.Uri}, {args.Country}, {args.Message}");
        }

        private void OnBegin(object sender, RepositoryArgs args)
        {
            _logger.LogInformation(Process.GetCurrentProcess().Id, $"{args.Guid} : {args.Message} @ {args.DateTime}");
        }
        private void OnEnd(object sender, RepositoryArgs args)
        {
            _logger.LogInformation(Process.GetCurrentProcess().Id, $"{args.Guid} : {args.Message} @ {args.DateTime}");
        }
        private void OnInfo(object sender, RepositoryArgs args)
        {
            _logger.LogInformation(Process.GetCurrentProcess().Id, $"{args.Guid} : {args.Message} @ {args.DateTime}");
        }



        public T MapInvocation<T>(IMapResource<T> downloader, JObject jobj, string uri) where T : class, new()
        {
            return MapInvocation((dynamic)downloader, jobj, uri);
        }

        public void SaveInvocation<T>(IRepository<T> repository, T data) where T : class, new()
        {
            SaveInvocation((dynamic) repository, (dynamic)data);
        }
    }
}
