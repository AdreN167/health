using Health.Core.Features.Users.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Users.Commands.UpdateUsersInfo;

public class UpdateUsersInfoCommand : IRequest<BaseResponse<UsersInfoDto>>
{
    public string Email { get; set; }
    public int Age { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
}

