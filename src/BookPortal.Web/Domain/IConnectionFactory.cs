using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortal.Web.Domain
{
    public interface IConnectionFactory
    {
        IDbConnection DbConnection { get; }
    }
}
