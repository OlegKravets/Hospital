using Dapper;
using HospitalApi.Connection;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HospitalApi.Repositories
{
    public abstract class RepositoryBase<T>
        where T : class
    {
        public RepositoryBase(HospitalConnection connection)
        {
            Connection = connection;
        }

        protected HospitalConnection Connection { get; }

        protected virtual string AnyScript { get; }

        public async Task<bool> Any()
        {
            if (string.IsNullOrEmpty(AnyScript))
            {
                return false;
            }

            return await ExecuteScalarQuery(AnyScript);
        }

        protected async Task<bool> IsExist(string script, object param = null)
        {
            return await ExecuteScalarQuery(script, param);
        }

        protected async Task<bool> ExecuteScalarQuery(string script, object param = null)
        {
            using (IDbConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                return await connection.ExecuteScalarAsync<bool>(script, new DynamicParameters(param));
            }
        }

        protected async Task<IEnumerable<T>> GetData(string query, object param = null)
        {
            using (IDbConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                return await connection.QueryAsync<T>(query, param);
            }
        }

        protected async Task<T> GetFirstOrDefault(string query, object param = null)
        {
            using (IDbConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<T>(query, new DynamicParameters(param));
            }
        }

        protected async Task<int> ExecuteQuery(string query, object param = null)
        {
            using (IDbConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                return await connection.ExecuteAsync(query, new DynamicParameters(param));
            }
        }
    }
}
