using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    public partial class TabelaInicias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataCadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValorCompra = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorVenda = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EstoqueMinimo = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    EstoqueMaximo = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    QuantidadeEstoque = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CategoriaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id", "DataCadastro", "Nome" },
                values: new object[] { new Guid("8a2addd9-d584-424f-bfff-760e7e6df8df"), new DateTime(2022, 5, 24, 21, 20, 38, 152, DateTimeKind.Local).AddTicks(2661), "Brinquedo" });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id", "DataCadastro", "Nome" },
                values: new object[] { new Guid("d11fd92b-64a7-4e96-8668-0d2e35fe089b"), new DateTime(2022, 5, 24, 21, 20, 38, 152, DateTimeKind.Local).AddTicks(2662), "Sapato" });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id", "DataCadastro", "Nome" },
                values: new object[] { new Guid("fd92420e-eb29-409c-80a7-07b92ee16861"), new DateTime(2022, 5, 24, 21, 20, 38, 152, DateTimeKind.Local).AddTicks(2641), "Roupa" });

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CategoriaId",
                table: "Produto",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
