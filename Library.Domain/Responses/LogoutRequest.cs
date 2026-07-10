namespace Library.Domain.Responses
{
    public class LogoutRequest
    {
        public required string RefreshToken { get; set; }
    }
}