using StudentEnrollment.API.Services.Interfaces;

namespace StudentEnrollment.API.Services.Repositories
{
	public class FileUploadRepository : IFileUploadRepository
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public FileUploadRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
		{
			this._webHostEnvironment = webHostEnvironment;
			this._httpContextAccessor = httpContextAccessor;
		}

		public string UploadStudentFile(byte[] file, string imageName)
		{
			if (file == null)
			{
				return string.Empty;
			}
			string folderPath = "studentImages";
			string url = _httpContextAccessor.HttpContext?.Request.Host.Value;
			string extenstion = Path.GetExtension(imageName);
			string fileName = $"{Guid.NewGuid()}{extenstion}";

			string filePath = Path.Combine(_webHostEnvironment.WebRootPath, folderPath, fileName);
			UploadImage(file, filePath);
			return $"https://{url}/{folderPath}/{fileName}";
		}

		private void UploadImage(byte[] fileBytes, string filePath)
		{
			FileInfo file = new FileInfo(filePath);
			file?.Directory?.Create();

			var fileStream = file?.Create();
			fileStream?.Write(fileBytes, 0, fileBytes.Length);
			fileStream?.Close();
		}
	}
}
