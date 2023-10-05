using Authentication.Data;
using Authentication.Models;
using Authentication.Models.Dto;
using Authentication.Service.IService;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Service
{
    public class UserService : IUserInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IJWTokenGenerator _jwtGenerator;
        public UserService(ApplicationDbContext database, UserManager<ApplicationUser> userManager, IJWTokenGenerator tokenGenerator, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _context = database;
            _jwtGenerator = tokenGenerator;
        }
        public async Task<bool> AssignUserRole(string email, string Rolename)
        {
            //Get user by email
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if(user != null)
            {
                //check if user exists and assign role
                if (!_roleManager.RoleExistsAsync(Rolename).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(Rolename)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, Rolename);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginDto loginDto)
        {
            //Get user by userName
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName.ToLower()==loginDto.UserName.ToLower());
            Console.WriteLine(user.UserName);
            //Verify password
            var isValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            //check if user is null or PasswordHasher is wrong
            if(!isValid || user == null)
            {
                new LoginResponseDto();
            }
            //user provides right credential
            var roles = await _userManager.GetRolesAsync(user);
            //create Token
            var token = _jwtGenerator.GenerateToken(user, roles);
            Console.WriteLine(token);
            var loggedUser = new LoginResponseDto()
            {
                User = _mapper.Map<UserDto>(user),
                Token = token
            };
            return loggedUser;
        }

        public async Task<string> RegisterUser(RegisterDto registerDto)
        {
            var user = _mapper.Map<ApplicationUser>(registerDto);
            try
            {
                //user is Created
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if(result.Succeeded)
                {
                    return "";
                }
                else
                {
                    //identify error if any
                    return result.Errors.FirstOrDefault().Description;
                }
            }catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

    
}
