using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.BL;

public class ProjectMemberRemoveDto
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
}
