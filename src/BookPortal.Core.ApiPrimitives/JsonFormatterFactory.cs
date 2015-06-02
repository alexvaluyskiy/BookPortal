using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;

namespace BookPortal.Core.ApiPrimitives
{
    public class JsonFormatterFactory
    {
        public static JsonOutputFormatter Create()
        {
            var jsonOutputFormatter = new JsonOutputFormatter();
            jsonOutputFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonOutputFormatter.SerializerSettings.Formatting = Formatting.Indented;
            jsonOutputFormatter.SerializerSettings.ContractResolver = new LowerCasePropertyNamesContractResolver();
            jsonOutputFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            return jsonOutputFormatter;
        }
    }
}
