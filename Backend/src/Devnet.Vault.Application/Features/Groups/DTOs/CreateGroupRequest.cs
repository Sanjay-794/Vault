using Devnet.Vault.Domain.Enums;

namespace Devnet.Vault.Application.Features.Groups.DTOs;

public class CreateGroupRequest
{
    public string Name { get; set; } = string.Empty;
    public long? ParentGroupId { get; set; }
    public bool IsFavourite { get; set; }
    public string? MetadataJson { get; set; }
    public GroupType GroupType { get; set; }
}

public class CreateGroupResponse
{
    public long GroupId { get; set; }
}
