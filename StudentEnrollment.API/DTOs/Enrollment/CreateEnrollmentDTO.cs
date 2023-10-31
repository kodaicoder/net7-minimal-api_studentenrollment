namespace StudentEnrollment.API.DTOs.Enrollment
{
	public class CreateEnrollmentDTO
	{
		public int CourseId { get; set; }
		public int StudentId { get; set; }

	}

	//public class CreateEnrollmentDTOValidator : AbstractValidator<CreateEnrollmentDTO>
	//{
	//	public readonly ICourseRepository _courseRepository;
	//	public readonly IStudentRepository _studentRepository;

	//	public CreateEnrollmentDTOValidator(ICourseRepository courseRepository, IStudentRepository studentRepository)
	//	{
	//		this._courseRepository = courseRepository;
	//		this._studentRepository = studentRepository;

	//		RuleFor(x => x.CourseId)
	//		.NotEmpty()
	//		.MustAsync(async (courseId, cancellation) => await _courseRepository.IsExistAsync(courseId))
	//		.WithMessage("Course does not exist");


	//		RuleFor(x => x.StudentId)
	//		.NotEmpty()
	//		.MustAsync(async (studentId, cancellation) => await _studentRepository.IsExistAsync(studentId))
	//		.WithMessage("Student does not exist");

	//	}
	//}
}
