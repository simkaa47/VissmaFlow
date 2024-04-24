using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VissmaFlow.Core.Migrations
{
    /// <inheritdoc />
    public partial class addlogcell : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_ParameterBases_ParameterId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_RtkUnits_RtkUnitId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_IndicationCells_ParameterBases_ParameterId",
                table: "IndicationCells");

            migrationBuilder.DropForeignKey(
                name: "FK_IndicationCells_RtkUnits_RtkUnitId",
                table: "IndicationCells");

            migrationBuilder.CreateTable(
                name: "LogCell",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RtkUnitId = table.Column<int>(type: "INTEGER", nullable: true),
                    ParameterId = table.Column<int>(type: "INTEGER", nullable: true),
                    LogSettingsId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogCell", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogCell_LogSettingses_LogSettingsId",
                        column: x => x.LogSettingsId,
                        principalTable: "LogSettingses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LogCell_ParameterBases_ParameterId",
                        column: x => x.ParameterId,
                        principalTable: "ParameterBases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_LogCell_RtkUnits_RtkUnitId",
                        column: x => x.RtkUnitId,
                        principalTable: "RtkUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogCell_LogSettingsId",
                table: "LogCell",
                column: "LogSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_LogCell_ParameterId",
                table: "LogCell",
                column: "ParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_LogCell_RtkUnitId",
                table: "LogCell",
                column: "RtkUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_ParameterBases_ParameterId",
                table: "Events",
                column: "ParameterId",
                principalTable: "ParameterBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_RtkUnits_RtkUnitId",
                table: "Events",
                column: "RtkUnitId",
                principalTable: "RtkUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_IndicationCells_ParameterBases_ParameterId",
                table: "IndicationCells",
                column: "ParameterId",
                principalTable: "ParameterBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_IndicationCells_RtkUnits_RtkUnitId",
                table: "IndicationCells",
                column: "RtkUnitId",
                principalTable: "RtkUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_ParameterBases_ParameterId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_RtkUnits_RtkUnitId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_IndicationCells_ParameterBases_ParameterId",
                table: "IndicationCells");

            migrationBuilder.DropForeignKey(
                name: "FK_IndicationCells_RtkUnits_RtkUnitId",
                table: "IndicationCells");

            migrationBuilder.DropTable(
                name: "LogCell");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_ParameterBases_ParameterId",
                table: "Events",
                column: "ParameterId",
                principalTable: "ParameterBases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_RtkUnits_RtkUnitId",
                table: "Events",
                column: "RtkUnitId",
                principalTable: "RtkUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IndicationCells_ParameterBases_ParameterId",
                table: "IndicationCells",
                column: "ParameterId",
                principalTable: "ParameterBases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IndicationCells_RtkUnits_RtkUnitId",
                table: "IndicationCells",
                column: "RtkUnitId",
                principalTable: "RtkUnits",
                principalColumn: "Id");
        }
    }
}
