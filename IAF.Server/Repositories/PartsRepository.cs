using IAF.Server.Domain.DTO;
using IAF.Server.Domain.Models;
using IAF.Server.Helpers;
using IAF.Server.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace IAF.Server.Repositories
{
    public class PartsRepository : IPartsRepository
    {
        private readonly DapperHelper _dapper;

        public PartsRepository(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<Parts>> GetAllPartsAsync()
        {
            try
            {
                var result = await _dapper.QueryAsync<Parts>("spGetAllParts");
                return result;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error while retrieving parts: {ex.Message}", ex);
            }
        }

        public async Task AddPartAsync(Parts part)
        {
            try
            {
                await _dapper.ExecuteAsync(
                    "spAddPart",
                    new
                    {
                        part.Parts_ID,
                        part.Parts_Desc,
                        part.Quantity,
                        part.Image,
                        part.Product_ID
                    }
                );
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("already exists"))
                {
                    throw;
                }
                else
                {
                    throw new Exception($"Database error while adding part {part.Parts_ID}: {ex.Message}", ex);

                }
            }
        }

        public async Task UpdatePartAsync(Parts part)
        {
            try
            {
                await _dapper.ExecuteAsync(
                    "spUpdatePart",
                    new
                    {
                        Parts_ID = part.Parts_ID,
                        Image = part.Image,
                        part.Quantity
                    }
                );
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error while updating part {part.Parts_ID}: {ex.Message}", ex);
            }
        }

        public async Task<string> DeletePartAsync(string partsId)
        {
            try
            {
                var productId = await _dapper.QuerySingleAsync<string>(
                    "spDeletePart",
                    new { Parts_ID = partsId }
                );

                return productId ?? string.Empty;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error while deleting part {partsId}: {ex.Message}", ex);
            }
        }


        public async Task<Parts> GetLatestPartByProductIdAsync(string productId)
        {
            try
            {
                var result = await _dapper.QuerySingleAsync<Parts>(
                    "spGetLatestPartByProductId",
                    new { Product_ID = productId }
                );
                return result;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error while retrieving latest part for product {productId}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Parts>> GetPartsByProductIdAsync(string productId)
        {
            try
            {
                return await _dapper.QueryAsync<Parts>(
                "spGetPartsByProductId",
                new { Product_ID = productId }
            );
            } 
            catch (SqlException)
            {
                throw;
            }
        }
    }
}
