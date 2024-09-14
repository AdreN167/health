using Health.Core.Features.Authentication.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Authentication.Commands.Registration;

public class RegistrationCommand : IRequest<BaseResponse<RegistrationDto>>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

