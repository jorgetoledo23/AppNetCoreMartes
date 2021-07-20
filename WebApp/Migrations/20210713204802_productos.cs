using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class productos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblProductos",
                columns: table => new
                {
                    ProductoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Precio = table.Column<int>(nullable: false),
                    PrecioOferta = table.Column<int>(nullable: false),
                    EnOferta = table.Column<bool>(nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    Imagen = table.Column<string>(nullable: true),
                    CategoriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductos", x => x.ProductoId);
                    table.ForeignKey(
                        name: "FK_tblProductos_tblCategorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "tblCategorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblProductos_CategoriaId",
                table: "tblProductos",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblProductos");
        }
    }
}
