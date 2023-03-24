using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Autoglass.DesafioTecnico.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Situacao = table.Column<bool>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    DataFabricacao = table.Column<DateTime>(nullable: false),
                    DataValidade = table.Column<DateTime>(nullable: false),
                    CodigoFornecedor = table.Column<int>(nullable: false),
                    DescricaoFornecedor = table.Column<string>(nullable: true),
                    CNPJFornecedor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Codigo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
