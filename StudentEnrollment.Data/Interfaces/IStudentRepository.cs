namespace StudentEnrollment.Data.Interfaces
{
	public interface IStudentRepository : IGenericRepository<Student>
	{
		Task<Student> GetAllCoursesByStudentId(int studentId);
	}
}
