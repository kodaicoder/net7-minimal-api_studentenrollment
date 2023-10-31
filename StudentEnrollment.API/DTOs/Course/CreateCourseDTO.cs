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
				.MaximumLength(100);

			RuleFor(x => x.Credits)
				.NotEmpty()
				.LessThan(100);
		}
	}
}
