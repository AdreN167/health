using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Authentication.Commands.CreateAdminUser;

public class CreateAdminUserCommand : IRequest<BaseResponse<string>>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

