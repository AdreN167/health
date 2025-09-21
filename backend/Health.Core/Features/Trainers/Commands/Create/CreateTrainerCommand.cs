using Health.Domain.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Health.Core.Features.Trainers.Commands.Create;

public class CreateTrainerCommand : IRequest<BaseResponse<long>>
{
    public string Name { get; set; }
    public IFormFile? Image { get; set; }
}

