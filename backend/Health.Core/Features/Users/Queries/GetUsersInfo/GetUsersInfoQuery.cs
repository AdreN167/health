using Health.Core.Features.Users.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Users.Queries.GetUsersInfo;

public record GetUsersInfoQuery(string Email) : IRequest<BaseResponse<UsersInfoDto>>;

