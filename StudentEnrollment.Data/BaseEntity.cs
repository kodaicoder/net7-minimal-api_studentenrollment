namespace StudentEnrollment.Data
{
	public abstract class BaseEntity
	{
		public int Id { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public string CreateBy { get; set; } = new string("SYSTEM");
		public DateTime UpdatedDate { get; set; } = DateTime.Now;
		public string UpdateBy { get; set; } = new string("SYSTEM");
	}

}