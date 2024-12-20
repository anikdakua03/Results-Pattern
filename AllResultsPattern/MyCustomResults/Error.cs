﻿namespace AllResultsPattern.MyCustomResults;

public class Error
{
    public string Code { get; }
    public string Description { get; }
    public ErrorType ErrorType { get; }
    // public Dictionary<string, List<ValidationError>>? Metadata { get; }
    public Dictionary<string, object>? Metadata { get; }

    // private Error(string code, string description, ErrorType type, Dictionary<string, List<ValidationError>>? metadata)
    private Error(string code, string description, ErrorType type, Dictionary<string, object>? metadata)
    {
        Code = code;
        Description = description;
        ErrorType = type;
        //NumericType = (int)type;
        Metadata = metadata;
    }

    public static Error None()
    {
        return new(ErrorTypesConstant.NONE, string.Empty, ErrorType.None, []);
    }

    public static Error Failure(string code = ErrorTypesConstant.BAD_REQUEST, string description = "A failure has occurred.", Dictionary<string, object>? metadata = null)
    {
        return new Error(code, description, ErrorType.Failure, metadata);
    }

    public static Error Unexpected(string code = ErrorTypesConstant.BAD_REQUEST, string description = "An unexpected error has occurred.", Dictionary<string, object>? metadata = null)
    {
        return new Error(code, description, ErrorType.Unexpected, metadata);
    }

    public static Error Validation(string code = ErrorTypesConstant.FAILED, string description = "A validation error has occurred.", Dictionary<string, object>? metadata = null)
    {
        return new Error(code, description, ErrorType.Validation, metadata);
    }

    public static Error Conflict(string code = ErrorTypesConstant.CONFLICT, string description = "A conflict error has occurred.", Dictionary<string, object>? metadata = null)
    {
        return new Error(code, description, ErrorType.Conflict, metadata);
    }

    public static Error NotFound(string code = ErrorTypesConstant.NOT_FOUND, string description = "A 'Not Found' error has occurred.", Dictionary<string, object>? metadata = null)
    {
        return new Error(code, description, ErrorType.NotFound, metadata);
    }

    public static Error Unauthorized(string code = ErrorTypesConstant.UNAUTHORIZED, string description = "An 'Unauthorized' error has occurred.", Dictionary<string, object>? metadata = null)
    {
        return new Error(code, description, ErrorType.Unauthorized, metadata);
    }

    public static Error Forbidden(string code = ErrorTypesConstant.FORBIDDEN, string description = "A 'Forbidden' error has occurred.", Dictionary<string, object>? metadata = null)
    {
        return new Error(code, description, ErrorType.Forbidden, metadata);
    }

    public static Error CustomError(string code, string description, ErrorType errorType, Dictionary<string, object>? metadata = null)
    {
        return new Error(code, description, errorType, metadata);
    }
}

public class ErrorTypesConstant
{
    public const string NONE = "None";
    public const string FAILED = "ValidationFailed";
    public const string CONFLICT = "Duplicate";
    public const string UNAUTHORIZED = "Unauthorized";
    public const string NOT_FOUND = "NotFound";
    public const string BAD_REQUEST = "Unexpected";
    public const string FORBIDDEN = "Forbidden";
}

public enum ErrorType
{
    None,
    Failure,
    Unexpected,
    Validation,
    Conflict,
    NotFound,
    Unauthorized,
    Forbidden
}