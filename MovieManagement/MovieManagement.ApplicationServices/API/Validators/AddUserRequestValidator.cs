using FluentValidation;
using MovieManagement.ApplicationServices.API.Domain;

namespace MovieManagement.ApplicationServices.API.Validators;

public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
{
    public AddUserRequestValidator()
    {
        RuleFor(x => x.FirstName).Length(1, 20).WithMessage("WRONG_LENGTH_(CORRECT BETWEEN_1_AND_20)");
        RuleFor(x => x.LastName).Length(1, 50).WithMessage("WRONG_LENGTH_(CORRECT BETWEEN_1_AND_50)");
        RuleFor(x => x.Username).Length(8, 15).WithMessage("WRONG_LENGTH_(CORRECT BETWEEN_8_AND_15)");
        RuleFor(x => x.Password).Length(10, 30).WithMessage("WRONG_LENGTH_(CORRECT BETWEEN_10_AND_30)");
    }
}
