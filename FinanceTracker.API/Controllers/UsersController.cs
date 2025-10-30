using FinanceTracker.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinanceTracker.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            // Mock implementation
            var userProfile = new UserProfileDto { Name = "John Doe", Email = "john.doe@example.com", Currency = "USD" };
            return Ok(userProfile);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileDto model)
        {
            // Mock implementation
            return NoContent();
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
        {
            // Mock implementation
            return NoContent();
        }

        [HttpDelete("delete-account")]
        public async Task<IActionResult> DeleteAccount()
        {
            // Mock implementation
            return NoContent();
        }
    }
}
