
using FluentValidation;

namespace BugTrackingSystem.BL;

public class RoleAddDtoValidator : AbstractValidator<RoleAddDto>
{
    public RoleAddDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Role name is required.")
            .Must(name => new[] { "Manager", "Admin", "Developer", "Tester" }.Contains(name))
            .WithMessage("Role name must be one of the following: Manager, Admin, Developer, Tester.");

        RuleFor(x => x.Description)
            .MaximumLength(200)
            .WithMessage("Description cannot exceed 200 characters.");
    
}
}

