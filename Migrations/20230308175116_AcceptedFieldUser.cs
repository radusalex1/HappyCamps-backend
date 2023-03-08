using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyCamps_backend.Migrations
{
    /// <inheritdoc />
    public partial class AcceptedFieldUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "Users");
        }
    }
}
