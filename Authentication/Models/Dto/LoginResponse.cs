namespace Authentication.Models.Dto
{
    public class LoginResponse
    {
        public UserDto User { get; set; } = default!;
        public string Token { get; set; } = string.Empty;
    }
}
