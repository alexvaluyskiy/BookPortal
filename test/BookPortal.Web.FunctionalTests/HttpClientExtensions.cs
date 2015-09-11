using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using Newtonsoft.Json;

namespace BookPortal.Web.FunctionalTests
{
    public static class HttpClientExtensions
    {
        public static async Task<PageResult<List<T>>> ReadAsJsonPageAsync<T>(this HttpContent content)
        {
            var stringContent = await content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(stringContent))
                return null;
            
            var result = JsonConvert.DeserializeObject<SuccessResult<PageResult<List<T>>>>(stringContent);
            return result.Result;
        }
    }
}
