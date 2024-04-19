using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VissmaFlow.Core.Migrations
{
    /// <inheritdoc />
    public partial class addsinglemeassettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SingleMeasureSettingses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SourceId = table.Column<int>(type: "INTEGER", nullable: true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleMeasureSettingses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SingleMeasureSettingses_ParameterBases_SourceId",
                        column: x => x.SourceId,
                        principalTable: "ParameterBases",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SingleMeasurePoint",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AvgValueId = table.Column<int>(type: "INTEGER", nullable: true),
                    DestinationId = table.Column<int>(type: "INTEGER", nullable: true),
                    SingleMeasureSettingsId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleMeasurePoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SingleMeasurePoint_ParameterBases_AvgValueId",
                        column: x => x.AvgValueId,
                        principalTable: "ParameterBases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SingleMeasurePoint_ParameterBases_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "ParameterBases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SingleMeasurePoint_SingleMeasureSettingses_SingleMeasureSettingsId",
                        column: x => x.SingleMeasureSettingsId,
                        principalTable: "SingleMeasureSettingses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SingleMeasurePoint_AvgValueId",
                table: "SingleMeasurePoint",
                column: "AvgValueId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleMeasurePoint_DestinationId",
                table: "SingleMeasurePoint",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleMeasurePoint_SingleMeasureSettingsId",
                table: "SingleMeasurePoint",
                column: "SingleMeasureSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleMeasureSettingses_SourceId",
                table: "SingleMeasureSettingses",
                column: "SourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SingleMeasurePoint");

            migrationBuilder.DropTable(
                name: "SingleMeasureSettingses");
        }
    }
}
