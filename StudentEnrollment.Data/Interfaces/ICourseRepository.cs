namespace StudentEnrollment.Data.Interfaces
{
	public interface ICourseRepository : IGenericRepository<Course>
	{
		Task<Course> GetAllStudentsByCourseId(int courseId);
	}
}
