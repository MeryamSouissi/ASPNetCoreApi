using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZoneFranche.Migrations
{
    /// <inheritdoc />
    public partial class addedloginforvisiteur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idLogin",
                table: "Visiteurs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Visiteurs_idLogin",
                table: "Visiteurs",
                column: "idLogin");

            migrationBuilder.AddForeignKey(
                name: "FK_Visiteurs_Logins_idLogin",
                table: "Visiteurs",
                column: "idLogin",
                principalTable: "Logins",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visiteurs_Logins_idLogin",
                table: "Visiteurs");

            migrationBuilder.DropIndex(
                name: "IX_Visiteurs_idLogin",
                table: "Visiteurs");

            migrationBuilder.DropColumn(
                name: "idLogin",
                table: "Visiteurs");
        }
    }
}
