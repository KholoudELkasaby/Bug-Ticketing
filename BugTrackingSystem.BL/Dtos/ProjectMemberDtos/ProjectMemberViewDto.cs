using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.BL;

public class ProjectMemberViewDto
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTime JoinedDate { get; set; } = DateTime.UtcNow;

}
