using AutoMapper;
using FinanceTracker.API.DTOs;
using FinanceTracker.API.Models;

namespace FinanceTracker.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();
            CreateMap<Transaction, TransactionDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Budget, BudgetDto>().ReverseMap();
            CreateMap<Goal, GoalDto>().ReverseMap();
        }
    }
}
