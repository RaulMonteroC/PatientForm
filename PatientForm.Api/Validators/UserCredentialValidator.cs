using FluentValidation;
using PatientForm.Application.DTOs;

namespace PatientForm.Api.Validators;

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