using System;
using System.Data;
using System.Data.SqlClient;

namespace BookPortal.Web.Domain
{
    public class ConnectionFactory : IConnectionFactory, IDisposable
    {
        private readonly string _connectionString;

        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection _connection;

        private IDbConnection Create()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                _connection = new SqlConnection();
                _connection.ConnectionString = _connectionString;
                _connection.Open();
            }

            return _connection;
        }

        public IDbConnection GetDbConnection => Create();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connection?.Close();
            }
        }

        ~ConnectionFactory()
        {
            Dispose(false);
        }
    }
}
