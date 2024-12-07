using FluentValidation;

namespace AllResultsPattern.Validations;

public class SampleRequestValidation : AbstractValidator<SampleUserRequest>
{
	public SampleRequestValidation()
	{
        RuleFor(user => user.Name)
        .NotEmpty().WithMessage("Name is required.")
        .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(user => user.Age)
            .InclusiveBetween(18, 60).WithMessage("Age must be between 18 and 60.");
    }
}
