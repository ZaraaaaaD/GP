using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Savior.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClinicAddressAndPhone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildingNumber",
                table: "Clinics");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Clinics",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Clinics",
                newName: "Address");

            migrationBuilder.AddColumn<int>(
                name: "ClinicID",
                table: "WeeklySchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClinicID",
                table: "DailySchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WeeklySchedules_ClinicID",
                table: "WeeklySchedules",
                column: "ClinicID");

            migrationBuilder.CreateIndex(
                name: "IX_DailySchedules_ClinicID",
                table: "DailySchedules",
                column: "ClinicID");

            migrationBuilder.AddForeignKey(
                name: "FK_DailySchedules_Clinics_ClinicID",
                table: "DailySchedules",
                column: "ClinicID",
                principalTable: "Clinics",
                principalColumn: "ClinicID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklySchedules_Clinics_ClinicID",
                table: "WeeklySchedules",
                column: "ClinicID",
                principalTable: "Clinics",
                principalColumn: "ClinicID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailySchedules_Clinics_ClinicID",
                table: "DailySchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_WeeklySchedules_Clinics_ClinicID",
                table: "WeeklySchedules");

            migrationBuilder.DropIndex(
                name: "IX_WeeklySchedules_ClinicID",
                table: "WeeklySchedules");

            migrationBuilder.DropIndex(
                name: "IX_DailySchedules_ClinicID",
                table: "DailySchedules");

            migrationBuilder.DropColumn(
                name: "ClinicID",
                table: "WeeklySchedules");

            migrationBuilder.DropColumn(
                name: "ClinicID",
                table: "DailySchedules");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Clinics",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Clinics",
                newName: "City");

            migrationBuilder.AddColumn<int>(
                name: "BuildingNumber",
                table: "Clinics",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
