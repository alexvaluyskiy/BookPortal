﻿using System;
using Microsoft.Framework.ConfigurationModel;

namespace BookPortal.Core.Configuration
{
    public static class ConfigurationServiceExtension
    {
        public static IConfigurationSourceRoot AddConfigurationService(this IConfigurationSourceRoot configuration, string configServiceUri)
        {
            if (configServiceUri == null)
            {
                throw new ArgumentNullException(nameof(configServiceUri));
            }

            configuration.Add(new ConfigurationServiceSource(new Uri(configServiceUri)));

            return configuration;
        }
    }
}
