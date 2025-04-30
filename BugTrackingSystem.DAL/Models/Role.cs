using Microsoft.AspNetCore.Identity;

namespace BugTrackingSystem.DAL
{
    public class Role : IdentityRole<Guid>
    {
        public string Description { get; set; } = string.Empty;
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}
