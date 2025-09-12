using Microsoft.AspNetCore.Mvc;
using UnitOfWork.Services.Users;
using UnitOfWork.Services.Users.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnitOfWork.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")] // bu method nomi bilan yo'lni belgilash uchun
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            _logger.LogInformation("GetAllAsync method called. Retrieved {Count} users.", users.Count);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID {Id} not found.", id);
                return NotFound();
            }
            _logger.LogInformation("User with ID {Id} retrieved.", id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult>  CreateAsync([FromBody] UserCreateDto user)
        {
            if (!ModelState.IsValid) // modelni tekshirish
            {
                _logger.LogWarning("Invalid model state for CreateAsync.");
                return BadRequest(ModelState);
            }
            var userId = await _userService.AddUserAsync(user);
            _logger.LogInformation("User created successfully.");

            return Ok(userId);    
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateAsync(int id, [FromBody] UserUpdateDto user)
        {
            if (!ModelState.IsValid) // modelni tekshirish
            {
                _logger.LogWarning("Invalid model state for UpdateAsync.");
                return BadRequest(ModelState);
            }
            await _userService.UpdateUserAsync(id, user);
            _logger.LogInformation("User with ID {Id} updated successfully.", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _userService.DeleteUserAsync(id);
            _logger.LogInformation("User with ID {Id} deleted successfully.", id);
            return NoContent();
        }
    }
}
