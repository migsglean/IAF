using IAF.Server.Domain.DTO;
using IAF.Server.Domain.Models;

namespace IAF.Server.Interfaces
{
    public interface IAuthRepository
    {
        Task InsertUserAsync(SignUpDto data);

        Task<Credentials> LoginExecutionAsync(LoginDto data);
    }
}
