using IAF.Server.Domain.DTO;

namespace IAF.Server.Interfaces
{
    public interface IPartsService
    {
        Task<PartsDto> GetAllPartsAsync();

        Task<ResponseDefaultDto> AddPartAsync(PartsDetails part);

        Task<ResponseDefaultDto> UpdatePartAsync(PartsDetails part);

        Task<ResponseDefaultDto> DeletePartAsync(string partsId);
    }
}
