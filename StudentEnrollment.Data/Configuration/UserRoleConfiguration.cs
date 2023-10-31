using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentEnrollment.Data.Configuration
{
	internal class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
		{
			builder.HasData(
				new IdentityUserRole<string>
				{
					///You can specific ID by yourself but for now just left it to the program to handle
					RoleId = "6793830E-04BE-4192-ACC5-30B470830E3C",
					UserId = "79E63EC9-D33F-461C-A218-F536E553CF45"
				},
				new IdentityUserRole<string>
				{
					///You can specific ID by yourself but for now just left it to the program to handle
					RoleId = "4AF63ED2-BE77-47C0-9FAC-4CE3752D4CE8",
					UserId = "78421E0E-6993-4C24-A5D6-D27E3CB7DC0B",
				}
			);
		}
	}
}
