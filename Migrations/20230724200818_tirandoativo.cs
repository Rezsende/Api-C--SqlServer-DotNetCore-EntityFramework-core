using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace c_.Migrations
{
    /// <inheritdoc />
    public partial class tirandoativo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "usuarios");

            migrationBuilder.AddColumn<string>(
                name: "photo",
                table: "usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photo",
                table: "usuarios");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
