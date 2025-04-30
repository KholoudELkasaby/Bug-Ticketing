using BugTrackingSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.BL;

public class UserManager : IUserManager
{
    private readonly IUnitWork _unitWork;
    private readonly UserUpdateDtoValidator _userUpdateDtoValidator;
    public UserManager(IUnitWork unitWork,
        UserUpdateDtoValidator userUpdateDtoValidator)
    {
        _unitWork = unitWork;
        _userUpdateDtoValidator = userUpdateDtoValidator;
    }

    public async Task<GeneralResult> DeleteUserAsync(Guid id)
    {
        var user = await _unitWork.UserRepository.GetByIdAsync(id);
        if (user == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "User not found"
            };
        }
        await _unitWork.UserRepository.DeleteAsync(id);
        await _unitWork.SaveChangesAsync();
        return new GeneralResult
        {
            Success = true,
            Message = "User deleted successfully"
        };
    }

    public async Task<List<UserViewDto>> GetAllUsersAsync()
    {
        var usersFromDb = await _unitWork.UserRepository.GetUsersWithRolesInfo();
        var users = usersFromDb.Select(user => new UserViewDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.UserRoles
                .Select(ur => new RoleViewDto
                {
                    Id = ur.Role.Id,
                    Name = ur.Role.Name,
                    Description = ur.Role.Description
                }).ToList(),
            IsActive = user.IsActive
        }).ToList();
        return users;
    }

    public async Task<GeneralResult> GetUserAsync(Guid id)
    {
        var user = await _unitWork.UserRepository.GetUserByIdWithAllInfo(id);
        if (user == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "User not found"
            };
        }
        var userViewDto = new UserViewDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.UserRoles
                .Select(ur => new RoleViewDto
                {
                    Id = ur.Role.Id,
                    Name = ur.Role.Name,
                    Description = ur.Role.Description
                }).ToList(),
            IsActive = user.IsActive
        };
        return new GeneralResult<UserViewDto>
        {
            Success = true,
            Message = "User found",
            Data = userViewDto
        };
    }

    public async Task<GeneralResult> GetUsersWithRolesAndBugsInfo()
    {
        var users = await _unitWork.UserRepository
            .GetUsersWithRolesAndBugsInfo();
        if (users == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "No users found"
            };
        }
        var userViewDtos = users.Select(user => new UserViewRolesAndBugsDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.UserRoles
                .Select(ur => new RoleViewDto
                {
                    Id = ur.Role.Id,
                    Name = ur.Role.Name,
                    Description = ur.Role.Description
                }).ToList(),
            Bugs = user.BugAssignments.Select(ba => new BugViewForUserDto
            {
                Id = ba.Bug.BugId,
                Title = ba.Bug.Title,
                Description = ba.Bug.Description,
                Status = ba.Bug.Status,
                Priority = ba.Bug.Priority,
            }).ToList(),
            IsActive = user.IsActive
        }).ToList();
        return new GeneralResult<List<UserViewRolesAndBugsDto>>
        {
            Success = true,
            Message = "Users found",
            Data = userViewDtos
        };
    }

    public async Task<GeneralResult> UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto)
    {
        var validationResult = _userUpdateDtoValidator.ValidateAsync(userUpdateDto);
        if(!validationResult.Result.IsValid)
        {
            return validationResult.Result.MapErrorToGeneralResult();
        }

        var user = await _unitWork.UserRepository.GetByIdAsync(id);
        if (user == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "User not found"
            };
        }
        user.FirstName = userUpdateDto.FirstName;
        user.LastName = userUpdateDto.LastName;
        user.IsActive = userUpdateDto.IsActive;
        await _unitWork.UserRepository.UpdateAsync(user);
        await _unitWork.SaveChangesAsync();
        return new GeneralResult
        {
            Success = true,
            Message = "User updated successfully"
        };
    }
}
