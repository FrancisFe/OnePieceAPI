using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnePieceAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FrutasDelDiablo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrutasDelDiablo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Piratas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Recompensa = table.Column<long>(type: "bigint", nullable: false),
                    FrutaDelDiabloId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piratas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Piratas_FrutasDelDiablo_FrutaDelDiabloId",
                        column: x => x.FrutaDelDiabloId,
                        principalTable: "FrutasDelDiablo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Piratas_FrutaDelDiabloId",
                table: "Piratas",
                column: "FrutaDelDiabloId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Piratas");

            migrationBuilder.DropTable(
                name: "FrutasDelDiablo");
        }
    }
}
