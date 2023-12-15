namespace BankApp.Utils;

public static class Constants
{
    public const string UserCreationApiEndpoint = "/create-user"; 
    public const string TransactionCreationApiEndpoint = "/transactions";
    public const string UserUpgradeApiEndPoint = "/upgrade-user";

    public const string SuccessMessage = "success";
    public const string FailureMessage = "failed";
    public const string UserCreationFailure = "Failed to create user";
    public const string UserCreationSuccess = "User created successfully";
    public const string TransactionCreationFailure = "Failed to create transaction";
    public const string TransactionCreationSuccess = "Transaction created successfully";
    public const string UserUpgradeFailure = "Failed to upgrade user";
    public const string UserUpgradeSuccess = "User upgraded successfully";
    public const string ValidationErrorsPrefix = "Validation Errors";

    public const int MinimumPasswordLength = 7;


    public const string ApiUrl = "http://ec2-44-202-225-54.compute-1.amazonaws.com:3000";

    public static class ValidationMessages
    {
        public const string EmailRequired = "Email is required";
        public const string FirstNameRequired = "First Name is required";
        public const string LastNameRequired = "Last Name is required";
        public const string PasswordRequired = "Password is required";
        public const string EmailInvalid = "A valid email is required.";
        public const string FirstNameLength = "First name must be between 2 and 50 characters";
        public const string LastNameLength = "Last name must be between 2 and 50 characters";
        public const string PasswordLength = "Password must be longer than or equal to 7 characters";

        public const string TransactionIdRequired = "Transaction Id is required";
        public const string TransactionIdLength = "Transaction Id must be at least 12 characters long";
        public const string AmountRequired = "Amount is required";
        public const string AmountRange = "Amount must be between 10 and 2000";
        public const string RecipientEmailRequired = "Recipient Email is required";
        public const string RecipientEmailInvalid = "Recipient Email is invalid";
        public const string RecipientFirstNameRequired = "Recipient First Name is required";
        public const string RecipientLastNameRequired = "Recipient Last Name is required";
    }
    public static class APIErrorMessages
    {
        public const string EmailNameMismatch = "The email and names of this recipient do not match with our external provider. Please try different name.";
        public const string InsufficentKYC = "Due to insufficient KYC, recipient cannot recieve any more transactions above 600";
        public const string FailedRetryAbly = "Failed please try again.";
    }

    public static class SystemDefaults
    {
        public static string DefaultRegion = "US";
        public static string DefaultLocale = "en_US";
    }

    public enum TransactionStatus
    {
        PAID,
        KYC_FAILURE,
        FAILED_RETRYABLY,
        DUPLICATE_TRANSACTION_ID,
        PENDING,
        UNKNOWN
    }
}


