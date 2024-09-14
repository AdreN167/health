using Health.Core.Features.Authentication.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Authentication.Commands.Login;

public class LoginCommand : IRequest<BaseResponse<LoginDto>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

