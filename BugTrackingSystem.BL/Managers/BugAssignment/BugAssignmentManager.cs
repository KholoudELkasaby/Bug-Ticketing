using BugTrackingSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.BL;

public class BugAssignmentManager : IBugAssignmentManager
{
    private readonly IUnitWork _unitWork;
    private readonly AddBugToUserDtoValidator _addBugToUserDtoValidatorValidator;
    private readonly RemoveBugFromUserValidator _reomveBugFromUserValidator;
    public BugAssignmentManager(IUnitWork unitWork,
        AddBugToUserDtoValidator addBugToUserDtoValidatorValidator,
        RemoveBugFromUserValidator reomveBugFromUserValidator
        )
    {
        _unitWork = unitWork;
        _addBugToUserDtoValidatorValidator = addBugToUserDtoValidatorValidator;
        _reomveBugFromUserValidator = reomveBugFromUserValidator;
    }
    public async Task<GeneralResult> AssignBugToUserAsync(Guid bugId, AssignUserRequestDto assignUserRequestDto)
    {
        var addBugToUserDto = new AddBugToUserDto
        {
            BugId = bugId,
            UserId = assignUserRequestDto.UserId,
            AssignedDate = assignUserRequestDto.AssignedDate
        };
        var validationResult = await _addBugToUserDtoValidatorValidator
            .ValidateAsync(addBugToUserDto);
        if (!validationResult.IsValid)
        {
            return validationResult.MapErrorToGeneralResult();
        }
        var bugAssignment = new BugAssignment
        {
            BugId = addBugToUserDto.BugId,
            UserId = addBugToUserDto.UserId,
            AssignedDate = DateTime.UtcNow
        };
        await _unitWork.BugAssignmentRepository
            .AssignBugToUserAsync(bugAssignment);
        await _unitWork.SaveChangesAsync();
        return new GeneralResult
        {
            Success = true,
            Message = "Bug assigned successfully."
        };
    }

    public async Task<GeneralResult> RemoveBugAssignmentAsync(Guid bugId, Guid userId)
    {
        var removeBugFromUser = new RemoveBugFromUserDto
        {
            BugId = bugId,
            UserId = userId
        };
        var validationResult = await _reomveBugFromUserValidator
            .ValidateAsync(removeBugFromUser);
        if (!validationResult.IsValid)
        {
            return validationResult.MapErrorToGeneralResult();
        }
        await _unitWork.BugAssignmentRepository
            .RemoveBugAssignmentAsync(removeBugFromUser.BugId, removeBugFromUser.UserId);
        await _unitWork.SaveChangesAsync();
        return new GeneralResult
        {
            Success = true,
            Message = "Bug assignment is removed successfully."
        };
    }
}
