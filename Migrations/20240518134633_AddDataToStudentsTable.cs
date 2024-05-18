using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollegeProject.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToStudentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentID", "Address", "DOB", "Email", "StudentName" },
                values: new object[,]
                {
                    { 1, "India", new DateTime(2008, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "india@gmail.com", "Sample1" },
                    { 2, "USA", new DateTime(2012, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "usa@gmail.com", "Sample2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentID",
                keyValue: 2);
        }
    }
}
