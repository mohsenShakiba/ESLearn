using System.Threading.Tasks;
using ESLearn.API.DTOs;
using ESLearn.Domain.AggregatesModel.PostsAggregate;
using ESLearn.Domain.AggregatesModel.UsersAggregate;
using Microsoft.AspNetCore.Mvc;

namespace ESLearn.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("insert")]
        public async Task<IActionResult> InsertUser(CreateUserDto dto)
        {
            var user = new User(dto.UserName, dto.Password);
            await _userRepository.IndexAsync(user);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> FindUser(string userId)
        {
            var user = await _userRepository.QueryAsync(userId);
            return Ok(user);
        }
        
    }
}