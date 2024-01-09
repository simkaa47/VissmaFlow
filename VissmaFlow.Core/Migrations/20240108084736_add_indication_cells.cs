using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VissmaFlow.Core.Migrations
{
    /// <inheritdoc />
    public partial class addindicationcells : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IndicationCells",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RtkUnitId = table.Column<int>(type: "INTEGER", nullable: true),
                    ParameterId = table.Column<int>(type: "INTEGER", nullable: true),
                    MeasUnit = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicationCells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndicationCells_ParameterBases_ParameterId",
                        column: x => x.ParameterId,
                        principalTable: "ParameterBases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IndicationCells_RtkUnits_RtkUnitId",
                        column: x => x.RtkUnitId,
                        principalTable: "RtkUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndicationCells_ParameterId",
                table: "IndicationCells",
                column: "ParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicationCells_RtkUnitId",
                table: "IndicationCells",
                column: "RtkUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndicationCells");
        }
    }
}
