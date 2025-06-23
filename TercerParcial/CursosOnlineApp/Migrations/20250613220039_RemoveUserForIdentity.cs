using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace CursosOnlineApp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserForIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_AspNetUsers_ApplicationUserId",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Usuario_UsuarioId",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Usuario_UsuarioId",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscripciones_AspNetUsers_ApplicationUserId",
                table: "Inscripciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscripciones_Usuario_UsuarioId",
                table: "Inscripciones");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgresosLecciones_AspNetUsers_ApplicationUserId",
                table: "ProgresosLecciones");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgresosLecciones_Usuario_UsuarioId",
                table: "ProgresosLecciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Resenas_AspNetUsers_ApplicationUserId",
                table: "Resenas");

            migrationBuilder.DropForeignKey(
                name: "FK_Resenas_Usuario_UsuarioId",
                table: "Resenas");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Resenas_ApplicationUserId",
                table: "Resenas");

            migrationBuilder.DropIndex(
                name: "IX_ProgresosLecciones_ApplicationUserId",
                table: "ProgresosLecciones");

            migrationBuilder.DropIndex(
                name: "IX_Inscripciones_ApplicationUserId",
                table: "Inscripciones");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_UsuarioId",
                table: "Cursos");

            migrationBuilder.DropIndex(
                name: "IX_Comentarios_ApplicationUserId",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Resenas");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ProgresosLecciones");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Inscripciones");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Comentarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_AspNetUsers_UsuarioId",
                table: "Comentarios",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscripciones_AspNetUsers_UsuarioId",
                table: "Inscripciones",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgresosLecciones_AspNetUsers_UsuarioId",
                table: "ProgresosLecciones",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resenas_AspNetUsers_UsuarioId",
                table: "Resenas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_AspNetUsers_UsuarioId",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscripciones_AspNetUsers_UsuarioId",
                table: "Inscripciones");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgresosLecciones_AspNetUsers_UsuarioId",
                table: "ProgresosLecciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Resenas_AspNetUsers_UsuarioId",
                table: "Resenas");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "Resenas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "ProgresosLecciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "Inscripciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Cursos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "Comentarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false),
                    Rol = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Resenas_ApplicationUserId",
                table: "Resenas",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgresosLecciones_ApplicationUserId",
                table: "ProgresosLecciones",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_ApplicationUserId",
                table: "Inscripciones",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_UsuarioId",
                table: "Cursos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_ApplicationUserId",
                table: "Comentarios",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_AspNetUsers_ApplicationUserId",
                table: "Comentarios",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Usuario_UsuarioId",
                table: "Comentarios",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Usuario_UsuarioId",
                table: "Cursos",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inscripciones_AspNetUsers_ApplicationUserId",
                table: "Inscripciones",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inscripciones_Usuario_UsuarioId",
                table: "Inscripciones",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgresosLecciones_AspNetUsers_ApplicationUserId",
                table: "ProgresosLecciones",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgresosLecciones_Usuario_UsuarioId",
                table: "ProgresosLecciones",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resenas_AspNetUsers_ApplicationUserId",
                table: "Resenas",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resenas_Usuario_UsuarioId",
                table: "Resenas",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
