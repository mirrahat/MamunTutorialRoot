namespace MamunTutorial.Models
{
    public class AuthenticationResponse
    {
        public string? Role { get; set; }=string.Empty;

        public string? Email { get; set; }

        public string? Token { get; set; }

        public DateTime Expiration { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpirationDateTime { get; set; }

    }
}
