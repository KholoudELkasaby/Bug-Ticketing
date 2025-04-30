using System;
using System.Collections.Generic;
using System.Linq;
namespace BugTrackingSystem.BL;

public class GeneralResult
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public List<ResultError>? Errors { get; set; } = null;
}
public class GeneralResult<T> : GeneralResult
{
    public T? Data { get; set; }

}
public class ResultError
{
    public string Message { get; set; } = string.Empty;
    public string? Code { get; set; } = null;
}
