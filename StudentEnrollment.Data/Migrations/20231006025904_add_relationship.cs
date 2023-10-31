using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentEnrollment.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65d06a2e-c6fe-4a09-aca1-591b7bc022a6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66e27e33-855b-4b2d-b566-4757e2e2ecf7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3315c3db-6ddc-47d7-8537-a362b78d365e", null, "User", "USER" },
                    { "65a44371-29a6-4788-be33-c562a589b51f", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 6, 9, 59, 4, 306, DateTimeKind.Local).AddTicks(3469), new DateTime(2023, 10, 6, 9, 59, 4, 306, DateTimeKind.Local).AddTicks(3489) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 6, 9, 59, 4, 306, DateTimeKind.Local).AddTicks(3493), new DateTime(2023, 10, 6, 9, 59, 4, 306, DateTimeKind.Local).AddTicks(3494) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3315c3db-6ddc-47d7-8537-a362b78d365e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65a44371-29a6-4788-be33-c562a589b51f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "65d06a2e-c6fe-4a09-aca1-591b7bc022a6", null, "User", "USER" },
                    { "66e27e33-855b-4b2d-b566-4757e2e2ecf7", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 9, 28, 20, 59, 50, 332, DateTimeKind.Local).AddTicks(3706), new DateTime(2023, 9, 28, 20, 59, 50, 332, DateTimeKind.Local).AddTicks(3725) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 9, 28, 20, 59, 50, 332, DateTimeKind.Local).AddTicks(3729), new DateTime(2023, 9, 28, 20, 59, 50, 332, DateTimeKind.Local).AddTicks(3730) });
        }
    }
}
