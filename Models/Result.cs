namespace BankApp.Models;
public class Result
{
    public Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }
    public bool IsSuccess { get; set; }
    public string SuccessMessage { get; set; } = string.Empty;
    public bool IsFailure => !IsSuccess;
    public string APIErrorMessage { get; set; } = string.Empty;
    public List<string> RequestValidationErrors { get; set; } = new List<string>();
}

