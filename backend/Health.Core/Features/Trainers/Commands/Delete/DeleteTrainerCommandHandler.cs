using AutoMapper;
using Health.Core.Features.Trainers.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Common;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Trainers.Commands.Delete;

public class DeleteTrainerCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<DeleteTrainerCommand, BaseResponse<TrainerDto>>
{
    public async Task<BaseResponse<TrainerDto>> Handle(DeleteTrainerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var trainer = await context.Trainers.FindAsync([request.Id], cancellationToken);

            if (trainer == null)
            {
                return new BaseResponse<TrainerDto>
                {
                    ErrorCode = (int)ErrorCode.TrainerNotFound,
                    ErrorMessage = ErrorMessages.TrainerNotFound
                };
            }

            if (!string.IsNullOrWhiteSpace(trainer.FileName))
            {
                File.Delete(Path.Combine(Constants.TRAINERS_FOLDER, trainer.FileName));
            }

            context.Trainers.Remove(trainer);
            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<TrainerDto>
            {
                Data = mapper.Map<TrainerDto>(trainer)
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<TrainerDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

