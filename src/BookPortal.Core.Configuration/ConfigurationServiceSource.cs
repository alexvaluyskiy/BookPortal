using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Framework.ConfigurationModel;
using Newtonsoft.Json;

namespace BookPortal.Core.Configuration
{
    public class ConfigurationServiceSource : ConfigurationSource
    {
        private readonly Uri _configServiceUri;
        private readonly HttpClient _client;

        public ConfigurationServiceSource(Uri configServiceUri)
        {
            _configServiceUri = configServiceUri;
            _client = new HttpClient();
        }

        public override void Load()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = new Uri(_configServiceUri, "api/configurations");
            requestMessage.Method = HttpMethod.Get;

            try
            {
                var response = _client.SendAsync(requestMessage).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    Data = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                }
            }
            catch
            {

            }
        }
    }
}
