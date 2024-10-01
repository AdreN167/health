using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Health.Core.Features.Goals.Commands.Create;

public class CreateGoalCommandHandler(ApplicationDbContext context)
    : IRequestHandler<CreateGoalCommand, BaseResponse<long>>
{
    public async Task<BaseResponse<long>> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == request.UserEmail, cancellationToken);

            if (user == null)
            {
                return new BaseResponse<long>
                {
                    ErrorCode = (int)ErrorCode.UserNotFound,
                    ErrorMessage = ErrorMessages.UserNotFound
                };
            }

            if (string.IsNullOrWhiteSpace(request.Name) 
                || string.IsNullOrWhiteSpace(request.Deadline)
                || !DateTime.TryParse(request.Deadline, out var deadline))
            {
                return new BaseResponse<long>
                {
                    ErrorCode = (int)ErrorCode.InvalidRequest,
                    ErrorMessage = ErrorMessages.InvalidRequest
                };
            }

            var newGoal = new Goal
            {
                Name = request.Name,
                Description = request.Description ?? "",
                Deadline = deadline,
                User = user
            };

            await context.Goals.AddAsync(newGoal, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<long>
            {
                Data = newGoal.Id
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<long>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

