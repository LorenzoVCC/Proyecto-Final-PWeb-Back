using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Final_ProgramacionWEB.Migrations
{
    /// <inheritdoc />
    public partial class RenamePasswordHashToPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Restaurants",
                newName: "Password");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Restaurants",
                newName: "PasswordHash");
        }
    }
}
