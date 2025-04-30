using BugTrackingSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.BL;

public static class UserManagerMappingFunctions
{
 
    public static User ToUser(this UserUpdateDto userUpdateDto, Guid id)
    {
        return new User
        {
            Id = id,
            FirstName = userUpdateDto.FirstName,
            LastName = userUpdateDto.LastName,
            IsActive = userUpdateDto.IsActive
        };
    }
}
