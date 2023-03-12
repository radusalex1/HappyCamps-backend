﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyCamps_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedCityFieldToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Users");
        }
    }
}
