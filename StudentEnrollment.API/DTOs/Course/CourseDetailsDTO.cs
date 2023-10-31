using StudentEnrollment.API.DTOs.Student;

namespace StudentEnrollment.API.DTOs.Course
{
	public class CourseDetailsDTO : CourseDTO
	{
		public List<StudentDTO> Students { get; set; } = new List<StudentDTO>();
	}
}
