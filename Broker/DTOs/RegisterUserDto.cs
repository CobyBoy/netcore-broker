using System.ComponentModel.DataAnnotations;

namespace BrokerApi.DTOs
{
    public class RegisterUserDto
    {
        [Required, MinLength(4, ErrorMessage = "Username must be 4 characters at least")]
        [MaxLength(16, ErrorMessage = "Password must be 16 characters maximum")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z0-9]{3,10}$", ErrorMessage = "Username must contain at least a number")]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(4, ErrorMessage = "Password must be 4 characters at least")]
        [MaxLength(16, ErrorMessage = "Password must be 16 characters maximum")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{4,}$", ErrorMessage = "Password must be uppercase, lowercase, a special character and a number")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        [RegularExpression("[0-9]+", ErrorMessage = "Please provide only numbers for area code")]
        public string AreaCode { get; set; } = string.Empty;

        [Required]
        [RegularExpression("[0-9]+", ErrorMessage = "Please provide only numbers for phone number")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
