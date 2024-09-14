using Health.Domain.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace Health.Core.Services;

public class PasswordService : IPasswordService
{
    public string HashPassword(string userPassword)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(userPassword));
        return Convert.ToBase64String(hash).ToLower();
    }

    public bool IsVerifyPassword(string userPasswordHash, string userPassword)
    {
        var hash = HashPassword(userPassword);
        return hash == userPasswordHash;
    }
}

