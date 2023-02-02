using System.ComponentModel.DataAnnotations;

namespace BrokerApi.Models
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(64)]
        public string Username { get; set; } = string.Empty;

        [MaxLength(254)]
        public string EmailAddress { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        public string AreaCode { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool isUserVerified { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime? ConfirmedRegistrationAt { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public string? VerificationToken { get; set; }
        public string? PasswordResetToken { get; set; }
    }
}
