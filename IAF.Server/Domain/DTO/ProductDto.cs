namespace IAF.Server.Domain.DTO
{
    public class ProductDto
    {
        public List<ProductDetails> ProductDetails { get; set; }
        public ResponseDefaultDto ResponseDefaultDto { get; set; }
    }

    public class ProductDetails
    {
        public string ProductId { get; set; }
        public string ProductDesc { get; set; }
        public int ForecastedProducedCount { get; set; }
        public string? ImageBase64 { get; set; }
    }
}
