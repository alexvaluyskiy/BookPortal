using Newtonsoft.Json.Serialization;

namespace BookPortal.Core.Framework
{
    public class LowerCasePropertyNamesContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}
