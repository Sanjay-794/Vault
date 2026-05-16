using Devnet.Vault.Domain.Entities.Groups;
using Devnet.Vault.Domain.Enums;

namespace Devnet.Vault.Application.Features.Groups.Interfaces.Repositories;

public interface IGroupRepository
{
    Task<bool> CreateNewGroup(GroupDetails group);
    Task<bool> DoesGroupExist(string groupName, long ownerId, long? parentGroupId);
    Task<GroupDetails?> ReuseDeletedGroupName(GroupDetails group);
    Task<bool> UpdateGroupName(string groupName, long groupId, long ownerId, long updatedBy);
    Task<bool> UpdateGroupFavouriteStatus(bool isFavourite, long groupId, long ownerId, long updatedBy);
    Task<bool> UpdateGroupParent(long? parentGroupId, long groupId, long ownerId, long updatedBy);
    Task<bool> UpdateGroupMetadata(long groupId, string metadataJson, long ownerId, long updatedBy);
    Task<bool> DeleteGroup(long groupId, long ownerId, long updatedBy);
    Task<List<GroupDetails>> GetChildGroupDetails(long ownerId, long? parentGroupId, GroupType groupType);
    Task<GroupDetails?> GetParentGroupDetails(long ownerId, long childGroupId);
    Task<GroupDetails?> GetGroupDetailsById(long groupId);
}
