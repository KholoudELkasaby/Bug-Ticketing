using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.BL;

public class ProjectUpdateDtoValidator : AbstractValidator<ProjectUpdateDto>
{
    public ProjectUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Project name is required.")
            .MinimumLength(2)
            .WithMessage("Project name must be at least 2 characters long.")
            .Matches(@"^[a-zA-Z0-9\s]+$")
            .WithMessage("Project name can only contain letters, numbers, and spaces.");
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Project description is required.")
            .MinimumLength(2)
            .WithMessage("Project description must be at least 2 characters long.");
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Project status is required and must be a valid enum value.");
        RuleFor(x => x.StartDate)
            .NotNull()
            .WithMessage("Project start date is required.")
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Project start date must be in the past or present.");
        RuleFor(x => x.EndDate)
            .NotNull()
            .WithMessage("Project end date is required.")
            .GreaterThanOrEqualTo(x => x.StartDate.Value)
            .When(x => x.StartDate.HasValue)
            .WithMessage("Project end date must be greater than or equal to the start date.");
    }
}
