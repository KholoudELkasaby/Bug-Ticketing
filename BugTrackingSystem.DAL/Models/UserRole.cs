using Microsoft.AspNetCore.Identity;

namespace BugTrackingSystem.DAL
{
    public class UserRole: IdentityUserRole<Guid>
    {
        public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
        public virtual User User { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
        public override string ToString()
        {
            return $"{User} - {Role}";
        }
    }
}
