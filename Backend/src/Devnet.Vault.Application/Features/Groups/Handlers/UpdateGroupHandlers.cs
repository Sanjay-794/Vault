using Devnet.Vault.Application.Features.Groups.Commands;
using Devnet.Vault.Application.Features.Groups.Interfaces.Repositories;
using MediatR;

namespace Devnet.Vault.Application.Features.Groups.Handlers;

public class UpdateGroupNameCommandHandler(IGroupRepository _groupRepository) : IRequestHandler<UpdateGroupNameCommand, bool>
{
    public async Task<bool> Handle(UpdateGroupNameCommand request, CancellationToken cancellationToken)
    {
        var req = request.Request;
        return await _groupRepository.UpdateGroupName(req.Name, req.GroupId, request.GroupOwnerId, request.UserId);
    }
}

public class UpdateGroupFavouriteCommandHandler(IGroupRepository _groupRepository) : IRequestHandler<UpdateGroupFavouriteCommand, bool>
{
    public async Task<bool> Handle(UpdateGroupFavouriteCommand request, CancellationToken cancellationToken)
    {
        var req = request.Request;
        return await _groupRepository.UpdateGroupFavouriteStatus(req.IsFavourite, req.GroupId, request.GroupOwnerId, request.UserId);
    }
}

public class UpdateGroupParentCommandHandler(IGroupRepository _groupRepository) : IRequestHandler<UpdateGroupParentCommand, bool>
{
    public async Task<bool> Handle(UpdateGroupParentCommand request, CancellationToken cancellationToken)
    {
        var req = request.Request;
        return await _groupRepository.UpdateGroupParent(req.ParentGroupId, req.GroupId, request.GroupOwnerId, request.UserId);
    }
}

public class UpdateGroupMetaDataJsonCommandHandler(IGroupRepository _groupRepository) : IRequestHandler<UpdateGroupMetaDataJsonCommand, bool>
{
    public async Task<bool> Handle(UpdateGroupMetaDataJsonCommand request, CancellationToken cancellationToken)
    {
        var req = request.Request;
        return await _groupRepository.UpdateGroupMetadata(req.GroupId, req.MetadataJson, request.GroupOwnerId, request.UserId);
    }
}

public class DeleteGroupCommandHandler(IGroupRepository _groupRepository) : IRequestHandler<DeleteGroupCommand, bool>
{
    public async Task<bool> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var req = request.Request;
        return await _groupRepository.DeleteGroup(req.GroupId, request.GroupOwnerId, request.UserId);
    }
}
