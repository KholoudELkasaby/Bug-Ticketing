using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.BL;

public interface IAuthManager
{
    Task<GeneralResult> Login(Login model);
    Task<GeneralResult> Register(Register model);
}
