namespace IAF.Server.Domain.DTO
{
    public class ProduceDto
    {
        public string userName { get; set; }
        public string ProductId { get; set; }
        public List<string> PartsId { get; set; }
        public int Quantity { get; set; }
    }
}
