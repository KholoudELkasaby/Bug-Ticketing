using BugTrackingSystem.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace BugTrackingSystem.BL;

public static class IdentityConfig
{
    public static IServiceCollection AddIdentityServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add Identity
        services.AddIdentity<User, Role>(ConfigureIdentityOptions)
            .AddEntityFrameworkStores<BugTrackingSystemContext>()
            .AddDefaultTokenProviders();

        // Configure JWT authentication
        ConfigureJwtAuthentication(services, configuration);

        return services;
    }

    private static void ConfigureIdentityOptions(IdentityOptions options)
    {
        // Password settings
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;

        // Lockout settings
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings
        options.User.RequireUniqueEmail = true;
        options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

        // SignIn settings
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
    }

    private static void ConfigureJwtAuthentication(
        IServiceCollection services,
        IConfiguration configuration)
    {
        // Configure JWT settings
        var jwtSettingsSection = configuration.GetSection("JwtSettings");
        services.Configure<JwtSettings>(jwtSettingsSection);
        services.AddSingleton(provider => provider.GetRequiredService<IOptions<JwtSettings>>().Value);
        var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
        var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                ClockSkew = TimeSpan.Zero
            };
        });
        services.AddScoped<IJwtTokenManager, JwtTokenManager>();
        services.AddScoped<IAuthManager, AuthManager>();
    }
    //Add Authorization policies
    public static void AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Constants.Policies.Admin, 
                policy => policy.RequireRole(Constants.Policies.Admin)
                .RequireClaim(ClaimTypes.NameIdentifier)
                );
            options.AddPolicy(Constants.Policies.Manager, 
                policy => policy.RequireRole(Constants.Policies.Manager)
                );
            options.AddPolicy(Constants.Policies.Developer, 
                policy => policy.RequireRole(Constants.Policies.Developer)
                );
            options.AddPolicy(Constants.Policies.Tester, 
                policy => policy.RequireRole(Constants.Policies.Tester)
                );
        });
    }
}
