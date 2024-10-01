using Health.Domain.Interfaces;
using Health.Domain.Models.Enums;

namespace Health.Domain.Models.Entities;

public class User : IEntity
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserToken UserToken { get; set; }
    public Role Role { get; set; }
    public ICollection<Goal>? Goals { get; set; }
}

