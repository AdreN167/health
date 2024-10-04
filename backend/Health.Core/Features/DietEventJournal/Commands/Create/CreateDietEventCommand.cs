using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.DietEventJournal.Commands.Create;

public class CreateDietEventCommand : IRequest<BaseResponse<long>>
{
    public long DietId { get; set; }
    public string Date { get; set; }
    public ICollection<long>? ProductIds { get; set; }
    public ICollection<long>? DishIds { get; set; }
}

