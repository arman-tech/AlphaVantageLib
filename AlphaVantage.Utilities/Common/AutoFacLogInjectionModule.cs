using Autofac;
using Autofac.Core;
using NLog;
using System.Linq;

namespace AlphaVantage.Utilities.Common
{
    public class AutoFacLogInjectionModule : Module
    {
        protected override void AttachToComponentRegistration(IComponentRegistry registry, IComponentRegistration registration)
        {
            registration.Preparing += OnComponentPreparing;
        }

        static void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            e.Parameters = e.Parameters.Union(new[]
            {
                new ResolvedParameter((p, i) => p.ParameterType == typeof(ILogger), (p, i) => LogManager.GetCurrentClassLogger())
            });

        }

    }
}
