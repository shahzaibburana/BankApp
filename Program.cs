using BankApp;
using BankApp.Models;
using BankApp.Utils;
using Bogus;

// http client to make api calls
var httpClient = new HttpClient();
var client = new TaptapBankClient(httpClient, Constants.ApiUrl);

// faker to generate random data
var faker = new Faker();
string userFirstName = faker.Name.FirstName();
string userLastName = faker.Name.LastName();
string userEmail = faker.Internet.Email();
string userPassword = faker.Internet.Password(10); ;


// Create a new user
await CreateNewUser();

// Send money to recipients 
await SendMoneyToRecipients();

// Upgrade the user
await UpgradeUser();

async Task CreateNewUser()
{
    Console.WriteLine($"Creating User: {userFirstName} {userLastName} {userEmail}");

    var createUserResult = await client.CreateUserAsync(new CreateUserRequest { FirstName = userFirstName, LastName = userLastName, Email = userEmail, Password = userPassword });

    await PrintResultOnConsole("User Creation",createUserResult);
}

async Task SendMoneyToRecipients()
{
    int recipientCounter = 1;
    int totalRecipients = 5;
    do
    {
        string recipientEmail = faker.Internet.Email();
        string recipientFirstName = faker.Name.FirstName();
        string recipientLastName = faker.Name.LastName();

        // Create a new transaction
        Console.WriteLine($"Try sending max 10K to Recipient :{recipientFirstName} {recipientLastName} {recipientEmail}");
        int totalBalance = 0;
        int totalAccountLimit = 10000;
        int kycLimit = 2000;
        int intialDepositLimit = 1000;
        int subsequentDepositLimit = 600;

        do
        {
            // if current balance is less than kyc limit, then deposit intial deposit limit (1000), else deposit subsequent deposit limit (600)
            int currentTrasactionAmount = totalBalance < kycLimit ? intialDepositLimit : ((totalAccountLimit - totalBalance) < subsequentDepositLimit ? (totalAccountLimit - totalBalance) : subsequentDepositLimit);
            string transactionId = faker.Random.AlphaNumeric(12);
            // create a new transaction
            var createTransactionResult = await client.CreateTransactionAsync(new CreateTransactionRequest { TransactionId = transactionId, Amount = currentTrasactionAmount, Recipient = new RecipientDetails { Email = recipientEmail, FirstName = recipientFirstName, LastName = recipientLastName }, Email = userEmail, Password = userPassword });
         
            // if transaction is successful, add the current transaction amount to total balance
            await PrintResultOnConsole("Transaction Creation", createTransactionResult);
            if (createTransactionResult.IsSuccess)
            { 
                totalBalance += currentTrasactionAmount;
            }
            // error handling
            else
            {
                // if transaction fails due to email name mismatch, then increase total recipients to be tried by 1
                if (createTransactionResult.IsFailure && createTransactionResult.APIErrorMessage.Equals(Constants.APIErrorMessages.EmailNameMismatch))
                {
                    // to avoid infinite loop, try max 50 recipients
                    if (totalRecipients <= 50)
                        totalRecipients++;
                }  
                break;
            }
            
            Console.WriteLine($"Current Transaction: {currentTrasactionAmount} Total Balance:{totalBalance}");
        }
        while (totalBalance < totalAccountLimit);
        recipientCounter++;
    }
    while (recipientCounter <= totalRecipients);
}

async Task UpgradeUser()
{
    Console.WriteLine($"Upgrading User: {userEmail}");

    var upgradeUserResult = await client.UpgradeUserAsync(new UpgradeUserRequest { Email = userEmail, Password = userPassword });

    PrintResultOnConsole("User Upgrade", upgradeUserResult);
}


async Task PrintResultOnConsole(string action, Result result)
{
    if (result.IsSuccess)
    {
        Console.WriteLine(result.SuccessMessage);
    }
    else
    {
        Console.WriteLine($"{action} Failed");
        if (result.RequestValidationErrors.Count() > 0)
        {
            var validationErrors = string.Join(Environment.NewLine, result.RequestValidationErrors);
            Console.WriteLine($"Validation Error(s): {validationErrors}");
        }
        else if (!string.IsNullOrEmpty(result.APIErrorMessage))
        {
            Console.WriteLine($"API Error(s): {result.APIErrorMessage}");
        }
    }
}