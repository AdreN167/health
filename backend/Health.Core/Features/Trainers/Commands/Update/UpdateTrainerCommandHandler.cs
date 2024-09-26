using AutoMapper;
using Health.Core.Features.Trainers.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Common;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Trainers.Commands.Update;

public class UpdateTrainerCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<UpdateTrainerCommand, BaseResponse<TrainerDto>>
{
    public async Task<BaseResponse<TrainerDto>> Handle(UpdateTrainerCommand request, CancellationToken cancellationToken)
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

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new BaseResponse<TrainerDto>
                {
                    ErrorCode = (int)ErrorCode.InvalidRequest,
                    ErrorMessage = ErrorMessages.InvalidRequest
                };
            }

            trainer.Name = request.Name;

            if (request.Image != null)
            {
                var folder = Constants.TRAINERS_FOLDER;

                if (!string.IsNullOrWhiteSpace(trainer.FileName))
                {
                    File.Delete(Path.Combine(folder, trainer.FileName));
                }

                var newFileName = $"trainer-{Guid.NewGuid()}-{request.Image.FileName}";
                var filePath = Path.Combine(folder, newFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(stream);
                }

                trainer.FileName = newFileName;
            }

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

