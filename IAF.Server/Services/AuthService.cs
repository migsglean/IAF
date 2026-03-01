using Azure.Core;
using IAF.Server.Domain.DTO;
using IAF.Server.Exceptions;
using IAF.Server.Helpers;
using IAF.Server.Interfaces;
using Microsoft.Data.SqlClient;

namespace IAF.Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;

        public AuthService(IAuthRepository repository)
        {
            _repository = repository;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto request)
        {
            var response = new LoginResponseDto();
            try
            {
                if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
                {
                    response.Message = "Username and password are required.";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }

                string hashedPassword = PasswordHelper.HashPassword(request.Password);

                request.Password = hashedPassword;

                var result = await _repository.LoginExecutionAsync(request);

                response.UserName = result.UserName;
                response.Message = "Login successfully.";
                response.StatusCode = StatusCodes.Status200OK;
                return response;
            } 
            catch (InvalidLoginException ex)
            {
                response.Message = ex.Message;
                response.StatusCode = StatusCodes.Status401Unauthorized;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Internal server error: " + ex.Message;
                response.StatusCode = StatusCodes.Status500InternalServerError;
                return response;
            }
        }

        public async Task<ResponseDefaultDto> SignUpAsync(SignUpDto request)
        {
            var response = new ResponseDefaultDto();
            try
            {
                if (request.Password != request.ConfirmPassword)
                {
                    response.Message = "Password and Confirm Password do not match.";
                    response.StatusCode = StatusCodes.Status400BadRequest; 
                    return response;
                }

                string hashedPassword = PasswordHelper.HashPassword(request.Password);

                request.Password = hashedPassword;

                await _repository.InsertUserAsync(request);

                response.Message = "User registered successfully.";
                response.StatusCode = StatusCodes.Status200OK;

                return response;
            }
            catch (DuplicateUserException ex)
            {
                response.Message = ex.Message;
                response.StatusCode = StatusCodes.Status409Conflict; 
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Internal server error: " + ex.Message;
                response.StatusCode = StatusCodes.Status500InternalServerError;
                return response;
            }
        }
    }
}
