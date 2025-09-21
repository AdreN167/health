using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.WorkoutEventJournal.Commands.Create;

public class CreateWorkoutEventCommand : IRequest<BaseResponse<long>>
{
    public long WorkoutId { get; set; }
    public string Date { get; set; } 
}

