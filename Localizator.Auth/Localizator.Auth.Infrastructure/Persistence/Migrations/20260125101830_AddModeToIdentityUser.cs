using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Localizator.Auth.Infrastructure.Localizator.Auth.Localizator.Auth.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddModeToIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Mode",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mode",
                table: "AspNetUsers");
        }
    }
}
