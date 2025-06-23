using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursosOnlineApp.Migrations
{
    /// <inheritdoc />
    public partial class AddDuracionToLecciones : Migration
    {
        /// <inheritdoc />
         protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<int>(
                name: "DuracionMinutos",
                table: "Lecciones",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DuracionMinutos",
                table: "Lecciones");
        }
    }
}
