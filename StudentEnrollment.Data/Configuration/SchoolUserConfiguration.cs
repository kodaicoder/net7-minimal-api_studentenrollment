using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentEnrollment.Data.Configuration
{
	internal class SchoolUserConfiguration : IEntityTypeConfiguration<SchoolUser>
	{
		public void Configure(EntityTypeBuilder<SchoolUser> builder)
		{
			var hasher = new PasswordHasher<SchoolUser>();
			builder.HasData(
				new SchoolUser
				{
					Id = "79E63EC9-D33F-461C-A218-F536E553CF45",
					Email = "schooladmin@localhost.com",
					NormalizedEmail = "SCHOOLADMIN@LOCALHOST.COM",
					UserName = "schoolAdmin",
					NormalizedUserName = "SCHOOLADMIN",
					FirstName = "Nutchapon",
					LastName = "Makelai",
					PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
					EmailConfirmed = true,
				},
				new SchoolUser
				{
					Id = "78421E0E-6993-4C24-A5D6-D27E3CB7DC0B",
					Email = "schooluser@localhost.com",
					NormalizedEmail = "SCHOOLUSER@LOCALHOST.COM",
					UserName = "schoolUser",
					NormalizedUserName = "SCHOOLUSER",
					FirstName = "Jitaree",
					LastName = "Buarian",
					PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
					EmailConfirmed = true,
				}
			);
		}
	}
}
