using Autofac;
using AlphaVantage.Utilities.Common;
using AlphaVantage.Core.Interfaces;
using AlphaVantage.Common.Models;

namespace AlphaVantage.TimedTask.Runner.Common
{
    public class AutoFacBootStrapperEx : AutoFacBootStrapper
    {
        public override AutoFacBootStrapper InfrastructureSetup()
        {
            this.Builder.RegisterType<TimedTask>().As<IAvTask<AvConfigFileUri, AvConfigFileObj, TimedTaskArgs>>();
            this.Builder.RegisterType<Helm>().SingleInstance();

            return base.InfrastructureSetup();
        }
    }

}
