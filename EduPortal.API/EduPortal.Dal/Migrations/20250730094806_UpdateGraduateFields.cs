using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduPortal.Dal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGraduateFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Graduate");

            migrationBuilder.RenameColumn(
                name: "School",
                table: "Graduate",
                newName: "בAccountNumber");

            migrationBuilder.RenameColumn(
                name: "AccountNumber",
                table: "Graduate",
                newName: "cycle");

            migrationBuilder.AddColumn<string>(
                name: "AddFatherBusinessPhone",
                table: "Graduate",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AddHomePhone",
                table: "Graduate",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Age",
                table: "Graduate",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FatherApartment",
                table: "Graduate",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FatherBusinessPhone",
                table: "Graduate",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FatherCity",
                table: "Graduate",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FatherEntrance",
                table: "Graduate",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "FatherHouseNumber",
                table: "Graduate",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherPhone",
                table: "Graduate",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FatherStreet",
                table: "Graduate",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "HomePhone",
                table: "Graduate",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Kind",
                table: "Graduate",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "Graduate",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Graduate",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddFatherBusinessPhone",
                table: "Graduate");

            migrationBuilder.DropColumn(
                name: "AddHomePhone",
                table: "Graduate");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Graduate");

            migrationBuilder.DropColumn(
                name: "FatherApartment",
                table: "Graduate");

            migrationBuilder.DropColumn(
                name: "FatherBusinessPhone",
                table: "Graduate");

            migrationBuilder.DropColumn(
                name: "FatherCity",
                table: "Graduate");

            migrationBuilder.DropColumn(
                name: "FatherEntrance",
                table: "Graduate");

            migrationBuilder.DropColumn(
                name: "FatherHouseNumber",
                table: "Graduate");

            migrationBuilder.DropColumn(
                name: "FatherPhone",
                table: "Graduate");

            migrationBuilder.DropColumn(
                name: "FatherStreet",
                table: "Graduate");

            migrationBuilder.DropColumn(
                name: "HomePhone",
                table: "Graduate");

            migrationBuilder.DropColumn(
                name: "Kind",
                table: "Graduate");

            migrationBuilder.DropColumn(
                name: "Mail",
                table: "Graduate");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Graduate");

            migrationBuilder.RenameColumn(
                name: "בAccountNumber",
                table: "Graduate",
                newName: "School");

            migrationBuilder.RenameColumn(
                name: "cycle",
                table: "Graduate",
                newName: "AccountNumber");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Graduate",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
