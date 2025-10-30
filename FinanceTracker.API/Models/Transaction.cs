using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTracker.API.Models
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        [Required, MaxLength(20)]
        public string Type { get; set; } = string.Empty; // Income / Expense

        [Required]
        public decimal Amount { get; set; }

        public int CategoryId { get; set; }

        [MaxLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [MaxLength(500)]
        public string Note { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
