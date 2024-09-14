using Health.Domain.Interfaces;

namespace Health.Domain.Models.Entities;

public class UserToken : IEntity
{
    public long Id { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public User User { get; set; }
    public long UserId { get; set; }
}

