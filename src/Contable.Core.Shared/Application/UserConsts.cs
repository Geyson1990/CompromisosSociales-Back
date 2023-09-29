namespace Contable.Application
{
    public class UserConsts
    {
        public const int MaxPhoneNumberLength = 24;
        public const string PersonIdType = "INT";
        public const string PersonType = "INT";
        public const string AlertResponsibleIdType = "INT";

        public const int NameMinLength = 0;
        public const int NameMaxLength = 256;
        public const string NameType = "VARCHAR(256)";

        public const int SurnameMinLength = 0;
        public const int SurnameMaxLength = 256;
        public const string SurnameType = "VARCHAR(256)";

        public const int Surname2MinLength = 0;
        public const int Surname2MaxLength = 256;
        public const string Surname2Type = "VARCHAR(256)";

        public const int DocumentMinLength = 8;
        public const int DocumentMaxLength = 8;
        public const string DocumentType = "VARCHAR(25)";

        public const int ProfilePictureMinLength = 64;
        public const int ProfilePictureMaxLength = 64;
        public const string ProfilePictureType = "VARCHAR(64)";

        public const string PasswordResetCodeType = "VARCHAR(32)";
        public const string PasswordResetTimeType = "DATETIME";
        public const string EmailConfirmationCodeType = "VARCHAR(32)";
        public const string EmailConfirmationTimeType = "DATETIME";

        public const int NotificationValidationMinutes = 30;
        public const int NewNotificationMinutes = 5;
    }
}
