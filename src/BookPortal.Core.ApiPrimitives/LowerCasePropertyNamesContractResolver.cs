using Newtonsoft.Json.Serialization;

namespace BookPortal.Core.ApiPrimitives
{
    public class LowerCasePropertyNamesContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}
