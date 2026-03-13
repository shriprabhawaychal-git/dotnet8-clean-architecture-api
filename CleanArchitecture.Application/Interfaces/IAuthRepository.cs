using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> UserExistsAsync(string email);

        Task<AppUser> RegisterAsync(RegisterDto dto);

        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    }
}