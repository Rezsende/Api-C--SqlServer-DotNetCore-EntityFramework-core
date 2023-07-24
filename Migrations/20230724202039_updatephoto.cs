using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace c_.Migrations
{
    /// <inheritdoc />
    public partial class updatephoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "photo",
                table: "usuarios",
                newName: "PhotoFileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoFileName",
                table: "usuarios",
                newName: "photo");
        }
    }
}
