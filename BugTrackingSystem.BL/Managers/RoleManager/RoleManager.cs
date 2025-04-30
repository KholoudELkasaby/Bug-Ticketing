using BugTrackingSystem.DAL;

namespace BugTrackingSystem.BL;

public class RoleManager : IRoleManager
{
    private readonly IUnitWork _unitWork;
    private readonly RoleAddDtoValidator _roleAddDtoValidator;
    private readonly RoleUpdateDtoValidator _roleUpdateDtoValidator;

    public RoleManager(
        IUnitWork unitWork,
        RoleAddDtoValidator roleAddDtoValidator,
        RoleUpdateDtoValidator roleUpdateDtoValidator)
    {
        _unitWork = unitWork;
        _roleAddDtoValidator = roleAddDtoValidator;
        _roleUpdateDtoValidator = roleUpdateDtoValidator;
    }

    public async Task<GeneralResult> AddRoleAsync(RoleAddDto roleAddDto)
    {
        var validationResult = await _roleAddDtoValidator.ValidateAsync(roleAddDto);
        if (!validationResult.IsValid)
        {
            return validationResult.MapErrorToGeneralResult();
        }

        var existingRole = await _unitWork.RoleRepository
            .GetFirstOrDefaultAsync(r => r.Name == roleAddDto.Name);

        if (existingRole != null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "This role already exists."
            };
        }

        var role = new Role
        {
            Name = roleAddDto.Name,
            Description = roleAddDto.Description ?? "NA"
        };

        await _unitWork.RoleRepository.AddAsync(role);
        await _unitWork.SaveChangesAsync();

        return new GeneralResult
        {
            Success = true,
            Message = "Role added successfully."
        };
    }

    public async Task<GeneralResult<List<RoleViewDto>>> GetAllRolesAsync()
    {
        var roles = await _unitWork.RoleRepository.GetAllAsync();

        var roleDtos = roles.Select(role => new RoleViewDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description
        }).ToList();

        return new GeneralResult<List<RoleViewDto>>
        {
            Success = true,
            Message = "Roles retrieved successfully.",
            Data = roleDtos
        };
    }

    public async Task<GeneralResult> UpdateRoleAsync(Guid id, RoleUpdateDto roleUpdateDto)
    {
        var validationResult = await _roleUpdateDtoValidator.ValidateAsync(roleUpdateDto);
        if (!validationResult.IsValid)
        {
            return validationResult.MapErrorToGeneralResult();
        }

        var role = await _unitWork.RoleRepository.GetByIdAsync(id);
        if (role == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Role not found.",
                Errors = new List<ResultError>
                {
                    new ResultError
                    {
                        Message = "Role not found.",
                        Code = "404"
                    }
                }
            };
        }
        role.Name = roleUpdateDto.Name;
        role.Description = roleUpdateDto.Description ?? role.Description;

         await  _unitWork.RoleRepository.UpdateAsync(role);
        await _unitWork.SaveChangesAsync();

        return new GeneralResult
        {
            Success = true,
            Message = "Role updated successfully."
        };
    }

    public async Task<GeneralResult> DeleteRoleAsync(Guid id)
    {
        var role = await _unitWork.RoleRepository.GetByIdAsync(id);
        if (role == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Role not found.",
                Errors = new List<ResultError>
                {
                    new ResultError
                    {
                        Message = "Role not found.",
                        Code = "404"
                    }
                }
            };
        }

        await _unitWork.RoleRepository.DeleteAsync(id);
        await _unitWork.SaveChangesAsync();

        return new GeneralResult
        {
            Success = true,
            Message = "Role deleted successfully."
        };
    }
    public async Task<GeneralResult<List<RoleViewDto>>> GetRoleByIdAsync(Guid id)
    {
        var role = await _unitWork.RoleRepository.GetByIdAsync(id);
        if (role == null)
        {
            return new GeneralResult<List<RoleViewDto>>
            {
                Success = false,
                Message = "Role not found.",
                Errors = new List<ResultError>
                {
                    new ResultError
                    {
                        Message = "Role not found.",
                        Code = "404"
                    }
                }
            };
        }
        var roleDto = new RoleViewDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description
        };
        return new GeneralResult<List<RoleViewDto>>
        {
            Success = true,
            Message = "Role retrieved successfully.",
            Data = new List<RoleViewDto> { roleDto }
        };

    }

    }

