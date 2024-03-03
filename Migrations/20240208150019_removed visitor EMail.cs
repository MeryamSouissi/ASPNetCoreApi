using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZoneFranche.Migrations
{
    /// <inheritdoc />
    public partial class removedvisitorEMail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "Visiteurs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Visiteurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
