namespace AllResultsPattern.MyCustomResults;

public static class SampleError
{
    public static Error SomeSamepleError => Error.Failure();

    public static Error SamepleNotFoundError => Error.NotFound();

    public static Error SamepleValidationError => Error.Validation();
}
