using IAF.Server.Domain.DTO;

namespace IAF.Server.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetAllProductsAsync();

        Task<ResponseDefaultDto> ProduceProductAsync(ProduceDto request);
    }
}
