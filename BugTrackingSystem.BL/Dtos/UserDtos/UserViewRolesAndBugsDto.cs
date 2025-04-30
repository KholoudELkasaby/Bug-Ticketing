using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.BL;

public class UserViewRolesAndBugsDto
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    public List<RoleViewDto>? Roles { get; set; }
    public List<BugViewForUserDto>? Bugs { get; set; }
}
