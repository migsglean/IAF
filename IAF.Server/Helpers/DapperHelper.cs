using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace IAF.Server.Helpers
{
    public class DapperHelper
    {
        private readonly string _connectionString;

        public DapperHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        // Query single record
        public async Task<T> QuerySingleAsync<T>(string storedProc, object parameters = null)
        {
            using (var conn = GetConnection())
            {
                return await conn.QueryFirstOrDefaultAsync<T>(
                    storedProc,
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        // Query multiple records
        public async Task<IEnumerable<T>> QueryAsync<T>(string storedProc, object parameters = null)
        {
            using (var conn = GetConnection())
            {
                return await conn.QueryAsync<T>(
                    storedProc,
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        // Execute Insert/Update/Delete with return (affected rows)
        public async Task<int> ExecuteAsync(string storedProc, object parameters = null)
        {
            using (var conn = GetConnection())
            {
                return await conn.ExecuteAsync(
                    storedProc,
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        //// Execute Insert/Update/Delete without return
        public async Task ExecuteVoidAsync(string storedProc, object parameters = null)
        {
            using (var conn = GetConnection())
            {
                await conn.ExecuteAsync(
                    storedProc,
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }
    }
}