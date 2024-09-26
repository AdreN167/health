using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.Exercises.Commands.Create;

public class CreateExerciseCommandHandler(ApplicationDbContext context)
    : IRequestHandler<CreateExerciseCommand, BaseResponse<long>>
{
    public async Task<BaseResponse<long>> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Name)
                || string.IsNullOrWhiteSpace(request.Description)
                || request.CaloriesBurned <= 0)
            {
                return new BaseResponse<long>
                {
                    ErrorCode = (int)ErrorCode.InvalidRequest,
                    ErrorMessage = ErrorMessages.InvalidRequest
                };
            }

            var newExercise = new Exercise
            {
                Name = request.Name,
                Description = request.Description,
            };

            if (request.TrainerId != null && request.TrainerId > 0)
            {
                var trainer = await context.Trainers
                    .Where(t => t.Id == request.TrainerId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (trainer == null)
                {
                    return new BaseResponse<long>
                    {
                        ErrorCode = (int)ErrorCode.TrainerNotFound,
                        ErrorMessage = ErrorMessages.TrainerNotFound
                    };
                }

                newExercise.Trainer = trainer;
            }
            
            await context.Exercises.AddAsync(newExercise, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<long>
            {
                Data = newExercise.Id
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

