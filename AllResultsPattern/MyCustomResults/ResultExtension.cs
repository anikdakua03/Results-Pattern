using Microsoft.AspNetCore.Mvc;

namespace AllResultsPattern.MyCustomResults;

public static class ResultExtension
{
    public static IActionResult ToHttpResponse(this Result result)
    {
        if(result.IsSuccess)
        {
            return new OkObjectResult(result);
        }

        return MapErrorResponse(result);
    }

    public static IActionResult ToHttpResponse<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result);
        }

        return MapErrorResponse(result);
    }

    private static IActionResult MapErrorResponse(Result result)
    {
        return result.HasErrors
            ? new BadRequestObjectResult(result)
            : result.Error is null ?
            new BadRequestObjectResult(result)
            : result.Error.Code switch
            {
                ErrorTypesConstant.NONE => new BadRequestObjectResult(result),
                ErrorTypesConstant.NOT_FOUND => new NotFoundObjectResult(result),
                _ => new BadRequestObjectResult(result),
            };
    }
}
