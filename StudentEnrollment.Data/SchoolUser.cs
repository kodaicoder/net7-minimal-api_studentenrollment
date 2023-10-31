using Microsoft.AspNetCore.Identity;

namespace StudentEnrollment.Data
{
	public class SchoolUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName => $"{FirstName} {LastName}";
		public DateTime? DateOfBirth { get; set; }
		public int Age => DateTime.Now.Year - DateOfBirth?.Year ?? 0;

	}
}
