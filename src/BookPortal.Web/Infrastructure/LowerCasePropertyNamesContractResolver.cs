using Newtonsoft.Json.Serialization;

namespace BookPortal.Web.Infrastructure
{
    public class LowerCasePropertyNamesContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}
