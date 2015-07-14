using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;

namespace BookPortal.Core.Framework
{
    public class JsonFormatterFactory
    {
        public static JsonOutputFormatter Create()
        {
            var jsonOutputFormatter = new JsonOutputFormatter();
            jsonOutputFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonOutputFormatter.SerializerSettings.Formatting = Formatting.Indented;
            jsonOutputFormatter.SerializerSettings.ContractResolver = new LowerCaseUnderscorePropertyNamesContractResolver();
            jsonOutputFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            return jsonOutputFormatter;
        }
    }
}
