namespace Devnet.Vault.Domain.Common;

public class AuditProperty
{
    public long? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public long? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public long? DeletedBy { get; set; }
}
