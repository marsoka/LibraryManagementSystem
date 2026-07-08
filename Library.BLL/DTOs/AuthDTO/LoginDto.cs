namespace Library.BLL.DTOs.AuthDTO
{
    public class LoginDto
    {
        public required string Username { get; set; }

        public required string Password { get; set; }
    }
}