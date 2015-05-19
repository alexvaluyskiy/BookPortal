using System;
using Microsoft.Framework.ConfigurationModel;

namespace BookPortal.Core.Configuration
{
    public static class ConfigurationServiceExtension
    {
        public static IConfigurationSourceRoot AddConfigurationService(this IConfigurationSourceRoot configuration, string configServiceUri, string profile)
        {
            if (configServiceUri == null)
            {
                throw new ArgumentNullException(nameof(configServiceUri));
            }
            if (profile == null)
            {
                throw new ArgumentNullException(nameof(profile));
            }

            configuration.Add(new ConfigurationServiceSource(new Uri(configServiceUri), profile));

            return configuration;
        }
    }
}

