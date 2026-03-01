using IAF.Server.Domain.DTO;
using IAF.Server.Domain.Models;

namespace IAF.Server.Interfaces
{
    public interface IPartsRepository
    {
        Task<IEnumerable<Parts>> GetAllPartsAsync();

        Task AddPartAsync(Parts part);

        Task UpdatePartAsync(Parts part);

        Task<string> DeletePartAsync(string partsId);

        Task<Parts> GetLatestPartByProductIdAsync(string productId);

        Task<IEnumerable<Parts>> GetPartsByProductIdAsync(string productId);
    }
}
