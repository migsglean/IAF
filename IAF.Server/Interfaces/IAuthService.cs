using IAF.Server.Domain.DTO;

namespace IAF.Server.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDefaultDto> SignUpAsync(SignUpDto request);

        Task<LoginResponseDto> LoginAsync(LoginDto request);
    }
}
