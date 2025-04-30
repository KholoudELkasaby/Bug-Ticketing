using BugTrackingSystem.DAL;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.BL;

public class LoginDtoValidator : AbstractValidator<Login>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    public LoginDtoValidator(
        UserManager<User> userManager,
        SignInManager<User> signInManager
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .MustAsync(ValidateUserExists)
            .WithMessage("User dons't exist");
        RuleFor(x => x.Password)
             .NotEmpty()
             .WithMessage("Password is required.")
             .MinimumLength(6)
             .WithMessage("Password must be at least 6 characters long.")
             .MustAsync(async (model, password, cancellationToken) =>
             {
                 var user = await _userManager.FindByEmailAsync(model.Email);
                 if (user == null)
                 {
                     return false;
                 }

                 var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
                 return result.Succeeded;
             })
             .WithMessage("Invalid password.");

    }
    public async Task<bool> ValidateUserExists(string email, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user != null;
    }
}
