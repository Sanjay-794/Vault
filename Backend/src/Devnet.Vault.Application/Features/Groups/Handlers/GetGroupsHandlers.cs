using Devnet.Vault.Application.Features.Groups.DTOs;
using Devnet.Vault.Application.Features.Groups.Interfaces.Repositories;
using Devnet.Vault.Application.Features.Groups.Queries;
using MediatR;

namespace Devnet.Vault.Application.Features.Groups.Handlers;

public class GetChildGroupsQueryHandler(IGroupRepository _groupRepository) : IRequestHandler<GetChildGroupsQuery, List<GroupDetailsResponse>>
{
    // Get child groups of a parent group. If parentGroupId is null, get root level groups for the owner
    public async Task<List<GroupDetailsResponse>> Handle(GetChildGroupsQuery request, CancellationToken cancellationToken)
    {
        var items = await _groupRepository.GetChildGroupDetails(request.OwnerId, request.ParentGroupId, request.GroupType);
        return [.. items.Select(g => new GroupDetailsResponse
        {
            GroupId = g.GroupId,
            Name = g.Name,
            ParentGroupId = g.ParentGroupId,
            OwnerId = g.OwnerId,
            IsFavourite = g.IsFavourite,
            MetadataJson = g.MetadataJson,
            GroupType = g.GroupType
        })];
    }
}

// Get parent group of a child group. If childGroupId is null, return null
public class GetParentGroupQueryHandler(IGroupRepository _groupRepository) : IRequestHandler<GetParentGroupQuery, GroupDetailsResponse?>
{
    public async Task<GroupDetailsResponse?> Handle(GetParentGroupQuery request, CancellationToken cancellationToken)
    {
        var parent = await _groupRepository.GetParentGroupDetails(request.OwnerId, request.ChildGroupId);
        if (parent == null) return null;
        return new GroupDetailsResponse
        {
            GroupId = parent.GroupId,
            Name = parent.Name,
            ParentGroupId = parent.ParentGroupId,
            OwnerId = parent.OwnerId,
            IsFavourite = parent.IsFavourite,
            MetadataJson = parent.MetadataJson,
            GroupType = parent.GroupType
        };
    }
}

// Get group details by group id
public class GetGroupByIdQueryHandler(IGroupRepository _groupRepository) : IRequestHandler<GetGroupByIdQuery, GroupDetailsResponse?>
{
    public async Task<GroupDetailsResponse?> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetGroupDetailsById(request.GroupId);
        if (group == null) return null;
        return new GroupDetailsResponse
        {
            GroupId = group.GroupId,
            Name = group.Name,
            ParentGroupId = group.ParentGroupId,
            OwnerId = group.OwnerId,
            IsFavourite = group.IsFavourite,
            MetadataJson = group.MetadataJson,
            GroupType = group.GroupType
        };
    }
}
