using Devnet.Vault.Application.Features.Groups.Commands;
using Devnet.Vault.Application.Features.Groups.Interfaces.Repositories;
using Devnet.Vault.Domain.Entities.Groups;
using MediatR;
using static Devnet.Vault.Domain.Constants.Messages.ValidationMessages;

namespace Devnet.Vault.Application.Features.Groups.Handlers;

public class CreateGroupCommandHandler(IGroupRepository _groupRepository) : IRequestHandler<CreateGroupCommand, long>
{
    public async Task<long> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var req = request.Request;
        var userId = request.UserId;

        // If group exists and not deleted -> fail
        var exists = await _groupRepository.DoesGroupExist(req.Name, userId, req.ParentGroupId);
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

        var reused = await _groupRepository.ReuseDeletedGroupName(candidate);
        if (reused != null)
            return reused.GroupId;

        var ok = await _groupRepository.CreateNewGroup(candidate);
        if (!ok)
            throw new InvalidOperationException(GroupValidationMessages.FAILED_GROUP_CREATION);

        return candidate.GroupId;
    }
}
