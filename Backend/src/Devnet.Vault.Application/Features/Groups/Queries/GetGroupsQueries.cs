using Devnet.Vault.Application.Features.Groups.DTOs;
using Devnet.Vault.Domain.Enums;
using MediatR;

namespace Devnet.Vault.Application.Features.Groups.Queries;

public record GetChildGroupsQuery(long OwnerId, long? ParentGroupId, GroupType GroupType) : IRequest<List<GroupDetailsResponse>>;
public record GetParentGroupQuery(long OwnerId, long ChildGroupId) : IRequest<GroupDetailsResponse?>;
public record GetGroupByIdQuery(long GroupId) : IRequest<GroupDetailsResponse?>;
