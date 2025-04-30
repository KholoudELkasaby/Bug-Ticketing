using BugTrackingSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.BL;

public interface IJwtTokenManager
{
    Task<string> GenerateJwtToken(User user);
    Task<IList<string>> GetUserRoles(User user);
}
