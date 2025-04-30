using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.BL;

public class BugAssignmentViewDto
{
    public Guid BugId { get; set; }
    public Guid UserId { get; set; }
    public DateTime AssignedDate { get; set; }
  
}
