using System.Text;
using BankApp.Models;
using BankApp.Utils;
using Newtonsoft.Json;
using BankApp.Validators;
using FluentValidation;

namespace BankApp;
public class TaptapBankClient
{
    private readonly HttpClient _httpClient;
    

    public TaptapBankClient(HttpClient httpClient, string baseUrl)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(baseUrl);
    }

    /// <summary>
    /// creates a new user.
    /// </summary>
    /// <param name="userRequest"></param>
    /// <returns></returns>
    public async Task<string> CreateUserAsync(CreateUserRequest userRequest)
    {
        // 1 - Validate the user object
        var createUserValidator = new CreateUserRequestValidator();
        string validationErrorMessage = await ValidateAsync(userRequest, createUserValidator);
        if (!string.IsNullOrEmpty(validationErrorMessage))
        {
            return $"{Constants.ValidationErrorsPrefix}:\n {validationErrorMessage}";
        }

        // 2 - convert the user object to xml
        var requestBodyXml = XmlConverter.SerializeToXml(userRequest);
        var content = new StringContent(requestBodyXml, Encoding.UTF8, "application/xml");

        // 3 - send the request to external api
        var response = await _httpClient.PostAsync(Constants.UserCreationApiEndpoint, content);

        // 4 - check if the request was successful and return error message if not
        if (!response.IsSuccessStatusCode)
        {
            var responseStr = await response.Content.ReadAsStringAsync();
            var errorMsg = HTMLParser.ExtractPreTagMessage(responseStr);
            return $"{Constants.UserCreationFailure}: {errorMsg}";
        }

        // 5 - return success message
        return Constants.UserCreationSuccess;
    }

    /// <summary>
    /// creates a new transaction
    /// </summary>
    /// <param name="transactionRequest"></param>
    /// <returns></returns>
    public async Task<string> CreateTransactionAsync(CreateTransactionRequest transactionRequest)
    {
        // 1 - Validate the transaction object
        var createTransactionValidator = new CreateTransactionRequestValidator();
        string validationErrorMessage = await ValidateAsync(transactionRequest, createTransactionValidator);
        if (!string.IsNullOrEmpty(validationErrorMessage))
        {
            return $"{Constants.ValidationErrorsPrefix}:\n {validationErrorMessage}";
        }

        // 2 - create the request body for transaction API
        var requestBody = JsonConvert.SerializeObject(transactionRequest);

        // 3 - prepare the PUT request for transaction API
        var request = new HttpRequestMessage(HttpMethod.Put, $"{Constants.TransactionCreationApiEndpoint}/{transactionRequest.TransactionId}")
        {
            Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
        };
   
        // 4 - add the authorization header
        var encryptedPassword = EncryptionHelper.CalculateMD5Hash(transactionRequest.Password);
        request.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{transactionRequest.Email}:{encryptedPassword}"))}");

        // 5 - send the request to external api
        var response = await _httpClient.SendAsync(request);

        // 6 - check if the request was successful and return error message if not
        if (!response.IsSuccessStatusCode)
        {
            var responseStr = await response.Content.ReadAsStringAsync();
            var errorMsg = HTMLParser.ExtractPreTagMessage(responseStr);
            return $"{Constants.TransactionCreationFailure}: {errorMsg}";
        }

        // 7 - return success message
        return Constants.TransactionCreationSuccess;
    }

    /// <summary>
    /// Upgrades an existing user's status to premium.
    /// </summary>
    /// <param name="upgradeUserRequest"></param>
    /// <returns></returns>
    public async Task<string> UpgradeUserAsync(UpgradeUserRequest upgradeUserRequest)
    {
        // 1 - Validate the upgrade user object
        var upgradeUserValidator = new UpgradeUserRequestValidator();
        string validationErrorMessage = await ValidateAsync(upgradeUserRequest, upgradeUserValidator);
        if (!string.IsNullOrEmpty(validationErrorMessage))
        {
            return $"{Constants.ValidationErrorsPrefix}:\n {validationErrorMessage}";
        }

        // 2 - prepare the POST request for upgrade user API
        var request = new HttpRequestMessage(HttpMethod.Post, $"{Constants.UserUpgradeApiEndPoint}");

        // 3 - add the authorization header
        var encryptedPassword = EncryptionHelper.CalculateMD5Hash(upgradeUserRequest.Password);
        request.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{upgradeUserRequest.Email}:{encryptedPassword}"))}");

        // 4 - send the request to external api
        var response = await _httpClient.SendAsync(request);

        // 5 - check if the request was successful and return error message if not
        if (!response.IsSuccessStatusCode)
        {
            var responseStr = await response.Content.ReadAsStringAsync();
            var errorMsg = HTMLParser.ExtractPreTagMessage(responseStr);
            return $"{Constants.UserUpgradeFailure}: {errorMsg}";
        }

        // 6 - return success message
        return Constants.UserUpgradeSuccess;
    }

    private async Task<string> ValidateAsync<T>(T instance, IValidator<T> validator)
    {
        var results = await validator.ValidateAsync(instance);

        if (!results.IsValid)
        {
            return string.Join(Environment.NewLine, results.Errors.Select(failure => failure.ErrorMessage));
        }

        return null;
    }
}

