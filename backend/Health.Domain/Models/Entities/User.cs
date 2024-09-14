using Health.Domain.Interfaces;

namespace Health.Domain.Models.Entities;

public class User : IEntity
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserToken UserToken { get; set; }

    // TODO: добавить диеты и тренировки
}

