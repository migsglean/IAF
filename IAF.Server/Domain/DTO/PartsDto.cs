namespace IAF.Server.Domain.DTO
{
    public class PartsDto
    {
        public List<PartsDetails> PartsDetails { get; set; }
        public ResponseDefaultDto ResponseDefaultDto { get; set; }
    }

    public class PartsDetails
    {
        public string? PartsId { get; set; }
        public string PartsDesc { get; set; }
        public int Quantity { get; set; }
        public string? ImageBase64 { get; set; }
        public string ProductId { get; set; }
    }
}
