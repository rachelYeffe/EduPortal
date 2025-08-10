using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduPortal.Dal.Migrations
{
    /// <inheritdoc />
    public partial class ChangeHouseNumberToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "בAccountNumber",
                table: "Graduate",
                newName: "AccountNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountNumber",
                table: "Graduate",
                newName: "בAccountNumber");
        }
    }
}
