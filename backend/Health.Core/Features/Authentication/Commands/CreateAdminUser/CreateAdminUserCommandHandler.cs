using Health.Core.Features.Authentication.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Interfaces.Services;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.Authentication.Commands.CreateAdminUser;

public class CreateAdminUserCommandHandler(ApplicationDbContext context, IPasswordService passwordService)
    : IRequestHandler<CreateAdminUserCommand, BaseResponse<string>>
{
    public async Task<BaseResponse<string>> Handle(CreateAdminUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Password != request.ConfirmPassword)
        {
            return new BaseResponse<string>()
            {
                ErrorMessage = ErrorMessages.PasswordsAreNotEqual,
                ErrorCode = (int)ErrorCode.PasswordsAreNotEqual
            };
        }

        try
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (user != null)
            {
                return new BaseResponse<string>()
                {
                    ErrorMessage = ErrorMessages.UserAlreadyExists,
                    ErrorCode = (int)ErrorCode.UserAlreadyExists
                };
            }
            var hashUserPassword = passwordService.HashPassword(request.Password);

            user = new User()
            {
                Email = request.Email,
                Password = hashUserPassword,
                Role = Role.Admin
            };

            await context.Users.AddAsync(user, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<string>()
            {
                Data = user.Email
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<string>()
            {
                ErrorMessage = ErrorMessages.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }
}

