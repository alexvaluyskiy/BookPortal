using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace BookPortal.Web.Domain
{
    public static class DapperExtensions
    {
        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection connection, Func<T> typeBuilder, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }
    }
}
