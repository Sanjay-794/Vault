using Devnet.Vault.Application.Features.Account.DTOs;
using MediatR;

namespace Devnet.Vault.Application.Features.Account.Queries;

public record GetProfileDetailsQuery(long UserId) : IRequest<ProfileDetailsResponse>;
