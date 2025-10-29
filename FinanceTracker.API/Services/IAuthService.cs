using FinanceTracker.API.DTOs;
using FinanceTracker.API.Models;

namespace FinanceTracker.API.Services
{
    public interface IAuthService
    {
        Task<string> Register(RegisterDto dto);
        Task<string> Login(LoginDto dto);
    }
}
