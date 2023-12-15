using BankApp.Utils;
using FluentValidation;

namespace BankApp.Validators;
public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(user => user.FirstName)
            .NotEmpty().WithMessage(Constants.ValidationMessages.FirstNameRequired)
            .Length(2, 50).WithMessage(Constants.ValidationMessages.FirstNameLength);

        RuleFor(user => user.LastName)
            .NotEmpty().WithMessage(Constants.ValidationMessages.LastNameRequired)
            .Length(2, 50).WithMessage(Constants.ValidationMessages.LastNameLength);

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage(Constants.ValidationMessages.EmailRequired)
            .EmailAddress().WithMessage(Constants.ValidationMessages.EmailInvalid);

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage(Constants.ValidationMessages.PasswordRequired)
            .MinimumLength(Constants.MinimumPasswordLength).WithMessage(Constants.ValidationMessages.PasswordLength);
    }
}
