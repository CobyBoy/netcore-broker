using System.ComponentModel.DataAnnotations;

namespace BrokerApi.DTOs
{
    public class LoggedUserDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
