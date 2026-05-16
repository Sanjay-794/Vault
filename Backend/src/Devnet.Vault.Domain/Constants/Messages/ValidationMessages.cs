namespace Devnet.Vault.Domain.Constants.Messages;

public static class ValidationMessages
{
    public static class AuthValidationMessages
    {
        public const string IDENTIFIER_REQUIRED = "Identifier is required";
        public const string IDENTIFIER_INVALID = "Identifier must be a valid email address or phone number";
        public const string OTP_CACHE_KEY_REQUIRED = "OtpCacheKey is required";


        public const string REGISTRATION_FAILED = "Failed to register user";
        public const string LOGIN_DETAILS_NOT_FOUND = "Failed to get user login";
        public const string LOGIN_DETAILS_SAVE_FAILED = "Failed to save login details";

        public const string LOGIN_REVOKE_FAILED = "Failed to revoke login";

        public const string OTP_IDENTIFIER_REQUIRED = "Identifier and OTP are required";
        public const string OTP_REFRESH_TOKEN_REQUIRED = "Refresh token is required";
        public const string ACCOUNT_DEACTIVATED = "Account is deactivated";
        public const string ACCOUNT_DELETED = "Account is deleted";
    }

    public static class OtpValidationMessages
    {
        public const string CHANNEL_INVALID = "ChannelType must be either 'email' or 'SMS'";
        public const string SMS_UNDER_DEVELOPMENT = "SMS functionality is under development";
        public const string OTP_LENGTH_INVALID = "OTP must be 4–6 digits";
        public const string CHANNEL_VALUE_NULL = "No contact information found for otp";

        public const string OTP_INVALID = "Invalid or expired OTP";
        public const string OTP_LENGTH_ZERO = "OTP length must be greater than zero";


    }

    public static class UserInfoMessages
    {
        public const string USER_NOT_FOUND = "User not found";
        public const string REQUEST_USER_ID_INVALID = "Invalid user ID in token";


    }

    public static class ProfileMessages
    {
        public const string PROFILE_UPDATE_FAILED = "Failed to update your profile";
        public const string EMAIL_ALREADY_IN_USE = "Email is already in use";
        public const string PHONE_NUMBER_ALREADY_IN_USE = "Phone number is already in use";
        public const string INVALID_EMAIL_ADDRESS = "Email address is invalid";
        public const string INVALID_PHONE_NUMBER = "Phone number is invalid";
        public const string INVALID_COUNTRY_CODE = "Country code is invalid";
    }

    public static class GroupValidationMessages
    {
        public const string GROUP_NOT_FOUND = "Group not found";
        public const string GROUP_ALREADY_EXISTS = "Group already exists";
        public const string FAILED_GROUP_CREATION = "Failed to create group";
        public const string GROUP_DELETE_FAILED = "Failed to delete group";
        public const string GROUP_UPDATE_FAILED = "Failed to update group";
        public const string GROUP_NAME_REQUIRED = "Group name is required";

        public const string INVALID_GROUP_ID = "Invalid group id";
        public const string INVALID_GROUP_PARENT_ID = "Invalid group parent id";
        public const string METADATA_REQUIRED = "Group metadata is required";
        public const string INVALID_METDATA_JSON = "Group data is not in valid json format";
    }


}
