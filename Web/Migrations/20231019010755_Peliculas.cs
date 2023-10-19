using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class Peliculas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pelicula",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImagemPelicula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duracion = table.Column<int>(type: "int", nullable: true),
                    Clasificacion = table.Column<int>(type: "int", nullable: true),
                    GeneroRefId = table.Column<int>(type: "int", nullable: true),
                    TipoRefId = table.Column<int>(type: "int", nullable: true),
                    SubtituloRefId = table.Column<int>(type: "int", nullable: true),
                    FechaEstreno = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelicula", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pelicula_Genero_GeneroRefId",
                        column: x => x.GeneroRefId,
                        principalTable: "Genero",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Pelicula_Subtitulo_SubtituloRefId",
                        column: x => x.SubtituloRefId,
                        principalTable: "Subtitulo",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Pelicula_Tipo_TipoRefId",
                        column: x => x.TipoRefId,
                        principalTable: "Tipo",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pelicula_GeneroRefId",
                table: "Pelicula",
                column: "GeneroRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Pelicula_SubtituloRefId",
                table: "Pelicula",
                column: "SubtituloRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Pelicula_TipoRefId",
                table: "Pelicula",
                column: "TipoRefId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pelicula");
        }
    }
}
