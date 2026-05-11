namespace Devnet.Vault.Domain.Constants.Messages;

public static class ExceptionMessages
{
    public const string GENERIC_ERROR = "An error occurred while processing the request";
    public const string GENERIC_VALIDATION_ERROR = "Validation failed for the request";
    public const string JWT_SECRET_KEY_NOT_FOUND = "Issuer Key Not Found";
    public const string REDIS_CONNECTION_KEY_NOT_FOUND = "Redis connection string is not configured";
    public const string OPERATION_CANCELLED = "Operation Cancelled by client";
    public const string GENERIC_NULL_ERROR = "Unable to get request value";
}
