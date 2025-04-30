using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.BL;

public static class ErrorMapping
{

    public static GeneralResult MapErrorToGeneralResult(
        this FluentValidation.Results.ValidationResult validationResult)
    {
        GeneralResult generalResult = new GeneralResult();
        generalResult.Success = false;
        generalResult.Message = "Validation failed";
        generalResult.Errors = new List<ResultError>();
        foreach (var error in validationResult.Errors)
        {
            generalResult.Errors.Add(new ResultError
            {
                Message = error.ErrorMessage,
                Code = error.ErrorCode
            });
        }
        return generalResult;
    }
}
