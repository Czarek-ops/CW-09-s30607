using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Przychodnia.Migrations
{
    /// <inheritdoc />
    public partial class Createalltables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Medicament",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Medicament",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Medicament",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Medicament",
                newName: "FirstName");
        }
    }
}
