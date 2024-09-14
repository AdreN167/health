using Health.Core.Features.Tokens.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Tokens.Commands.Refresh;

public class RefreshTokenCommand : IRequest<BaseResponse<TokenDto>>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

