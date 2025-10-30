using FinanceTracker.API.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceTracker.API.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync(Guid userId);
        Task<TransactionDto> GetTransactionByIdAsync(Guid userId, Guid transactionId);
        Task<TransactionDto> AddTransactionAsync(Guid userId, CreateTransactionDto createTransactionDto);
        Task UpdateTransactionAsync(Guid userId, Guid transactionId, UpdateTransactionDto updateTransactionDto);
        Task DeleteTransactionAsync(Guid userId, Guid transactionId);
        Task<IEnumerable<TransactionDto>> FilterTransactionsAsync(Guid userId, DateTime? startDate, DateTime? endDate, int? categoryId, string type);
        Task<Dictionary<string, decimal>> GetMonthlySummaryAsync(Guid userId);
        Task<Dictionary<string, decimal>> GetYearlySummaryAsync(Guid userId);
    }
}
