using FluentValidation;

namespace StudentEnrollment.API.DTOs.Student
{
	public class StudentDTO : CreateStudentDTO
	{
		public int Id { get; set; }
	}

	public class StudentDTOValidator : AbstractValidator<StudentDTO>
	{
		public StudentDTOValidator()
		{
			Include(new CreateStudentDTOValidator());
		}
	}
}
