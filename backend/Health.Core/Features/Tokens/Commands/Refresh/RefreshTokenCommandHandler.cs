using Health.Core.Features.Tokens.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Interfaces.Services;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.Tokens.Commands.Refresh;

public class RefreshTokenCommandHandler(ApplicationDbContext context, ITokenService tokenService)
    : IRequestHandler<RefreshTokenCommand, BaseResponse<TokenDto>>
{
    public async Task<BaseResponse<TokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        string accessToken = request.AccessToken;
        string refreshToken = request.RefreshToken;

        var claimPrincipal = tokenService.GetPrincipalFromExpiredToken(accessToken);
        var userName = claimPrincipal.Identity?.Name; // userName = Email

        var user = await context.Users
            .Where(x => x.Email == userName)
            .Include(x => x.UserToken)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null 
            || user.UserToken.RefreshToken != refreshToken 
            || user.UserToken.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return new BaseResponse<TokenDto>()
            {
                ErrorMessage = ErrorMessages.InvalidClientRequest
                // нет смысла для нового кода ошибки...
            };
        }

        var newAccessToken = tokenService.GenerateAccessToken(claimPrincipal.Claims);
        var newRefreshToken = tokenService.GenerateRefreshToken();

        user.UserToken.RefreshToken = newRefreshToken;

        await context.SaveChangesAsync(cancellationToken);

        return new BaseResponse<TokenDto>
        {
            Data = new TokenDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken, // лучше не возвращать клиенту, тут просто для наглядности
            }
        };
    }
}

