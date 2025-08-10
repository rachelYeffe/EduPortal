using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduPortal.Dal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGraduateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Graduate",
                keyColumn: "HouseNumber",
                keyValue: null,
                column: "HouseNumber",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "HouseNumber",
                table: "Graduate",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HouseNumber",
                table: "Graduate",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
