using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VissmaFlow.Core.Migrations
{
    /// <inheritdoc />
    public partial class addcurvetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Curves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RtkUnitId = table.Column<int>(type: "INTEGER", nullable: true),
                    ParameterId = table.Column<int>(type: "INTEGER", nullable: true),
                    Color = table.Column<string>(type: "TEXT", nullable: false),
                    IsVisible = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Curves_ParameterBases_ParameterId",
                        column: x => x.ParameterId,
                        principalTable: "ParameterBases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Curves_RtkUnits_RtkUnitId",
                        column: x => x.RtkUnitId,
                        principalTable: "RtkUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curves_ParameterId",
                table: "Curves",
                column: "ParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_Curves_RtkUnitId",
                table: "Curves",
                column: "RtkUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Curves");
        }
    }
}
