using Devnet.Vault.Domain.Common;

namespace Devnet.Vault.Domain.Entities.Masters;

public class Countries : AuditProperty
{
    public int CountryId { get; private set; }
    public string CountryName { get; private set; } = null!;
    public string CountryCode { get; private set; } = null!;
    public string CountryCallingCode { get; private set; } = null!;
    public bool IsActive { get; private set; }

}
