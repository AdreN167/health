using AutoMapper;
using Health.Core.Features.Exercises.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Exercises.Commands.Delete;

public class DeleteExerciseCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<DeleteExerciseCommand, BaseResponse<ExerciseDto>>
{
    public async Task<BaseResponse<ExerciseDto>> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exercise = await context.Exercises.FindAsync([request.Id], cancellationToken);

            if (exercise == null)
            {
                return new BaseResponse<ExerciseDto>
                {
                    ErrorCode = (int)ErrorCode.ExerciseNotFound,
                    ErrorMessage = ErrorMessages.ExerciseNotFound
                };
            }

            context.Exercises.Remove(exercise);
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

