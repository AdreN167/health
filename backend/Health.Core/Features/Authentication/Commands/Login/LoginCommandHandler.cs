using Health.Core.Features.Authentication.Dtos;
using Health.Core.Features.Tokens.Dtos;
using Health.Core.Resources;
using Health.Core.Services;
using Health.DAL;
using Health.Domain.Interfaces.Services;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Health.Core.Features.Authentication.Commands.Login;

public class LoginCommandHandler(ApplicationDbContext context, ITokenService tokenService, IPasswordService passwordService)
    : IRequestHandler<LoginCommand, BaseResponse<LoginDto>>
{
    public async Task<BaseResponse<LoginDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await context.Users
                .Where(x => x.Email == request.Email)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                return new BaseResponse<LoginDto>()
                {
                    ErrorMessage = ErrorMessages.UserNotFound,
                    ErrorCode = (int)ErrorCode.UserNotFound
                };
            }

            if (!passwordService.IsVerifyPassword(user.Password, request.Password))
            {
                return new BaseResponse<LoginDto>()
                {
                    ErrorMessage = ErrorMessages.WrongPassword,
                    ErrorCode = (int)ErrorCode.WrongPassword
                };
            }

            var userToken = await context.UserTokens.FirstOrDefaultAsync(x => x.UserId == user.Id);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var accessToken = tokenService.GenerateAccessToken(claims);
            var refreshToken = tokenService.GenerateRefreshToken();

            if (userToken == null)
            {
                userToken = new UserToken()
                {
                    UserId = user.Id,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7),
                };

                await context.UserTokens.AddAsync(userToken, cancellationToken);
            }
            else
            {
                userToken.RefreshToken = refreshToken;
                userToken.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

                context.UserTokens.Update(userToken);
            }

            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<LoginDto>()
            {
                Data = new LoginDto()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    Role = user.Role.ToString()
                }
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<LoginDto>()
            {
                ErrorMessage = ErrorMessages.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }
}

