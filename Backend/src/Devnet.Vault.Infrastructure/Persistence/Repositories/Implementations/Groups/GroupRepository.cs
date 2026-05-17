using Devnet.Vault.Application.Features.Groups.Interfaces.Repositories;
using Devnet.Vault.Domain.Entities.Groups;
using Devnet.Vault.Domain.Enums;
using Devnet.Vault.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using static Devnet.Vault.Domain.Constants.Messages.ValidationMessages;

namespace Devnet.Vault.Infrastructure.Persistence.Repositories.Implementations.Groups;

public class GroupRepository(AppDbContext _dbContext) : IGroupRepository
{
    public async Task<bool> CreateNewGroup(GroupDetails group)
    {
        try
        {
            await _dbContext.GroupDetails.AddAsync(group);
            var changes = await _dbContext.SaveChangesAsync();
            return changes > 0;
        }
        catch (Exception)
        {
            _dbContext.ChangeTracker.Clear();
            throw;
        }
    }

    public async Task<bool> DoesGroupExist(string groupName, long ownerId, long? parentGroupId)
    {
        return await _dbContext.GroupDetails
            .Where(g => g.Name == groupName && g.OwnerId == ownerId && g.ParentGroupId == parentGroupId && !g.IsDeleted)
            .AnyAsync();
    }

    public async Task<GroupDetails?> ReuseDeletedGroupName(GroupDetails group)
    {
        var existingGroup = await _dbContext.GroupDetails
            .Where(g => g.IsDeleted)
            .OrderByDescending(g =>
                g.ParentGroupId == group.ParentGroupId &&
                g.OwnerId == group.OwnerId)
            .ThenByDescending(g =>
                g.OwnerId == group.OwnerId)
            .ThenBy(g => g.GroupId)
            .FirstOrDefaultAsync();

        if (existingGroup != null)
        {
            existingGroup.Name = group.Name;
            existingGroup.ParentGroupId = group.ParentGroupId;
            existingGroup.OwnerId = group.OwnerId;
            existingGroup.IsFavourite = group.IsFavourite;
            existingGroup.MetadataJson = group.MetadataJson;
            existingGroup.GroupType = group.GroupType;

            existingGroup.IsDeleted = false;
            existingGroup.DeletedBy = null;
            existingGroup.DeletedDate = null;
            existingGroup.UpdatedDate = DateTime.UtcNow;
            existingGroup.UpdatedBy = group.CreatedBy;

            var changes = await _dbContext.SaveChangesAsync();

            if (changes > 0)
                return existingGroup;
        }

        return null;
    }

    public async Task<bool> UpdateGroupName(string groupName, long groupId, long ownerId, long updatedBy)
    {
        var changes = await _dbContext.GroupDetails.Where(g => g.GroupId == groupId && g.OwnerId == ownerId && !g.IsDeleted)
               .ExecuteUpdateAsync(X => X.SetProperty(g => g.Name, groupName)
               .SetProperty(g => g.UpdatedDate, DateTime.UtcNow)
               .SetProperty(g => g.UpdatedBy, updatedBy)
               );
        return changes > 0;
    }

    public async Task<bool> UpdateGroupFavouriteStatus(bool isFavourite, long groupId, long ownerId, long updatedBy)
    {
        var changes = await _dbContext.GroupDetails.Where(g => g.GroupId == groupId && g.OwnerId == ownerId && !g.IsDeleted)
               .ExecuteUpdateAsync(X => X.SetProperty(g => g.IsFavourite, isFavourite)
               .SetProperty(g => g.UpdatedDate, DateTime.UtcNow)
               .SetProperty(g => g.UpdatedBy, updatedBy)
               );
        return changes > 0;
    }

