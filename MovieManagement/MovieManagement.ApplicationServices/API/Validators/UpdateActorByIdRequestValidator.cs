using FluentValidation;
using MovieManagement.ApplicationServices.API.Domain;

namespace MovieManagement.ApplicationServices.API.Validators;

public class UpdateActorByIdRequestValidator : AbstractValidator<UpdateActorByIdRequest>
{
    public UpdateActorByIdRequestValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("WRONG_VALUE_(CORRECT_MIN_1)");
        RuleFor(x => x.FirstName).Length(3, 50).WithMessage("WRONG_LENGTH_(CORRECT_BETWEEN_3_AND_50)");
        RuleFor(x => x.LastName).Length(3, 100).WithMessage("WRONG_LENGTH_(CORRECT_BETWEEN_3_AND_100)");
    }
}
