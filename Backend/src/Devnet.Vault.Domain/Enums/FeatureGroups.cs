using System.ComponentModel;

namespace Devnet.Vault.Domain.Enums;

public enum FeatureGroups
{
    [Description("Password Storage")]
    Password = 0,

    [Description("File Storage")]
    Files = 1,

    [Description("Subscription Management")]
    Subscriptions = 2,

    [Description("Adminstraction")]
    Adminstraction = 3,
}
