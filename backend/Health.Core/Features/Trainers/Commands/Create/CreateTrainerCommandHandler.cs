using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Common;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Trainers.Commands.Create;

public class CreateTrainerCommandHandler(ApplicationDbContext context)
    : IRequestHandler<CreateTrainerCommand, BaseResponse<long>>
{
    public async Task<BaseResponse<long>> Handle(CreateTrainerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new BaseResponse<long>
                {
                    ErrorCode = (int)ErrorCode.InvalidRequest,
                    ErrorMessage = ErrorMessages.InvalidRequest
                };
            }

            var newTrainer = new Trainer
            {
                Name = request.Name
            };

            if (request.Image != null)
            {
                var fileName = $"trainer-{Guid.NewGuid()}-{request.Image.FileName}";
                var filePath = Path.Combine(Constants.TRAINERS_FOLDER, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(stream);
                }

                newTrainer.FileName = fileName;
            }
            else
            {
                newTrainer.FileName = "";
            }

            await context.Trainers.AddAsync(newTrainer, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<long>
            {
                Data = newTrainer.Id
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

