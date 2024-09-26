using AutoMapper;
using Health.Core.Features.Dishes.Dto;
using Health.Core.Features.Exercises.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.Exercises.Commands.Update;

public class UpdateExerciseCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<UpdateExerciseCommand, BaseResponse<ExerciseDto>>
{
    public async Task<BaseResponse<ExerciseDto>> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exercise = await context.Exercises
                .Where(ex => ex.Id == request.Id)
                .Include(ex => ex.Trainer)
                .FirstOrDefaultAsync(cancellationToken);

            if (exercise == null)
            {
                return new BaseResponse<ExerciseDto>
                {
                    ErrorCode = (int)ErrorCode.ExerciseNotFound,
                    ErrorMessage = ErrorMessages.ExerciseNotFound
                };
            }

            if (string.IsNullOrWhiteSpace(request.Description)
                || string.IsNullOrWhiteSpace(request.Name)
                || request.CaloriesBurned <= 0)
            {
                return new BaseResponse<ExerciseDto>
                {
                    ErrorCode = (int)ErrorCode.InvalidRequest,
                    ErrorMessage = ErrorMessages.InvalidRequest
                };
            }

            exercise.Name = request.Name;
            exercise.Description = request.Description;
            exercise.CaloriesBurned = request.CaloriesBurned;

            exercise.Trainer = null;

            if (request.TrainerId != null && request.TrainerId > 0)
            {
                var trainer = await context.Trainers
                    .Where(t => t.Id == request.TrainerId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (trainer == null)
                {
                    return new BaseResponse<ExerciseDto>
                    {
                        ErrorCode = (int)ErrorCode.TrainerNotFound,
                        ErrorMessage = ErrorMessages.TrainerNotFound
                    };
                }

                exercise.Trainer = trainer;
            }

            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<ExerciseDto>
            {
                Data = mapper.Map<ExerciseDto>(exercise)
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ExerciseDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

