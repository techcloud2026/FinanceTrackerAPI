using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.API.DTOs
{
    public class UpdateTransactionDto
    {
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Type { get; set; } // Income or Expense
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
