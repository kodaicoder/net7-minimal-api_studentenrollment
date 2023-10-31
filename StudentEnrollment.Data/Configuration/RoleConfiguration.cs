using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentEnrollment.Data.Configuration
{
	internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
	{
		public void Configure(EntityTypeBuilder<IdentityRole> builder)
		{
			builder.HasData(
				new IdentityRole()
				{
					///You can specific ID by yourself but for now just left it to the program to handle
					Id = "6793830E-04BE-4192-ACC5-30B470830E3C",
					Name = "Administrator",
					NormalizedName = "ADMINISTRATOR"
				},
				new IdentityRole()
				{
					///You can specific ID by yourself but for now just left it to the program to handle
					Id = "4AF63ED2-BE77-47C0-9FAC-4CE3752D4CE8",
					Name = "User",
					NormalizedName = "USER"
				}
			);
		}
	}
}
