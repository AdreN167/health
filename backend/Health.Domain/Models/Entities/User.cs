using Health.Domain.Interfaces;
using Health.Domain.Models.Enums;

namespace Health.Domain.Models.Entities;

public class User : IEntity
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Age { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public virtual UserToken? UserToken { get; set; }
    public Role Role { get; set; }
    public virtual ICollection<Goal>? Goals { get; set; }
}

