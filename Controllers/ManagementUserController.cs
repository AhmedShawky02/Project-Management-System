using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Dtos.Accounts;
using Project_Management_System.Interfaces;
using Project_Management_System.Models;

namespace Project_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementUserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHashRepository _passwordHashRepo;
        private readonly IUserRoleRepository _userRoleRepo;
        private readonly ITokenRepository _tokenRepo;

        public ManagementUserController(IUserRepository userRepo,
            IPasswordHashRepository passwordHashRepo,
            IUserRoleRepository userRoleRepo,
            ITokenRepository tokenRepo)
        {
            _userRepo = userRepo;
            _passwordHashRepo = passwordHashRepo;
            _userRoleRepo = userRoleRepo;
            _tokenRepo = tokenRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var UserExists = await _userRepo.GetUserByEmailAsync(registerDto.Email!);

                if(UserExists != null)
                    return BadRequest("User with this email already exists.");

                var hashPassword = _passwordHashRepo.Hash(registerDto.Password!);

                var user = new User()
                {
                    UserName = registerDto.UserName!,
                    Email = registerDto.Email!,
                    PasswordHash = hashPassword,
                    CreatedAt = DateTime.Now,
                };
                await _userRepo.CreateUserAsync(user);

                if (user.UserId <= 0)
                    return StatusCode(500, "Failed to create user.");

                var userRole = new UserRole()
                {
                    UserId = user.UserId,
                    RoleId = 2
                };

                await _userRoleRepo.AddUserRoleAsync(userRole);  
                
                user.UserRoles = await _userRoleRepo.GetUserRolesByUserId(user.UserId);

                return Ok(new NewUserDto()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = await _tokenRepo.CreateToken(user)
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var UserExists = await _userRepo.GetUserByUserNameAsync(loginDto.UserName);

                if (UserExists == null)
                    return Unauthorized("Invalid email or password.");

                var isPasswordValid = _passwordHashRepo.Verified(UserExists.PasswordHash, loginDto.Password);
                if (!isPasswordValid)
                    return Unauthorized("Invalid email or password.");

                UserExists.UserRoles = await _userRoleRepo.GetUserRolesByUserId(UserExists.UserId);

                return Ok(new NewUserDto()
                {
                    UserName = UserExists.UserName,
                    Email = UserExists.Email,
                    Token = await _tokenRepo.CreateToken(UserExists)
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
