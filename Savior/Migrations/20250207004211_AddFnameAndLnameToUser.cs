using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Savior.Migrations
{
    /// <inheritdoc />
    public partial class AddFnameAndLnameToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Lname");

            migrationBuilder.AddColumn<string>(
                name: "Fname",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fname",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Lname",
                table: "Users",
                newName: "Name");
        }
    }
}
