using Authentication.Models.Dto;

namespace Authentication.Service.IService
{
    public interface IUserInterface
    {
        Task <string> RegisterUser(RegisterDto registerDto);
        Task<LoginResponseDto> Login(LoginDto loginDto);
        Task<bool> AssignUserRole(string email, string Rolename);
    }
}
