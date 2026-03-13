using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthRepository authRepository, ILogger<AuthController> logger)
        {
            _authRepository = authRepository;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await _authRepository.UserExistsAsync(dto.Email))
            {
                return BadRequest("User already exists.");
            }

            var user = await _authRepository.RegisterAsync(dto);

            _logger.LogInformation("User registered successfully with email: {Email}", dto.Email);

            return Ok(new
            {
                message = "User registered successfully.",
                user.UserName,
                user.Email
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var response = await _authRepository.LoginAsync(dto);

            if (response == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            _logger.LogInformation("User logged in successfully with email: {Email}", dto.Email);

            return Ok(response);
        }
    }
}