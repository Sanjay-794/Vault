using Devnet.Vault.Application.Features.Groups.Commands;
using Devnet.Vault.Application.Features.Groups.DTOs;
using Devnet.Vault.Application.Features.Groups.Interfaces.Repositories;
using Devnet.Vault.Domain.Entities.Groups;
using MediatR;
using static Devnet.Vault.Domain.Constants.Messages.ValidationMessages;

namespace Devnet.Vault.Application.Features.Groups.Handlers;

public class CreateGroupCommandHandler(IGroupRepository _groupRepository) : IRequestHandler<CreateGroupCommand, CreateGroupResponse>
{
    public async Task<CreateGroupResponse> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var req = request.Request;
        var userId = request.UserId;

        // If group exists and not deleted -> fail
        var exists = await _groupRepository.DoesGroupExist(req.Name, userId, req.ParentGroupId, cancellationToken);
        if (exists)
            throw new InvalidOperationException(GroupValidationMessages.GROUP_ALREADY_EXISTS);

        // Try to reuse a deleted group record
        var candidate = new GroupDetails
        {
            Name = req.Name,
            ParentGroupId = req.ParentGroupId,
            OwnerId = userId,
            IsFavourite = req.IsFavourite,
            MetadataJson = req.MetadataJson ?? string.Empty,
            GroupType = req.GroupType,
            CreatedBy = userId,
            CreatedDate = DateTime.UtcNow
        };
        CreateGroupResponse createGroupResponse = new();
        var reused = await _groupRepository.ReuseDeletedGroupName(candidate, cancellationToken);
        if (reused != null)
        {
            createGroupResponse.GroupId = reused.GroupId;
            return createGroupResponse;
        }


        var ok = await _groupRepository.CreateNewGroup(candidate, cancellationToken);
        if (!ok)
            throw new InvalidOperationException(GroupValidationMessages.FAILED_GROUP_CREATION);
        createGroupResponse.GroupId = candidate.GroupId;
        return createGroupResponse;
    }
}
