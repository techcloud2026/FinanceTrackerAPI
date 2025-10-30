using AutoMapper;
using FinanceTracker.API.Data;
using FinanceTracker.API.DTOs;
using FinanceTracker.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace FinanceTracker.API.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TransactionService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync(Guid userId)
        {
            var transactions = await _context.Transactions
                                             .Where(t => t.UserId == userId)
                                             .Include(t => t.Category)
                                             .ToListAsync();
            return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }

        public async Task<TransactionDto> GetTransactionByIdAsync(Guid userId, Guid transactionId)
        {
            var transaction = await _context.Transactions
                                            .Include(t => t.Category)
                                            .FirstOrDefaultAsync(t => t.Id == transactionId && t.UserId == userId);
            return _mapper.Map<TransactionDto>(transaction);
        }

        public async Task<TransactionDto> AddTransactionAsync(Guid userId, CreateTransactionDto createTransactionDto)
        {
            var transaction = _mapper.Map<Transaction>(createTransactionDto);
            transaction.UserId = userId;
            transaction.Date = DateTime.SpecifyKind(createTransactionDto.Date, DateTimeKind.Utc); // Ensure UTC
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return _mapper.Map<TransactionDto>(transaction);
        }

        public async Task UpdateTransactionAsync(Guid userId, Guid transactionId, UpdateTransactionDto updateTransactionDto)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId && t.UserId == userId);

            if (transaction == null)
            {
                throw new KeyNotFoundException("Transaction not found.");
            }

            _mapper.Map(updateTransactionDto, transaction);
            transaction.Date = DateTime.SpecifyKind(updateTransactionDto.Date, DateTimeKind.Utc); // Ensure UTC
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(Guid userId, Guid transactionId)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId && t.UserId == userId);
            if (transaction == null)
            {
                throw new KeyNotFoundException("Transaction not found.");
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TransactionDto>> FilterTransactionsAsync(Guid userId, DateTime? startDate, DateTime? endDate, int? categoryId, string type)
        {
            var query = _context.Transactions
                                .Where(t => t.UserId == userId)
                                .Include(t => t.Category)
                                .AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(t => t.Date >= startDate.Value.ToUniversalTime());
            }
            if (endDate.HasValue)
            {
                query = query.Where(t => t.Date <= endDate.Value.ToUniversalTime());
            }
            if (categoryId.HasValue)
            {
                query = query.Where(t => t.CategoryId == categoryId.Value);
            }
            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(t => t.Type.ToLower() == type.ToLower());
            }

            var transactions = await query.ToListAsync();
            return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }

        public async Task<Dictionary<string, decimal>> GetMonthlySummaryAsync(Guid userId)
        {
            var summary = await _context.Transactions
                                        .Where(t => t.UserId == userId && t.Date.Month == DateTime.UtcNow.Month && t.Date.Year == DateTime.UtcNow.Year)
                                        .GroupBy(t => t.Type)
                                        .Select(g => new { Type = g.Key, TotalAmount = g.Sum(t => t.Amount) })
                                        .ToListAsync();

            return summary.ToDictionary(s => s.Type, s => s.TotalAmount);
        }

        public async Task<Dictionary<string, decimal>> GetYearlySummaryAsync(Guid userId)
        {
            var summary = await _context.Transactions
                                        .Where(t => t.UserId == userId && t.Date.Year == DateTime.UtcNow.Year)
                                        .GroupBy(t => t.Type)
                                        .Select(g => new { Type = g.Key, TotalAmount = g.Sum(t => t.Amount) })
                                        .ToListAsync();

            return summary.ToDictionary(s => s.Type, s => s.TotalAmount);
        }
    }
}
