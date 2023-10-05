using Authentication.Models;

namespace Authentication.Service.IService
{
    public interface IJWTokenGenerator
    {
        string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}
