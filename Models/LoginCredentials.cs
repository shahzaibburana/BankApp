using Newtonsoft.Json;
namespace BankApp.Models;
public class LoginCredentials
{
    [JsonIgnore]
    public string Email { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
}

