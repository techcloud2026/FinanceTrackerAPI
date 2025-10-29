using System;

namespace FinanceTracker.API.DTOs
{
    public class GoalDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal TargetAmount { get; set; }
        public decimal SavedAmount { get; set; }
        public DateTime Deadline { get; set; }
    }
}
