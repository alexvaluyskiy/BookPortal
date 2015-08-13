using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BookPortal.Web.Domain
{
    public class ConnectionFactory : IConnectionFactory, IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        private void Create()
        {
            _connection = new SqlConnection();
            _connection.ConnectionString = _connectionString;
            _connection.Open();
        }

        public IDbConnection DbConnection
        {
            get
            {
                if (_connection == null || _connection.State != ConnectionState.Open)
                    Create();

                return _connection;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connection.Close();
            }
        }

        ~ConnectionFactory()
        {
            Dispose(false);
        }
    }
}
