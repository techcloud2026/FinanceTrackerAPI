using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.API.DTOs
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public Guid CategoryId { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}
