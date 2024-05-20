using FluentValidation;
using MovieManagement.ApplicationServices.API.Domain;

namespace MovieManagement.ApplicationServices.API.Validators;

public class UpdateMyPasswordRequestValidator : AbstractValidator<UpdateMyPasswordRequest>
{
    public UpdateMyPasswordRequestValidator()
    {
        RuleFor(x => x.Password).Length(10, 32).WithMessage("WRONG_LENGTH_(CORRECT_BETWEEN_10_AND_32)");
    }
}
