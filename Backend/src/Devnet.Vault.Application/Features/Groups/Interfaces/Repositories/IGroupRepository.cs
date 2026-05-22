using Devnet.Vault.Domain.Entities.Groups;
using Devnet.Vault.Domain.Enums;

namespace Devnet.Vault.Application.Features.Groups.Interfaces.Repositories;

public interface IGroupRepository
{
    Task<bool> CreateNewGroup(GroupDetails group, CancellationToken ctx);
    Task<bool> DoesGroupExist(string groupName, long ownerId, long? parentGroupId, CancellationToken ctx);
    Task<GroupDetails?> ReuseDeletedGroupName(GroupDetails group, CancellationToken ctx);
    Task<bool> UpdateGroupName(string groupName, long groupId, long ownerId, long updatedBy, CancellationToken ctx);
    Task<bool> UpdateGroupFavouriteStatus(bool isFavourite, long groupId, long ownerId, long updatedBy, CancellationToken ctx);
    Task<bool> UpdateGroupParent(long? parentGroupId, GroupType groupType, long groupId, long ownerId, long updatedBy, CancellationToken ctx);
    Task<bool> UpdateGroupMetadata(long groupId, string metadataJson, long ownerId, long updatedBy, CancellationToken ctx);
    Task<bool> DeleteGroup(long groupId, long ownerId, long updatedBy, CancellationToken ctx);
    Task<List<GroupDetails>> GetChildGroupDetails(long ownerId, long? parentGroupId, GroupType groupType, CancellationToken ctx);
    Task<GroupDetails?> GetParentGroupDetails(long ownerId, long childGroupId, CancellationToken ctx);
    Task<GroupDetails?> GetGroupDetailsById(long groupId, CancellationToken ctx);
}
