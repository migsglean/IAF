namespace IAF.Server.Domain.DTO
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; } // usually you'd hash before sending
    }
}
