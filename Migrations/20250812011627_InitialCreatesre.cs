using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadDoctor.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatesre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "patients",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "patients");
        }
    }
}
