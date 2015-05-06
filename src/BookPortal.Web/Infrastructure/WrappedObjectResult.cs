using System.Collections;
using System.Threading.Tasks;
using BookPortal.Web.Models.Api;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Infrastructure
{
    public class WrappedObjectResult : ObjectResult
    {
        public WrappedObjectResult(object value) : base(value)
        {
        }

        // TODO: get limit and offset parameters and default values for them
        public override Task ExecuteResultAsync(ActionContext context)
        {
            var listValue = Value as ICollection;

            if (listValue != null)
            {
                ApiRoot apiRoot = new ApiRoot();

                int rowNumber = 0;
                foreach (var rowValue in listValue)
                {
                    var apiRow = new ApiRow();
                    apiRow.RowNum = ++rowNumber;
                    apiRow.Values = rowValue;
                    apiRoot.Result.Rows.Add(apiRow);
                }

                Value = apiRoot;
            }
            else
            {
                ApiSingleResult apiRoot = new ApiSingleResult {Result = Value};

                Value = apiRoot;
            }

            return base.ExecuteResultAsync(context);
        }
    }
}
