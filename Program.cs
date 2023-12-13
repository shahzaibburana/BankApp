using BankApp;
using BankApp.Models;
using BankApp.Utils;
using Bogus;

// 
var faker = new Faker();
string userFirstName = faker.Name.FirstName();
string userLastName = faker.Name.LastName();
string userEmail = faker.Internet.Email();
string userPassword = faker.Internet.Password(10); ;

var httpClient = new HttpClient();

var client = new TaptapBankClient(httpClient, "http://ec2-44-202-225-54.compute-1.amazonaws.com:3000");

// Create a new user
Console.WriteLine($"Creating User: {userFirstName} {userLastName} {userEmail}");

var createUserResult = await client.CreateUserAsync(new CreateUserRequest { FirstName = userFirstName, LastName = userLastName, Email = userEmail, Password = userPassword });

Console.WriteLine(createUserResult);

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
        int currentTrasactionAmount = totalBalance < kycLimit ? intialDepositLimit : ((totalAccountLimit - totalBalance) < subsequentDepositLimit ? (totalAccountLimit - totalBalance)  : subsequentDepositLimit);
        string transactionId = faker.Random.AlphaNumeric(12);
        var createTransactionResult = await client.CreateTransactionAsync(new CreateTransactionRequest { TransactionId = transactionId, Amount = currentTrasactionAmount, Recipient = new RecipientDetails { Email = recipientEmail, FirstName = recipientFirstName, LastName = recipientLastName }, Email = userEmail, Password = userPassword });
        totalBalance+= currentTrasactionAmount;

        if(createTransactionResult.StartsWith(Constants.TransactionCreationFailure))
        {
            Console.WriteLine($"{createTransactionResult} Recipient: {recipientFirstName} {recipientLastName} {recipientEmail}");
            // to avoid infinite loop, try max 50 recipients
            if(totalRecipients <= 50)
                totalRecipients++;
            break;
        }
            
        Console.WriteLine($"{createTransactionResult} Current Transaction: {currentTrasactionAmount} Total Balance:{totalBalance}");
    }
    while (totalBalance < totalAccountLimit);
    recipientCounter++;
}
while (recipientCounter <= totalRecipients);

//// Upgrade the user
var upgradeUserResult = await client.UpgradeUserAsync(new UpgradeUserRequest() { Email = userEmail, Password = userPassword });

Console.WriteLine(upgradeUserResult);
