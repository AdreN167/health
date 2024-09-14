namespace Health.Domain.Interfaces.Services;

public interface IPasswordService
{
    string HashPassword(string userPassword);
    bool IsVerifyPassword(string userPasswordHash, string userPassword);

}

