using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentEnrollment.Data.Configuration
{
	internal class CourseConfiguration : IEntityTypeConfiguration<Course>
	{
		public void Configure(EntityTypeBuilder<Course> builder)
		{
			builder.HasData(
				new Course()
				{
					Id = 1,
					Title = "Course number 1",
					Credits = 3
				},
				new Course()
				{
					Id = 2,
					Title = "Course number 2",
					Credits = 5
				}
			);
		}
	}
}
