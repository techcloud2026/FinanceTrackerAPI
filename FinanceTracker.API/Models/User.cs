using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.API.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [MaxLength(10)]
        public string Currency { get; set; } = "INR";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
