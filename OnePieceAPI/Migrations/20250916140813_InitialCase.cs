using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OnePieceAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FrutasDelDiablo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrutasDelDiablo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Piratas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Recompensa = table.Column<long>(type: "bigint", nullable: false),
                    FrutaDelDiabloId = table.Column<int>(type: "integer", nullable: true),
                    TripulacionId = table.Column<int>(type: "integer", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Tripulaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    RecompensaTotal = table.Column<long>(type: "bigint", nullable: false),
                    CapitanId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tripulaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tripulaciones_Piratas_CapitanId",
                        column: x => x.CapitanId,
                        principalTable: "Piratas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Piratas_FrutaDelDiabloId",
                table: "Piratas",
                column: "FrutaDelDiabloId");

            migrationBuilder.CreateIndex(
                name: "IX_Piratas_TripulacionId",
                table: "Piratas",
                column: "TripulacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tripulaciones_CapitanId",
                table: "Tripulaciones",
                column: "CapitanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Piratas_Tripulaciones_TripulacionId",
                table: "Piratas",
                column: "TripulacionId",
                principalTable: "Tripulaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Piratas_FrutasDelDiablo_FrutaDelDiabloId",
                table: "Piratas");

            migrationBuilder.DropForeignKey(
                name: "FK_Piratas_Tripulaciones_TripulacionId",
                table: "Piratas");

            migrationBuilder.DropTable(
                name: "FrutasDelDiablo");

            migrationBuilder.DropTable(
                name: "Tripulaciones");

            migrationBuilder.DropTable(
                name: "Piratas");
        }
    }
}
