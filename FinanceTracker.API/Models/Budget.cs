using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.API.Models
{
    public class Budget
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        public Guid CategoryId { get; set; }

        public decimal LimitAmount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
