using Health.Core.Features.Authentication.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Interfaces.Services;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.Authentication.Commands.Registration;

public class RegistrationCommandHandler(ApplicationDbContext context, IPasswordService passwordService)
    : IRequestHandler<RegistrationCommand, BaseResponse<RegistrationDto>>
{
    public async Task<BaseResponse<RegistrationDto>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        if (request.Password != request.ConfirmPassword)
        {
            return new BaseResponse<RegistrationDto>()
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
                return new BaseResponse<RegistrationDto>()
                {
                    ErrorMessage = ErrorMessages.UserAlreadyExists,
                    ErrorCode = (int)ErrorCode.UserAlreadyExists
                };
            }
            var hashUserPassword = passwordService.HashPassword(request.Password);

            user = new User()
            {
                Email = request.Email,
                Password = hashUserPassword
            };

            await context.Users.AddAsync(user, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<RegistrationDto>()
            {
                Data = new RegistrationDto() { Email = user.Email }
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<RegistrationDto>()
            {
                ErrorMessage = ErrorMessages.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }
}

