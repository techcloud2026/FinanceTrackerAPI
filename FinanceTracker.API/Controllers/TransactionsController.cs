using FinanceTracker.API.DTOs;
using FinanceTracker.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinanceTracker.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("User ID not found in token.");
            }
            return Guid.Parse(userIdClaim.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            try
            {
                var userId = GetUserId();
                var transactions = await _transactionService.GetAllTransactionsAsync(userId);
                return Ok(transactions);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching transactions.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            try
            {
                var userId = GetUserId();
                var transaction = await _transactionService.GetTransactionByIdAsync(userId, id);
                if (transaction == null)
                {
                    return NotFound(new { message = $"Transaction with ID {id} not found." });
                }
                return Ok(transaction);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the transaction.", error = ex.Message });
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterTransactions(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int? categoryId,
            [FromQuery] string? type)
        {
            try
            {
                var userId = GetUserId();
                var transactions = await _transactionService.FilterTransactionsAsync(userId, startDate, endDate, categoryId, type);
                return Ok(transactions);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while filtering transactions.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] CreateTransactionDto createTransactionDto)
        {
            try
            {
                var userId = GetUserId();
                var newTransaction = await _transactionService.AddTransactionAsync(userId, createTransactionDto);
                return CreatedAtAction(nameof(GetTransactionById), new { id = newTransaction.Id }, newTransaction);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding the transaction.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(Guid id, [FromBody] UpdateTransactionDto updateTransactionDto)
        {
            try
            {
                var userId = GetUserId();
                await _transactionService.UpdateTransactionAsync(userId, id, updateTransactionDto);
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the transaction.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            try
            {
                var userId = GetUserId();
                await _transactionService.DeleteTransactionAsync(userId, id);
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the transaction.", error = ex.Message });
            }
        }

        [HttpGet("summary/monthly")]
        public async Task<IActionResult> GetMonthlySummary()
        {
            try
            {
                var userId = GetUserId();
                var summary = await _transactionService.GetMonthlySummaryAsync(userId);
                return Ok(summary);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching monthly summary.", error = ex.Message });
            }
        }

        [HttpGet("summary/yearly")]
        public async Task<IActionResult> GetYearlySummary()
        {
            try
            {
                var userId = GetUserId();
                var summary = await _transactionService.GetYearlySummaryAsync(userId);
                return Ok(summary);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching yearly summary.", error = ex.Message });
            }
        }
    }
}
