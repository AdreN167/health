namespace Health.Core.Features.Authentication.Dtos;

public class LoginDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string Email { get; set; }
}

