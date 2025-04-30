using BugTrackingSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.BL;

public class BugUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public BugStatus Status { get; set; }
    public BugPriority Priority { get; set; }
    public Guid ProjectId { get; set; }
}
