using AlphaVantage.Core.Common;
using AlphaVantage.TimedTask.Runner.Common;
using AlphaVantage.Utilities.Common;
using Autofac;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace AlphaVantage.TimedTask.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new AutoFacBootStrapperEx()
                .MongoDbSetup()
                .MapResourceSetup()
                .RepositorySetup()
                .AlphaVantageCoreSetup()
                .LoggerSetup()
                .InfrastructureSetup()
                .Build();

            var loggerFactory = container.Resolve<ILoggerFactory>();
            var config = new SystemConfig(SystemConfigRes.AppSettingsFileName).AppSetttings;

            NLog.LogManager.LoadConfiguration(config[SystemConfigRes.NlogTaskFileName]);

            var logger = NLog.LogManager.GetCurrentClassLogger();
            loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });



            logger.Log(NLog.LogLevel.Info, "===== log service started =====");
            var helm = container.Resolve<Helm>();

            helm.BeginScheduledTasks(config[SystemConfigRes.ScheduledTaskFileName]);
            helm.EndScheduledTasks();

            logger.Log(NLog.LogLevel.Info, "===== log service ended =====");

            System.Console.Write("Press any key to continue...");
            System.Console.ReadKey();
        }
    }
}
