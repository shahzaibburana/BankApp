using Newtonsoft.Json;

namespace BankApp.Models;
public class RecipientDetails
{
    [JsonProperty("firstname")]
    public string FirstName { get; set; }
    [JsonProperty("lastname")]
    public string LastName { get; set; }
    [JsonProperty("email")]
    public string Email { get; set; }
    [JsonProperty("phone")]
    public string PhoneNumber { get; set; } = string.Empty;
    [JsonProperty("nickname")]
    public string NickName { get; set; } = string.Empty;
    [JsonProperty("wallet")]
    public string Wallet { get; set; } = string.Empty;
}

