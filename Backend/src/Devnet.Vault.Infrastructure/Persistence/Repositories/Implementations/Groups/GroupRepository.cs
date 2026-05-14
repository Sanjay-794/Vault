using Devnet.Vault.Domain.Entities.Groups;
using Devnet.Vault.Domain.Enums;
using Devnet.Vault.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Devnet.Vault.Infrastructure.Persistence.Repositories.Implementations.Groups;

public class GroupRepository(AppDbContext _dbContext)
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
            .Where(g => g.IsDeleted).OrderBy(x => x.GroupId).FirstOrDefaultAsync();
        if (existingGroup != null)
        {
            existingGroup.Name = group.Name;
            existingGroup.ParentGroupId = group.ParentGroupId;
            existingGroup.OwnerId = group.OwnerId;
            existingGroup.IsFavourite = group.IsFavourite;
            existingGroup.MetadataJson = group.MetadataJson;
            existingGroup.GroupType = group.GroupType;
            existingGroup.IsDeleted = false;
            existingGroup.UpdatedDate = DateTime.UtcNow;
            existingGroup.UpdatedBy = group.CreatedBy;
            var changes = await _dbContext.SaveChangesAsync();

            if (changes > 0)
                return existingGroup;
        }
        return null;
    }

    public async Task<bool> UpdateGroupName(string groupName, long groupId, long updatedBy)
    {
        var changes = await _dbContext.GroupDetails.Where(g => g.GroupId == groupId && !g.IsDeleted)
               .ExecuteUpdateAsync(X => X.SetProperty(g => g.Name, groupName)
               .SetProperty(g => g.UpdatedDate, DateTime.UtcNow)
               .SetProperty(g => g.UpdatedBy, updatedBy)
               );
        return changes > 0;
    }

    public async Task<bool> UpdateGroupFavouriteStatus(bool isFavourite, long groupId, long updatedBy)
    {
        var changes = await _dbContext.GroupDetails.Where(g => g.GroupId == groupId && !g.IsDeleted)
               .ExecuteUpdateAsync(X => X.SetProperty(g => g.IsFavourite, isFavourite)
               .SetProperty(g => g.UpdatedDate, DateTime.UtcNow)
               .SetProperty(g => g.UpdatedBy, updatedBy)
               );
        return changes > 0;
    }

    public async Task<bool> UpdateGroupParent(long parentGroupId, long groupId, long updatedBy)
    {
        var changes = await _dbContext.GroupDetails.Where(g => g.GroupId == groupId && !g.IsDeleted)
               .ExecuteUpdateAsync(X => X.SetProperty(g => g.ParentGroupId, parentGroupId)
               .SetProperty(g => g.UpdatedDate, DateTime.UtcNow)
               .SetProperty(g => g.UpdatedBy, updatedBy)
               );
        return changes > 0;
    }

    public async Task<bool> DeleteGroup(long groupId, long updatedBy)
    {
        // Only allow deletion if there are no vault items or child groups inside this group
        var changes = await _dbContext.GroupDetails.Where(g => g.GroupId == groupId && !g.IsDeleted
        && g.VaultEntries.Any() == false && g.VaultFiles.Any() == false && g.ChildGroups.Any() == false)
               .ExecuteUpdateAsync(X => X.SetProperty(g => g.IsDeleted, true)
               .SetProperty(g => g.UpdatedDate, DateTime.UtcNow)
               .SetProperty(g => g.UpdatedBy, updatedBy)
               );
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
