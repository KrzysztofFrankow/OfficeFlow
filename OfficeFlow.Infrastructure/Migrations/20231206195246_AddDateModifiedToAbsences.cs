using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDateModifiedToAbsences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Absences",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Absences");
        }
    }
}
