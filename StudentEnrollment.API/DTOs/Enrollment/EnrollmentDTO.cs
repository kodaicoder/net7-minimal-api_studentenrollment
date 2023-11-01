using FluentValidation;
using StudentEnrollment.API.DTOs.Course;
using StudentEnrollment.API.DTOs.Student;

namespace StudentEnrollment.API.DTOs.Enrollment
{
	public class EnrollmentDTO : CreateEnrollmentDTO
	{
		public int Id { get; set; }
		public virtual CourseDTO Course { get; set; }
		public virtual StudentDTO Student { get; set; }
	}

	public class EnrollmentDTOValidator : AbstractValidator<EnrollmentDTO>
	{
		public EnrollmentDTOValidator(IServiceScopeFactory scopeFactory)
		{
			Include(new CreateEnrollmentDTOValidator(scopeFactory));
		}
	}
}
