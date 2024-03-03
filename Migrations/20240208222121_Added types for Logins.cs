using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZoneFranche.Migrations
{
    /// <inheritdoc />
    public partial class AddedtypesforLogins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Logins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "Logins");
        }
    }
}
