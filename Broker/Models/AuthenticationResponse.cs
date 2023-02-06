namespace BrokerApi.Models
{
    public class AuthenticationResponse
    {
        public string? Access_Token { get; set; }
        public string? Token_Type { get; set; }
        public DateTime? Expires_In { get; set; }
        public string? Refresh_Token { get; set; } 
    }
}
