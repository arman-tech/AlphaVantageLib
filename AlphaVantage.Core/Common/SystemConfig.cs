using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Core.Common
{
    public class SystemConfig
    {
        readonly IConfigurationBuilder _appSettings;

        public SystemConfig(string appSettingFileName)
        {
            _appSettings = new ConfigurationBuilder()
                    .AddJsonFile(appSettingFileName, optional: true, reloadOnChange: true);

        }

        public IConfigurationRoot AppSetttings => _appSettings.Build();
    }
}
