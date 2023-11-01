using FluentValidation;
using StudentEnrollment.Data.Interfaces;

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
	public class CreateEnrollmentDTOValidator : AbstractValidator<CreateEnrollmentDTO>
	{
		private readonly IServiceScopeFactory _scopeFactory;
		public CreateEnrollmentDTOValidator(IServiceScopeFactory scopeFactory)
		{
			this._scopeFactory = scopeFactory;

			RuleFor(x => x.CourseId)
			.NotEmpty()
			  .MustAsync(async (courseId, cancellation) =>
			  {
				  using (var scope = _scopeFactory.CreateScope())
				  {
					  var courseRepository = scope.ServiceProvider.GetRequiredService<ICourseRepository>();
					  return await courseRepository.IsExistAsync(courseId);
				  }
			  })
			.WithMessage("Course does not exist");


			RuleFor(x => x.StudentId)
			.NotEmpty()
			  .MustAsync(async (studentId, cancellation) =>
			  {
				  using (var scope = _scopeFactory.CreateScope())
				  {
					  var studentRepository = scope.ServiceProvider.GetRequiredService<IStudentRepository>();
					  return await studentRepository.IsExistAsync(studentId);
				  }
			  })
			.WithMessage("Student does not exist");

		}
	}
}
