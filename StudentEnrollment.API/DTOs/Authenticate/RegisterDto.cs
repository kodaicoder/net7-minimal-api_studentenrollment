using FluentValidation;

namespace StudentEnrollment.API.Endpoints;

public class RegisterDto : LoginDto
{

	public string Email { get; set; }
	public string ConfirmPassword { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public DateTime? DateOfBirth { get; set; }

}

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
	public RegisterDtoValidator()
	{
		//inclued a rule from login dto validator
		Include(new LoginDtoValidator());

		RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress();

		RuleFor(x => x.ConfirmPassword)
			.MinimumLength(6)
			.MaximumLength(20)
			.Equal(x => x.Password).WithMessage("Password confrim is mismatch with password");

		RuleFor(x => x.FirstName)
			.NotEmpty()
			.MaximumLength(50);

		RuleFor(x => x.LastName)
			.NotEmpty()
			.MaximumLength(50);

		RuleFor(x => x.DateOfBirth)
			.Must(dob =>
			{
				if (dob.HasValue)
				{
					return dob.Value < DateTime.Now;
				}
				return true;
			})
			.WithMessage("Date of birth must be less than today's date.");
	}
}



