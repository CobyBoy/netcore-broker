using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrokerApi.Models
{
    public class User: IdentityUser
    {
        [Column(Order = 7)]
        public DateTime RegisteredAt { get; set; }
        [Column(Order = 8)]
        public DateTime? ConfirmedRegistrationAt { get; set; }
        [Column(Order = 9)]
        public bool IsActive { get; set; } = true;
        [Column(Order = 10)]
        public string AreaCode { get; set; } = string.Empty;
        [Column(Order = 13)]
        public DateTime? ResetTokenExpires { get; set; }
        [Column(Order = 14)]
        public string? VerificationToken { get; set; }
        [Column(Order = 15)]
        public string? PasswordResetToken { get; set; }
    }
}
