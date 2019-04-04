using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactManager.Migrations
{
    public partial class FillContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "LastName", "Name", "MiddleName", "BirthDate", "Tag" },
                values: new object[] { "Petrov", "Igor", "Ivanovich", new DateTime(1990, 4, 10), "developer" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "LastName", "Name", "MiddleName", "BirthDate", "Tag" },
                values: new object[] { "Ivanova", "Svetlana", "Stepanovna", new DateTime(1996, 1, 21), "student" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "LastName", "Name", "MiddleName", "BirthDate", "Tag" },
                values: new object[] { "Smirnov", "Alexander", "Ivanovich", new DateTime(1987, 7, 3), "developer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
