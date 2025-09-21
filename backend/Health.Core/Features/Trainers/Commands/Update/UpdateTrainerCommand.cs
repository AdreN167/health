using Health.Core.Features.Trainers.Dtos;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Health.Core.Features.Trainers.Commands.Update;

public class UpdateTrainerCommand : IRequest<BaseResponse<TrainerDto>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public IFormFile? Image { get; set; }
}

