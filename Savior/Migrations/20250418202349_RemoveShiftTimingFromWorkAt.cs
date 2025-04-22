using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Savior.Migrations
{
    /// <inheritdoc />
    public partial class RemoveShiftTimingFromWorkAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DailySchedules",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DailySchedules",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WeeklySchedules",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WeeklySchedules",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WorksAts",
                keyColumns: new[] { "ClinicID", "DoctorID" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "WorksAts",
                keyColumns: new[] { "ClinicID", "DoctorID" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "Clinics",
                keyColumn: "ClinicID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clinics",
                keyColumn: "ClinicID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorID",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "ShiftTiming",
                table: "WorksAts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "ShiftTiming",
                table: "WorksAts",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.InsertData(
                table: "Clinics",
                columns: new[] { "ClinicID", "Address", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "Cairo, Shubra, 10th Building", "Cairo Heart Clinic", "0123456789" },
                    { 2, "Alexandria, Corniche, 25th Building", "Alexandria Medical Center", "0987654321" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "DoctorID", "BookingPrice", "Description", "Email", "FirstName", "HospitalAddress", "HospitalName", "ImageUrl", "IsApproved", "LastName", "MedicalLicenseNumber", "Specialty" },
                values: new object[,]
                {
                    { 5, 300m, "", "ahmed@example.com", "Ahmed", "Cairo, Egypt", "Cairo Heart Hospital", "", true, "Ali", "MLN123", "Cardiology" },
                    { 6, 250m, "", "mohamed@example.com", "Mohamed", "Alexandria, Egypt", "Alexandria Medical Center", "", true, "Hassan", "MLN456", "Orthopedics" }
                });

            migrationBuilder.InsertData(
                table: "DailySchedules",
                columns: new[] { "ID", "ClinicID", "DayOfWeek", "DoctorID", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { 1, 1, "Monday", 5, new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0) },
                    { 2, 2, "Tuesday", 6, new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "WeeklySchedules",
                columns: new[] { "ID", "ClinicID", "DayOfWeek", "DoctorID" },
                values: new object[,]
                {
                    { 1, 1, "Monday", 5 },
                    { 2, 2, "Tuesday", 6 }
                });

            migrationBuilder.InsertData(
                table: "WorksAts",
                columns: new[] { "ClinicID", "DoctorID", "ShiftTiming" },
                values: new object[,]
                {
                    { 1, 5, new TimeOnly(0, 0, 0) },
                    { 2, 6, new TimeOnly(0, 0, 0) }
                });
        }
    }
}
