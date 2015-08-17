using System.Data;

namespace BookPortal.Web.Domain
{
    public interface IConnectionFactory
    {
        IDbConnection GetDbConnection { get; }
    }
}
