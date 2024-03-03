using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZoneFranche.Migrations
{
    /// <inheritdoc />
    public partial class LoginForEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idLogin",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_idLogin",
                table: "Employees",
                column: "idLogin");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Logins_idLogin",
                table: "Employees",
                column: "idLogin",
                principalTable: "Logins",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Logins_idLogin",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_idLogin",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "idLogin",
                table: "Employees");
        }
    }
}
