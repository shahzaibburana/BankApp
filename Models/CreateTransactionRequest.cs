using Newtonsoft.Json;
namespace BankApp.Models;
public class CreateTransactionRequest : LoginCredentials
{
    [JsonIgnore]
    public string TransactionId { get; set; }
    [JsonProperty("amount")]
    public decimal Amount { get; set; }
    [JsonProperty("recipient")]
    public RecipientDetails Recipient { get; set; } 
}

