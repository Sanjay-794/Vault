namespace Devnet.Vault.Application.Features.Groups.DTOs;


public class UpdateGroupNameRequest
{
    public long GroupId { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class UpdateGroupFavouriteRequest
{
    public long GroupId { get; set; }
    public bool IsFavourite { get; set; }
}

public class UpdateGroupParentRequest
{
    public long GroupId { get; set; }
    public long? ParentGroupId { get; set; }
}
public class UpdateGroupMetaDataJsonRequest
{
    public long GroupId { get; set; }
    public string MetadataJson { get; set; } = string.Empty;
}

public class DeleteGroupRequest
{
    public long GroupId { get; set; }
}