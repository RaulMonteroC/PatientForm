using FluentValidation;
using PatientForm.Application.DTOs;

namespace PatientForm.Application.Validators;

public class PatientValidator
{
    public class RestaurantDtoValidator : AbstractValidator<PatientDto>
    {
        public RestaurantDtoValidator()
        {
            RuleFor(x => x.Name)
               .NotNull()
               .NotEmpty()
               .MaximumLength(100);

            RuleFor(x => x.LastName)
               .NotNull()
               .NotEmpty()
               .MaximumLength(100);
            
            RuleFor(x => x.PhoneNumber)
               .Length(7, 15);

            RuleFor(x => x.Email)
               .EmailAddress()
               .MaximumLength(35);
        }
    }
}