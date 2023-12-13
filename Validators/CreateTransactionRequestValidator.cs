using BankApp.Models;
using BankApp.Utils;
using FluentValidation;
using PhoneNumbers;

public class CreateTransactionRequestValidator : AbstractValidator<CreateTransactionRequest>
{
    public CreateTransactionRequestValidator()
    {
        RuleFor(x => x.TransactionId)
            .NotEmpty().WithMessage("Transaction ID is required.")
            .Length(10, 20).WithMessage("Transaction ID must be at least 10 characters and no more than 20 characters long.")
            .Matches(@"^[a-zA-Z0-9]*$").WithMessage("Transaction ID must contain only letters and numbers.");

        RuleFor(x => x.Amount)
            .InclusiveBetween(2, 1000).WithMessage("Amount must be between 2 and 1000, inclusive.")
            .PrecisionScale(6, 2, true).WithMessage("Amount must have a precision of at most 2 decimal places.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Login email is required.")
            .EmailAddress().WithMessage("A valid login email is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Login password is required.")
            .MinimumLength(Constants.MinimumPasswordLength).WithMessage($"Password must be longer than or equal to {Constants.MinimumPasswordLength} characters.");

        RuleFor(x => x.Recipient.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");

        RuleFor(x => x.Recipient.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters.");

        RuleFor(x => x.Recipient.Email)
            .NotEmpty().WithMessage("Recipient's email is required.")
            .EmailAddress().WithMessage("Recipient's email is not a valid email address.");

        RuleFor(x => x.Recipient.PhoneNumber)
            .Must(BeAValidPhoneNumber).When(x => !string.IsNullOrWhiteSpace(x.Recipient.PhoneNumber))
            .WithMessage("Recipient's phone number is not in a valid format");

        /*RuleFor(x => x.Recepient.Wallet)
            .GreaterThanOrEqualTo(0).When(x => x.Recepient.Wallet.HasValue)
            .WithMessage("Recepient's wallet amount cannot be negative.");
        */
    }

    private bool BeAValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        try
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var numberProto = phoneNumberUtil.Parse(phoneNumber, null); // Specify a region code if needed
            return phoneNumberUtil.IsValidNumber(numberProto);
        }
        catch (NumberParseException)
        {
            return false;
        }
    }
}