using Microsoft.AspNetCore.Mvc;
using server.Models;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // 임시 사용자 (나중에 DB로 교체)
        public static List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                Username = "test1",
                Email = "test1@example.com",
                Password = "password123",
                StatusMessage = "안녕하세요!",
                IsOnline = false,
                CreatedAt = DateTime.Now,
                LastSeenAt = DateTime.Now
            },
            new User
            {
                Id = 2,
                Username = "test2",
                Email = "test2@example.com",
                Password = "password123",
                StatusMessage = "반갑습니다~",
                IsOnline = false,
                CreatedAt = DateTime.Now,
                LastSeenAt = DateTime.Now
            }
        };

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return Ok(_users);
        }

        [HttpGet("{id:long}")]
        public ActionResult<User> GetUserById(long id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { message = "사용자를 찾을 수 없습니다." });
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<User> Login([FromBody] LoginRequest request)
        {
            var user = _users.FirstOrDefault(u => u.Username == request.Username);

            if (user == null)
                return Unauthorized(new { message = "사용자를 찾을 수 없습니다." });

            // 실제로는 비밀번호 검증 로직이 필요
            user.IsOnline = true;
            user.LastSeenAt = DateTime.UtcNow;

            return Ok(user);
        }

        // DTO
        public class LoginRequest
        {
            public string Username { get; set; } = string.Empty;  
            public string Password { get; set; } = string.Empty;  
        }
        
    }
}