    public async Task<bool> UpdateGroupParent(long? parentGroupId, long groupId, long ownerId, long updatedBy)
    {
        // Cannot move inside itself
        if (parentGroupId == groupId)
            return false;

        // Current group info
        var currentGroup = await _dbContext.GroupDetails
            .Where(g => g.GroupId == groupId
                        && g.OwnerId == ownerId
                        && !g.IsDeleted)
            .Select(g => new
            {
                g.GroupId,
                g.ParentGroupId
            })
            .FirstOrDefaultAsync() ?? throw new InvalidOperationException(GroupValidationMessages.GROUP_NOT_FOUND);

        // New parent must belong to same level
        var isSameLevel = await _dbContext.GroupDetails
            .AnyAsync(g => g.GroupId == parentGroupId
                        && g.OwnerId == ownerId
                        && !g.IsDeleted
                        && g.ParentGroupId == currentGroup.ParentGroupId);

        if (!isSameLevel)
            throw new InvalidOperationException(GroupValidationMessages.DRAG_ALLOWED_AT_SAME_LEVEL);

        var changes = await _dbContext.GroupDetails
            .Where(g => g.GroupId == groupId)
            .ExecuteUpdateAsync(x => x
                .SetProperty(g => g.ParentGroupId, parentGroupId)
                .SetProperty(g => g.UpdatedDate, DateTime.UtcNow)
                .SetProperty(g => g.UpdatedBy, updatedBy));

        return changes > 0;
    }

    public async Task<bool> UpdateGroupMetadata(long groupId, string metadataJson, long ownerId, long updatedBy)
    {
        var changes = await _dbContext.GroupDetails.Where(g => g.GroupId == groupId && g.OwnerId == ownerId && !g.IsDeleted)
               .ExecuteUpdateAsync(X => X.SetProperty(g => g.MetadataJson, metadataJson)
               .SetProperty(g => g.UpdatedDate, DateTime.UtcNow)
               .SetProperty(g => g.UpdatedBy, updatedBy)
               );
        return changes > 0;
    }

    public async Task<bool> DeleteGroup(long groupId, long ownerId, long updatedBy)
    {
        // Only allow deletion if there are no vault items or child groups inside this group
        var canDelete = await _dbContext.GroupDetails
            .Where(g => g.GroupId == groupId
                        && g.OwnerId == ownerId
                        && !g.IsDeleted)
            .Select(g => new
            {
                HasChildGroups = _dbContext.GroupDetails
                    .Any(c => c.ParentGroupId == groupId && !c.IsDeleted),

                HasVaultEntries = _dbContext.VaultEntries
                    .Any(v => v.GroupId == groupId && !v.IsDeleted),

                HasVaultFiles = _dbContext.VaultFiles
                    .Any(v => v.GroupId == groupId && !v.IsDeleted)
            })
            .FirstOrDefaultAsync() ?? throw new InvalidOperationException(GroupValidationMessages.GROUP_NOT_FOUND);

        if (canDelete.HasChildGroups ||
            canDelete.HasVaultEntries ||
            canDelete.HasVaultFiles)
            throw new InvalidOperationException(GroupValidationMessages.DELETE_NOT_ALLOWED);

        var changes = await _dbContext.GroupDetails
            .Where(g => g.GroupId == groupId
                        && g.OwnerId == ownerId
                        && !g.IsDeleted)
            .ExecuteUpdateAsync(x => x
                .SetProperty(g => g.IsDeleted, true)
                .SetProperty(g => g.UpdatedDate, DateTime.UtcNow)
                .SetProperty(g => g.UpdatedBy, updatedBy)
                .SetProperty(g => g.DeletedDate, DateTime.UtcNow)
                .SetProperty(g => g.DeletedBy, updatedBy));

        return changes > 0;
    }

    public async Task<List<GroupDetails>> GetChildGroupDetails(long ownerId, long? parentGroupId, GroupType groupType)
    {
        var query = _dbContext.GroupDetails.AsNoTracking().
            Where(g => g.OwnerId == ownerId && !g.IsDeleted
            && g.ParentGroupId == parentGroupId && g.GroupType == groupType);

        return await query.ToListAsync();
    }

    public async Task<GroupDetails?> GetParentGroupDetails(long ownerId, long childGroupId)
    {
        var parentId = await _dbContext.GroupDetails.AsNoTracking().
            Where(g => g.OwnerId == ownerId && !g.IsDeleted
            && g.GroupId == childGroupId).Select(g => g.ParentGroupId).FirstOrDefaultAsync();

        if (parentId == null) return null;

        return await GetGroupDetailsById(parentId ?? 0);
    }

    public async Task<GroupDetails?> GetGroupDetailsById(long groupId)
    {
        return await _dbContext.GroupDetails.AsNoTracking()
            .Where(g => g.GroupId == groupId && !g.IsDeleted)
            .FirstOrDefaultAsync();
    }
}
