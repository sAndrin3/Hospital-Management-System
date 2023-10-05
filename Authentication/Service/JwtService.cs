using Authentication.Models;
using Authentication.Service.IService;
using Authentication.Utility;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Authentication.Service
{
    public class JwtService:IJWTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;
        public JwtService(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
        }
        public string GenerateToken(ApplicationUser user, IEnumerable<string> roles)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            //Signing Credentials
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //payload-data
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.Name));
            claims.Add(new Claim(JwtRegisteredClaimNames.PhoneNumber, user.PhoneNumber));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

            //add Roles
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Expires = DateTime.UtcNow.AddHours(3),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = cred
            };

            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
