using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduPortal.Dal.Migrations
{
    /// <inheritdoc />
    public partial class CreateGraduatesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cycle",
                table: "Graduate",
                newName: "Cycle");

            migrationBuilder.AddColumn<string>(
                name: "Institution",
                table: "Graduate",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Institution",
                table: "Graduate");

            migrationBuilder.RenameColumn(
                name: "Cycle",
                table: "Graduate",
                newName: "cycle");
        }
    }
}
