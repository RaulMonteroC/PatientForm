using FluentValidation;
using PatientForm.Application.DTOs;

namespace PatientForm.Application.Validators;

public class UserCredentialValidator : AbstractValidator<UserCredentials>
{
    public UserCredentialValidator()
    {
        RuleFor(x => x.Username)
           .NotNull()
           .NotEmpty();

        RuleFor(x => x.Password)
           .NotNull()
           .NotEmpty();
    }
}