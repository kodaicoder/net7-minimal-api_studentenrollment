using FluentValidation;

namespace StudentEnrollment.API.DTOs.Course
{
	public class CourseDTO : CreateCourseDTO
	{
		public int Id { get; set; }

	}

	public class CourseDTOValidator : AbstractValidator<CourseDTO>
	{
		public CourseDTOValidator()
		{
			Include(new CreateCourseDTOValidator());
		}
	}
}
