using IAF.Server.Domain.DTO;
using IAF.Server.Domain.Enums;
using IAF.Server.Domain.Models;
using IAF.Server.Interfaces;
using IAF.Server.Repositories;
using Microsoft.Data.SqlClient;

namespace IAF.Server.Services
{
    public class PartsService : IPartsService
    {
        private readonly IPartsRepository _repository;
        private readonly IProductRepository _productRepository;

        public PartsService(
            IPartsRepository repository, 
            IProductRepository productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }

        public async Task<PartsDto> GetAllPartsAsync()
        {
            var response = new PartsDto
            {
                PartsDetails = new List<PartsDetails>(),
                ResponseDefaultDto = new ResponseDefaultDto()
            };

            try
            {
                var parts = await _repository.GetAllPartsAsync();

                response.PartsDetails = parts.Select(p => new PartsDetails
                {
                    PartsId = p.Parts_ID,
                    PartsDesc = p.Parts_Desc,
                    Quantity = p.Quantity,
                    ImageBase64 = p.Image != null ? Convert.ToBase64String(p.Image) : null,
                    ProductId = p.Product_ID
                }).ToList();

                response.ResponseDefaultDto.Message = "Parts retrieved successfully.";
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

        public async Task<ResponseDefaultDto> AddPartAsync(PartsDetails part)
        {
            var response = new ResponseDefaultDto();

            try
            {
                var latestPart = await _repository.GetLatestPartByProductIdAsync(part.ProductId);

                int nextSuffix = 1;
                if (latestPart != null)
                {
                    var split = latestPart.Parts_ID.Split('-');
                    if (split.Length > 1 && int.TryParse(split[1], out var num))
                    {
                        nextSuffix = num + 1;
                    }
                }

                string newPartsId = $"{part.ProductId}-{nextSuffix:D2}";
                part.PartsId = newPartsId;

                await _repository.AddPartAsync(new Parts
                {
                    Parts_ID = part.PartsId,
                    Parts_Desc = part.PartsDesc,
                    Quantity = part.Quantity,
                    Image = !string.IsNullOrEmpty(part.ImageBase64) ? Convert.FromBase64String(part.ImageBase64) : null,
                    Product_ID = part.ProductId
                });

                int forecast = await CalculateForecastedProducedCountAsync(part.ProductId);
                await _productRepository.UpdateForecastedProducedCountAsync(part.ProductId, forecast);

                response.Message = "Part added successfully.";
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

        public async Task<ResponseDefaultDto> UpdatePartAsync(PartsDetails part)
        {
            var response = new ResponseDefaultDto();

            try
            {
                await _repository.UpdatePartAsync(new Parts
                {
                    Parts_ID = part.PartsId,
                    Image = !string.IsNullOrEmpty(part.ImageBase64) ? Convert.FromBase64String(part.ImageBase64) : null,
                    Quantity = part.Quantity
                 });

                int forecast = await CalculateForecastedProducedCountAsync(part.ProductId);
                await _productRepository.UpdateForecastedProducedCountAsync(part.ProductId, forecast);

                response.Message = "Part updated successfully.";
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

        public async Task<ResponseDefaultDto> DeletePartAsync(string partsId)
        {
            var response = new ResponseDefaultDto();

            try
            {
                var result = await _repository.DeletePartAsync(partsId);

                int forecast = await CalculateForecastedProducedCountAsync(result);
                await _productRepository.UpdateForecastedProducedCountAsync(result, forecast);

                response.Message = "Part deleted successfully.";
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

        private async Task<int> CalculateForecastedProducedCountAsync(string productId)
        {
            try 
            {
                var parts = await _repository.GetPartsByProductIdAsync(productId);

                if (!parts.Any())
                    return 0;

                string[] requiredParts = productId switch
                {
                    "IAF01" => Enum.GetNames(typeof(CarParts)),
                    "IAF02" => Enum.GetNames(typeof(CellPhoneParts)),
                    "IAF03" => Enum.GetNames(typeof(DroneParts)),
                    _ => Array.Empty<string>()
                };

                foreach (var required in requiredParts)
                {
                    if (!parts.Any(p => p.Parts_Desc == required))
                        return 0;
                }

                int forecast = parts
                    .Where(p => requiredParts.Contains(p.Parts_Desc))
                    .Min(p => p.Quantity);

                return forecast;
            } 
            catch (SqlException)
            {
                throw;
            }
        }
    }
}
