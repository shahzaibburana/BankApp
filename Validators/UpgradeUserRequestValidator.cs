using BankApp.Models;
using BankApp.Utils;
using FluentValidation;

namespace BankApp.Validators;
public class UpgradeUserRequestValidator : AbstractValidator<UpgradeUserRequest>
{

    public UpgradeUserRequestValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");

        RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(Constants.MinimumPasswordLength).WithMessage($"Password must be longer than or equal to {Constants.MinimumPasswordLength} characters.");
    }
}

