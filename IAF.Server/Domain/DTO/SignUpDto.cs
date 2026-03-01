namespace IAF.Server.Domain.DTO
{
    public class SignUpDto
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; } 
        public string ConfirmPassword { get; set; } 
    }
}
