using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.DAL
{
    public class Bug
    {
        public Guid BugId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public BugStatus Status { get; set; } = BugStatus.New;
        public BugPriority Priority { get; set; } = BugPriority.Low;

        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public ICollection<BugAssignment> BugAssignments { get; set; } = new HashSet<BugAssignment>();
        public ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();
    }
}
