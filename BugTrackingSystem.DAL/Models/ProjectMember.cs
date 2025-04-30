using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.DAL;
public class ProjectMember
{
    public Guid ProjectId { get; set; }

    public Project Project { get; set; } = null!;
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime JoinedDate { get; set; } = DateTime.UtcNow;
    public string Notes { get; set; } = string.Empty;

}
