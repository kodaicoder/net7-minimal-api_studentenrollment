using FluentValidation;

namespace StudentEnrollment.API.DTOs.Course
{
	public class CreateCourseDTO
	{
		public string Title { get; set; }
		public int Credits { get; set; }

	}


	public class CreateCourseDTOValidator : AbstractValidator<CreateCourseDTO>
	{
		public CreateCourseDTOValidator()
		{
			RuleFor(x => x.Title)
				.NotEmpty()
				.MaximumLength(100)
				.WithMessage("Title must not great than 100 characters");

			RuleFor(x => x.Credits)
				.NotEmpty()
				.WithMessage("Credits must be between 1 and 100")
				.LessThan(100)
				.WithMessage("Credits must be between 1 and 100");
		}
	}
}
