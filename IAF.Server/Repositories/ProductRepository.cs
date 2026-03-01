using Dapper;
using IAF.Server.Domain.DTO;
using IAF.Server.Domain.Models;
using IAF.Server.Helpers;
using IAF.Server.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace IAF.Server.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperHelper _dapper;
        public ProductRepository(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<Products>> GetAllProductsAsync()
        {
            try
            {
                var result = await _dapper.QueryAsync<Products>(
                    "spGetAllProducts"
                );

                return result;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task UpdateForecastedProducedCountAsync(string productId, int forecastedCount)
        {
            try
            {
                await _dapper.ExecuteAsync(
                    "spUpdateForecastedProducedCount",
                    new { Product_ID = productId, ForecastedCount = forecastedCount }
                );
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task ProduceProductAsync(ProduceDto request)
        {
            try
            {
                // Loop through each part but keep the transaction atomic
                foreach (var partId in request.PartsId)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Product_ID", request.ProductId);
                    parameters.Add("@Parts_ID", partId);
                    parameters.Add("@Quantity", request.Quantity);
                    parameters.Add("@UserName", request.userName);
                    parameters.Add("@TransactionNo", this.GenerateTransactionNo());
                    parameters.Add("@OutputMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    await _dapper.ExecuteAsync(
                        "spProduceProduct",
                        parameters
                    );

                    var outputMessage = parameters.Get<string>("@OutputMessage");
                    if (!string.IsNullOrEmpty(outputMessage))
                    {
                        throw new Exception($"Error producing product {request.ProductId} with part {partId}: {outputMessage}");
                    }

                    await Task.Delay(300);
                }

                var updateParams = new DynamicParameters();
                updateParams.Add("@Product_ID", request.ProductId);
                updateParams.Add("@Quantity", request.Quantity);
                updateParams.Add("@OutputMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                await _dapper.ExecuteAsync(
                    "spUpdateProductForecast",
                    updateParams
                );

                var forecastMessage = updateParams.Get<string>("@OutputMessage");
                if (!string.IsNullOrEmpty(forecastMessage))
                {
                    throw new Exception($"Error updating forecast for product {request.ProductId}: {forecastMessage}");
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public string GenerateTransactionNo()
        {
            var now = DateTime.Now;

            string transactionNo = $"IAF-{now.ToString("MMddyyyy-mmHHssff")}";

            return transactionNo;
        }
    }
}
