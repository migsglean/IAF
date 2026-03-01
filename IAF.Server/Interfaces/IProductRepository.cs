using IAF.Server.Domain.DTO;
using IAF.Server.Domain.Models;

namespace IAF.Server.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetAllProductsAsync();

        Task UpdateForecastedProducedCountAsync(string productId, int forecastedCount);

        Task ProduceProductAsync(ProduceDto request);
    }
}
