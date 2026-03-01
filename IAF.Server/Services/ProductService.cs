using IAF.Server.Domain.DTO;
using IAF.Server.Interfaces;
using Microsoft.Data.SqlClient;

namespace IAF.Server.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDto> GetAllProductsAsync()
        {
            var response = new ProductDto
            {
                ProductDetails = new List<ProductDetails>(),
                ResponseDefaultDto = new ResponseDefaultDto()
            };

            try
            {
                var products = await _repository.GetAllProductsAsync();

                response.ProductDetails = products.Select(p => new ProductDetails
                {
                    ProductId = p.Product_ID,
                    ProductDesc = p.Product_Desc,
                    ForecastedProducedCount = p.Forecasted_Produced_Count,
                    ImageBase64 = p.Image != null ? Convert.ToBase64String(p.Image) : null
                }).ToList();

                response.ResponseDefaultDto.Message = "Products retrieved successfully.";
                response.ResponseDefaultDto.StatusCode = StatusCodes.Status200OK;
            }
            catch (SqlException ex)
            {
                response.ResponseDefaultDto.Message = ex.Message;
                response.ResponseDefaultDto.StatusCode = StatusCodes.Status500InternalServerError;
            }
            catch (Exception ex)
            {
                response.ResponseDefaultDto.Message = $"Internal server error: {ex.Message}";
                response.ResponseDefaultDto.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return response;
        }

        public async Task<ResponseDefaultDto> ProduceProductAsync(ProduceDto request)
        {
            var response = new ResponseDefaultDto();
            try
            {
                await _repository.ProduceProductAsync(request);
                response.Message = "Product produced successfully.";
                response.StatusCode = StatusCodes.Status200OK;
            }
            catch (SqlException ex)
            {
                response.Message = ex.Message;
                response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            catch (Exception ex)
            {
                response.Message = $"Internal server error: {ex.Message}";
                response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            return response;
        }
    }
}
