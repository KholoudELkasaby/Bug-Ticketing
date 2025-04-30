using BugTrackingSystem.DAL;
using FluentValidation;

namespace BugTrackingSystem.BL;

public class RegisterDtoValidator : AbstractValidator<Register>
{
    private readonly IUnitWork _unitWork;
    public RegisterDtoValidator(
        IUnitWork unitWork
        )
    {
        _unitWork = unitWork;
        //Rules
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .MinimumLength(2)
            .WithMessage("First name must be at least 2 characters long.")
            .Matches(@"^[a-zA-Z]+$")
            .WithMessage("First name must contain only letters.");
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MinimumLength(2)
            .WithMessage("Last name must be at least 2 characters long.")
            .Matches(@"^[a-zA-Z]+$")
            .WithMessage("Last name must contain only letters.");
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long.");
    }
}
