using BugTrackingSystem.DAL;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace BugTrackingSystem.BL;

public class AuthManager : IAuthManager
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtTokenManager _jwtTokenService;
    private readonly RoleManager<Role> _roleManager;
    private readonly LoginDtoValidator _loginDtoValidator;
    private readonly RegisterDtoValidator _registerDtoValidator;

    public AuthManager(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IJwtTokenManager jwtTokenService,
        RoleManager<Role> roleManager,
        LoginDtoValidator loginDtoValidator,
        RegisterDtoValidator registerDtoValidator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
        _roleManager = roleManager;
        _loginDtoValidator = loginDtoValidator;
        _registerDtoValidator = registerDtoValidator;
    }

    public async Task<GeneralResult> Login(Login model)
    {
        var validationResult = await _loginDtoValidator.ValidateAsync(model);
        
        if (!validationResult.IsValid)
        {
            return validationResult.MapErrorToGeneralResult();
        }
        var user = await _userManager.FindByEmailAsync(model.Email);
        var token = await _jwtTokenService.GenerateJwtToken(user);
        var roles = await _jwtTokenService.GetUserRoles(user);

        return new GeneralResult<AuthResponse>
        {
            Success = true,
            Message = "Login successful",
            Data = new AuthResponse
            {
                Token = token,
                UserId = user.Id.ToString(),
                Email = user.Email ?? "NA",
                Expiration = DateTime.Now.AddMinutes(120),
                Roles = roles.ToList()
            }
        };
    }

    public async Task<GeneralResult> Register(Register model)
    {
        var validationResult = await _registerDtoValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return validationResult.MapErrorToGeneralResult();
        }

        var user = new User
        {
            Email = model.Email,
            UserName = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        
        if (!result.Succeeded)
        {
            validationResult.Errors.AddRange(
                result.Errors.Select(
                    e => new ValidationFailure("Registration", e.Description)
                    )
                );
            return validationResult.MapErrorToGeneralResult();
        }
        var token = await _jwtTokenService.GenerateJwtToken(user);
        var roles = await _jwtTokenService.GetUserRoles(user);

        return new GeneralResult<AuthResponse>
        {
            Success = true,
            Message = "Registration successful",
            Data = new AuthResponse
            {
                Token = token,
                UserId = user.Id.ToString(),
                Email = user.Email ?? "NA",
                Expiration = DateTime.Now.AddMinutes(120),
                Roles = roles.ToList()
            }
        };
    }
}