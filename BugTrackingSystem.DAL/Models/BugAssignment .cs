using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.DAL
{
    public class BugAssignment
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid BugId { get; set; }
        public Bug Bug { get; set; } = null!;
        public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
    }
}
