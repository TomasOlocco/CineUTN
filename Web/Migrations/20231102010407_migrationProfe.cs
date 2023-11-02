using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class migrationProfe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CondicionPago",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CondicionPago", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUrgent = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoRefId = table.Column<int>(type: "int", nullable: true),
                    SonidoRefId = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sala_Sonido_SonidoRefId",
                        column: x => x.SonidoRefId,
                        principalTable: "Sonido",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Sala_Tipo_TipoRefId",
                        column: x => x.TipoRefId,
                        principalTable: "Tipo",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ListaPrecio",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaHasta = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CondicionPagoRefId = table.Column<int>(type: "int", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaPrecio", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ListaPrecio_CondicionPago_CondicionPagoRefId",
                        column: x => x.CondicionPagoRefId,
                        principalTable: "CondicionPago",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PedidoItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PedidoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoItem_Pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedido",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Funcion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaHoraFuncion = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    PeliculaRefId = table.Column<int>(type: "int", nullable: true),
                    SalaRefId = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcion_Pelicula_PeliculaRefId",
                        column: x => x.PeliculaRefId,
                        principalTable: "Pelicula",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Funcion_Sala_SalaRefId",
                        column: x => x.SalaRefId,
                        principalTable: "Sala",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Tarifa",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PorcentajeDescuento = table.Column<int>(type: "int", nullable: false),
                    ListaPrecioRefId = table.Column<int>(type: "int", nullable: true),
                    TarifaPrecio = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifa", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tarifa_ListaPrecio_ListaPrecioRefId",
                        column: x => x.ListaPrecioRefId,
                        principalTable: "ListaPrecio",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FuncionTarifa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TarifaRefId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FuncionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionTarifa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuncionTarifa_Funcion_FuncionId",
                        column: x => x.FuncionId,
                        principalTable: "Funcion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FuncionTarifa_Tarifa_TarifaRefId",
                        column: x => x.TarifaRefId,
                        principalTable: "Tarifa",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Programar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaHoraFuncion = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    PeliculaRefId = table.Column<int>(type: "int", nullable: true),
                    SalaRefId = table.Column<int>(type: "int", nullable: true),
                    Tarifa1RefId = table.Column<int>(type: "int", nullable: true),
                    Tarifa2RefId = table.Column<int>(type: "int", nullable: true),
                    Tarifa3RefId = table.Column<int>(type: "int", nullable: true),
                    Tarifa4RefId = table.Column<int>(type: "int", nullable: true),
                    Tarifa5RefId = table.Column<int>(type: "int", nullable: true),
                    Tarifa6RefId = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programar", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Programar_Pelicula_PeliculaRefId",
                        column: x => x.PeliculaRefId,
                        principalTable: "Pelicula",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Programar_Sala_SalaRefId",
                        column: x => x.SalaRefId,
                        principalTable: "Sala",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Programar_Tarifa_Tarifa1RefId",
                        column: x => x.Tarifa1RefId,
                        principalTable: "Tarifa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Programar_Tarifa_Tarifa2RefId",
                        column: x => x.Tarifa2RefId,
                        principalTable: "Tarifa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Programar_Tarifa_Tarifa3RefId",
                        column: x => x.Tarifa3RefId,
                        principalTable: "Tarifa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Programar_Tarifa_Tarifa4RefId",
                        column: x => x.Tarifa4RefId,
                        principalTable: "Tarifa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Programar_Tarifa_Tarifa5RefId",
                        column: x => x.Tarifa5RefId,
                        principalTable: "Tarifa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Programar_Tarifa_Tarifa6RefId",
                        column: x => x.Tarifa6RefId,
                        principalTable: "Tarifa",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funcion_PeliculaRefId",
                table: "Funcion",
                column: "PeliculaRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcion_SalaRefId",
                table: "Funcion",
                column: "SalaRefId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionTarifa_FuncionId",
                table: "FuncionTarifa",
                column: "FuncionId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionTarifa_TarifaRefId",
                table: "FuncionTarifa",
                column: "TarifaRefId");

            migrationBuilder.CreateIndex(
                name: "IX_ListaPrecio_CondicionPagoRefId",
                table: "ListaPrecio",
                column: "CondicionPagoRefId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItem_PedidoId",
                table: "PedidoItem",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Programar_PeliculaRefId",
                table: "Programar",
                column: "PeliculaRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Programar_SalaRefId",
                table: "Programar",
                column: "SalaRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Programar_Tarifa1RefId",
                table: "Programar",
                column: "Tarifa1RefId");

            migrationBuilder.CreateIndex(
                name: "IX_Programar_Tarifa2RefId",
                table: "Programar",
                column: "Tarifa2RefId");

            migrationBuilder.CreateIndex(
                name: "IX_Programar_Tarifa3RefId",
                table: "Programar",
                column: "Tarifa3RefId");

            migrationBuilder.CreateIndex(
                name: "IX_Programar_Tarifa4RefId",
                table: "Programar",
                column: "Tarifa4RefId");

            migrationBuilder.CreateIndex(
                name: "IX_Programar_Tarifa5RefId",
                table: "Programar",
                column: "Tarifa5RefId");

            migrationBuilder.CreateIndex(
                name: "IX_Programar_Tarifa6RefId",
                table: "Programar",
                column: "Tarifa6RefId");

            migrationBuilder.CreateIndex(
                name: "IX_Sala_SonidoRefId",
                table: "Sala",
                column: "SonidoRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Sala_TipoRefId",
                table: "Sala",
                column: "TipoRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarifa_ListaPrecioRefId",
                table: "Tarifa",
                column: "ListaPrecioRefId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuncionTarifa");

            migrationBuilder.DropTable(
                name: "PedidoItem");

            migrationBuilder.DropTable(
                name: "Programar");

            migrationBuilder.DropTable(
                name: "Funcion");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Tarifa");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "ListaPrecio");

            migrationBuilder.DropTable(
                name: "CondicionPago");
        }
    }
}
