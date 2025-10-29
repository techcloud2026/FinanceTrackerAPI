using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.API.Models
{
    public class Goal
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public decimal TargetAmount { get; set; }

        public decimal SavedAmount { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
