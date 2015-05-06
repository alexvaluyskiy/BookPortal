using System.Runtime.Serialization;

namespace BookPortal.Web.Models.Api
{
    [DataContract]
    public class ApiRoot
    {
        public ApiRoot()
        {
            Result = new ApiResult();
        }

        [DataMember(Name = "result")]
        public ApiResult Result { get; set; }
    }
}
