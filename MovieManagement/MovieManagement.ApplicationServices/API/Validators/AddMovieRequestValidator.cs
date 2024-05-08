using FluentValidation;
using MovieManagement.ApplicationServices.API.Domain;

namespace MovieManagement.ApplicationServices.API.Validators;

public class AddMovieRequestValidator : AbstractValidator<AddMovieRequest>
{
    public AddMovieRequestValidator()
    {
        RuleFor(x => x.Title).Length(3, 80).WithMessage("WRONG_LENGTH_(CORRET_BETWEEN_3_ AND_80)");
        RuleFor(x => x.Year).LessThanOrEqualTo(DateTime.Now.Year).WithMessage("WRONG_VALUE_(CORRECT_MAX CURRENT_YEAR)");
        RuleFor(x => x.Universe).Length(4, 50).WithMessage("WRONG_LENGTH_(CORRECT_BETWEEN_4_AND_50)");
        RuleFor(x => x.BoxOffice).GreaterThanOrEqualTo(0).WithMessage("WRONG_VALUE_(CORRECT_MIN_0)");
    }
}
