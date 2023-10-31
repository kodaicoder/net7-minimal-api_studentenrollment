using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentEnrollment.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_seed_user_and_role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26668208-3416-4b1a-82c2-6c7dc46a4cf8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f73324db-14f9-49b9-90aa-ae9e9ecb4292");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4AF63ED2-BE77-47C0-9FAC-4CE3752D4CE8", null, "User", "USER" },
                    { "6793830E-04BE-4192-ACC5-30B470830E3C", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "78421E0E-6993-4C24-A5D6-D27E3CB7DC0B", 0, "9b313582-10f8-469b-9768-14acc12ddbd7", null, "schooluser@localhost.com", true, "Jitaree", "Buarian", false, null, "SCHOOLUSER@LOCALHOST.COM", "SCHOOLUSER", "AQAAAAIAAYagAAAAEGrNqxJUwpro6nSCQA+lnpluofomYfwftRBAbAJ3E/8ZzTjn13M3LLPllO+2Tm7K6w==", null, false, "8fb80123-dd99-4e02-a64b-dfc16b6ca252", false, "schoolUser" },
                    { "79E63EC9-D33F-461C-A218-F536E553CF45", 0, "0b93410c-ea1d-4bef-88a8-cb90e3d2b941", null, "schooladmin@localhost.com", true, "Nutchapon", "Makelai", false, null, "SCHOOLADMIN@LOCALHOST.COM", "SCHOOLADMIN", "AQAAAAIAAYagAAAAEO8x0M5WgavgOO8OqB9hTXMcAq+EWtALslvJb5z+GcFR499tAd9WgqLKJgJQfYF6Dg==", null, false, "85970c6c-2403-4d57-9dc1-81debfcfb0df", false, "schoolAdmin" }
                });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 17, 11, 31, 5, 97, DateTimeKind.Local).AddTicks(2511), new DateTime(2023, 10, 17, 11, 31, 5, 97, DateTimeKind.Local).AddTicks(2529) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 17, 11, 31, 5, 97, DateTimeKind.Local).AddTicks(2533), new DateTime(2023, 10, 17, 11, 31, 5, 97, DateTimeKind.Local).AddTicks(2534) });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "4AF63ED2-BE77-47C0-9FAC-4CE3752D4CE8", "78421E0E-6993-4C24-A5D6-D27E3CB7DC0B" },
                    { "6793830E-04BE-4192-ACC5-30B470830E3C", "79E63EC9-D33F-461C-A218-F536E553CF45" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4AF63ED2-BE77-47C0-9FAC-4CE3752D4CE8", "78421E0E-6993-4C24-A5D6-D27E3CB7DC0B" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6793830E-04BE-4192-ACC5-30B470830E3C", "79E63EC9-D33F-461C-A218-F536E553CF45" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4AF63ED2-BE77-47C0-9FAC-4CE3752D4CE8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6793830E-04BE-4192-ACC5-30B470830E3C");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "78421E0E-6993-4C24-A5D6-D27E3CB7DC0B");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "79E63EC9-D33F-461C-A218-F536E553CF45");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "26668208-3416-4b1a-82c2-6c7dc46a4cf8", null, "User", "USER" },
                    { "f73324db-14f9-49b9-90aa-ae9e9ecb4292", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 17, 10, 51, 36, 540, DateTimeKind.Local).AddTicks(4186), new DateTime(2023, 10, 17, 10, 51, 36, 540, DateTimeKind.Local).AddTicks(4211) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 17, 10, 51, 36, 540, DateTimeKind.Local).AddTicks(4215), new DateTime(2023, 10, 17, 10, 51, 36, 540, DateTimeKind.Local).AddTicks(4215) });
        }
    }
}
