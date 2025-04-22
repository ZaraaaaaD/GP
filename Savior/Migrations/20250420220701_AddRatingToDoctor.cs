using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Savior.Migrations
{
    public partial class AddRatingToDoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // إضافة عمود Rating في جدول Doctors
            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Doctors",
                nullable: true);  // يمكنك تغيير nullable حسب احتياجك
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // إزالة عمود Rating في حالة التراجع عن الـ Migration
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Doctors");
        }
    }
}
