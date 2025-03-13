using ApiMinimalEntityFramework.DTOs;
using FluentValidation;

namespace ApiMinimalEntityFramework.Validations
{
    public class CreateGenreDTOValidator: AbstractValidator<CreateGenreDTO>
    {
        public CreateGenreDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .MaximumLength(50).WithMessage("The field {PropertyName} should be less than {MaxLength} caracteres");
        }
    }
}
