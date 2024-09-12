namespace Health.Domain.Models.Response;

public class CollectionResponse<T> : BaseResponse<ICollection<T>>
{
    public int Count { get; set; }
}

