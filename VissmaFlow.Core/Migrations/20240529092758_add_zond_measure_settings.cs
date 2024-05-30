using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VissmaFlow.Core.Migrations
{
    /// <inheritdoc />
    public partial class addzondmeasuresettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZondMeasureSettingses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SourceId = table.Column<int>(type: "INTEGER", nullable: true),
                    PipeType = table.Column<int>(type: "INTEGER", nullable: false),
                    Height = table.Column<float>(type: "REAL", nullable: false),
                    MeasTime = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZondMeasureSettingses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZondMeasureSettingses_ParameterBases_SourceId",
                        column: x => x.SourceId,
                        principalTable: "ParameterBases",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ZondMeasure",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Distance = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Result = table.Column<float>(type: "REAL", nullable: false),
                    ZondMeasureSettingsId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZondMeasure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZondMeasure_ZondMeasureSettingses_ZondMeasureSettingsId",
                        column: x => x.ZondMeasureSettingsId,
                        principalTable: "ZondMeasureSettingses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZondMeasure_ZondMeasureSettingsId",
                table: "ZondMeasure",
                column: "ZondMeasureSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_ZondMeasureSettingses_SourceId",
                table: "ZondMeasureSettingses",
                column: "SourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZondMeasure");

            migrationBuilder.DropTable(
                name: "ZondMeasureSettingses");
        }
    }
}
