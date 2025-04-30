using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BugTrackingSystem.BL;

public static class BLDependencyInjectionSetUp
{
    public static void AddBusinessLayer(this IServiceCollection services)
    {
        services.AddScoped<IProjectManager, ProjectManager>();
        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<IRoleManager, RoleManager>();
        services.AddScoped<IUserRoleManager, UserRoleManager>();
        services.AddScoped<IBugManager, BugManager>();
        services.AddScoped<IAttachmentManager, AttachmentManager>();
        services.AddScoped<IProjectMemberManager, ProjectMemberManager>();
        services.AddScoped<IBugAssignmentManager, BugAssignmentManager>();
        services.AddValidatorsFromAssembly(
            typeof(BLDependencyInjectionSetUp).Assembly
            );
    }
}
