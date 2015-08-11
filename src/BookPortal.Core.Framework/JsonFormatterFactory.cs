using Microsoft.AspNet.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace BookPortal.Core.Framework
{
    public class JsonFormatterFactory
    {
        public static JsonOutputFormatter Create()
        {
            var jsonOutputFormatter = new JsonOutputFormatter();
            jsonOutputFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonOutputFormatter.SerializerSettings.ContractResolver = new LowerCasePropertyNamesContractResolver();
            jsonOutputFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            jsonOutputFormatter.SupportedMediaTypes.Clear();
            jsonOutputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

            return jsonOutputFormatter;
        }
    }
}
