namespace Devnet.Vault.Domain.Constants.Routes;

public static class ApiEndpoints
{
    public static class AuthApiEndpoints
    {
        public const string REQUEST_AUTH_OTP_ENDPOINT = "request-auth-otp";
        public const string LOGIN_WITH_OTP_ENDPOINT = "login-with-otp";
        public const string REFRESH_TOKEN_ENDPOINT = "refresh-token";
        public const string LOGOUT_ENDPOINT = "logout";
    }

    public static class AccountApiEndpoints
    {
        public const string USER_PROFILE_ENDPOINT = "get-profile";
        public const string USER_PROFILE_UPDATE_ENDPOINT = "update-profile";
        public const string REQUEST_UPDATE_EMAIL_OTP_ENDPOINT = "update-email/request-otp";
        public const string UPDATE_EMAIL_ENDPOINT = "update-email";
        public const string REQUEST_UPDATE_PHONE_OTP_ENDPOINT = "update-phone/request-otp";
        public const string UPDATE_PHONE_ENDPOINT = "update-phone";
        public const string DEACTIVATE_OTP_REQUEST_ENDPOINT = "deactivate-account/request-otp";
        public const string DEACTIVATE_ACCOUNT_ENDPOINT = "deactivate-account";
        public const string DELETE_ACCOUNT_OTP_REQUEST_ENDPOINT = "delete-account/request-otp";
        public const string DELETE_ACCOUNT_ENDPOINT = "delete-account";
    }

    public static class UploadApiEndpoints
    {
        public const string UPLOAD_FILES_ENDPOINT = "upload-files";
        public const string UPLOAD_STATUS_ENDPOINT = "upload-status";
        public const string UPLOAD_STATUS_BATCH_ENDPOINT = "upload-status/batch";
    }

    public static class GroupApiEndpoints
    {
        public const string CREATE = "create";
        public const string UPDATE_NAME = "update-name";
        public const string UPDATE_FAVOURITE = "update-favourite";
        public const string UPDATE_PARENT = "update-parent";
        public const string DELETE_GROUP = "delete-group";
        public const string CHILDREN = "get-children";
        public const string PARENT = "get-parent";
        public const string GET_BY_ID = "get-group-details-by-id";
    }
}