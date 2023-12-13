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
}

