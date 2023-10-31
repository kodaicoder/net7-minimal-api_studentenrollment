using FluentValidation;

namespace StudentEnrollment.API.DTOs.Student
{
	public class CreateStudentDTO
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string IdNumber { get; set; }
		public byte[] ProfilePicture { get; set; }
		public string OriginalPictureName { get; set; }
	}

	public class CreateStudentDTOValidator : AbstractValidator<CreateStudentDTO>
	{
		public CreateStudentDTOValidator()
		{
			RuleFor(x => x.FirstName)
				.NotEmpty()
				.MaximumLength(100);

			RuleFor(x => x.LastName)
				.NotEmpty()
				.MaximumLength(100);

			RuleFor(x => x.DateOfBirth)
				.NotEmpty();

			RuleFor(x => x.IdNumber)
				.NotEmpty();

			RuleFor(x => x.OriginalPictureName)
				.NotNull().When(x => x.ProfilePicture != null);
		}
	}
}
