﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Core.ApiPrimitives.Models;

namespace Microsoft.AspNet.Mvc
{
    public class PageObjectResult : ObjectResult
    {
        public PageObjectResult(int statusCode, object value) : this(statusCode, value, 0, 0, 0)
        {
        }

        public PageObjectResult(int statusCode, object value, int totalrows, int limit, int offset) : base(value)
        {
            TotalRows = totalrows;
            Limit = limit;
            Offset = offset;
            StatusCode = statusCode;
        }

        public int TotalRows { get; }

        public int Limit { get; }

        public int Offset { get; }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            var pageResult = new PageResult<object>();
            pageResult.TotalRows = TotalRows;
            pageResult.Limit = Limit;
            pageResult.Offset = Offset;

            var collection = Value as IEnumerable<object>;

            if (collection != null)
            {
                foreach (var item in collection)
                {
                    var row = new PageRow<object> {RowNum = Offset + pageResult.Rows.Count, Values = item};
                    pageResult.Rows.Add(row);
                }

                var successResult = new SuccessResult<PageResult<object>> { Result = pageResult };
                Value = successResult;
            }

            return base.ExecuteResultAsync(context);
        }
    }
}
