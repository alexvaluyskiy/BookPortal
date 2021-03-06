﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Framework.Configuration;
using Newtonsoft.Json;

namespace BookPortal.Core.Configuration
{
    public class ConfigurationServiceSource : ConfigurationSource
    {
        private readonly Uri _configServiceUri;
        private readonly string _profile;
        private readonly HttpClient _client;

        public ConfigurationServiceSource(Uri configServiceUri, string profile)
        {
            _configServiceUri = configServiceUri;
            _profile = profile;
            _client = new HttpClient();
        }

        public override void Load()
        {
            LoadAsync().Wait();
        }

        public async Task LoadAsync()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = new Uri(_configServiceUri, "api/config/" + _profile);
            requestMessage.Method = HttpMethod.Get;

            var response = await _client.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(content);

                Data = result.ToDictionary(x => x.Key, x => x.Value);
            }
        }
    }
}
