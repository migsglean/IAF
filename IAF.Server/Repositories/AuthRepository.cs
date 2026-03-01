using IAF.Server.Domain.DTO;
using IAF.Server.Domain.Models;
using IAF.Server.Exceptions;
using IAF.Server.Helpers;
using IAF.Server.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace IAF.Server.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DapperHelper _dapper;

        public AuthRepository(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        public async Task<Credentials> LoginExecutionAsync(LoginDto data)
        {
            try
            {
                var result = await _dapper.QuerySingleAsync<Credentials>(
                    "spLoginUser",
                    data);
                        
                return result;
            }
            catch (SqlException ex) when (ex.Number == 50001)
            {
                throw new InvalidLoginException(ex.Message, ex);
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error while searching a user: {ex.Message}", ex);
            }
        }

        public async Task InsertUserAsync(SignUpDto data)
        {
            var parameter = new Credentials
            {
                UserName = data.UserName,
                EmailAddress = data.EmailAddress,
                Password = data.Password
            };
            try
            {
                await _dapper.ExecuteVoidAsync(
                    "spInsertUser",
                    parameter
                );
            }
            catch (SqlException ex) when (ex.Number == 2627) 
            {
                throw new DuplicateUserException("User with this username or email already exists.", ex);
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error while inserting user: {ex.Message}", ex);
            }
        }
    }
}
