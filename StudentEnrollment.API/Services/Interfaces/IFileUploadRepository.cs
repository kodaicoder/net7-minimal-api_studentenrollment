namespace StudentEnrollment.API.Services.Interfaces
{
	public interface IFileUploadRepository
	{
		string UploadStudentFile(byte[] file, string imageName);
	}
}
