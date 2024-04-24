using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VissmaFlow.Core.Migrations
{
    /// <inheritdoc />
    public partial class adddeletebahaviorforsinglemeasures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParameterBases_LogSettingses_LogSettingsId",
                table: "ParameterBases");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleMeasurePoint_ParameterBases_AvgValueId",
                table: "SingleMeasurePoint");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleMeasurePoint_ParameterBases_DestinationId",
                table: "SingleMeasurePoint");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleMeasureSettingses_ParameterBases_SourceId",
                table: "SingleMeasureSettingses");

            migrationBuilder.DropIndex(
                name: "IX_ParameterBases_LogSettingsId",
                table: "ParameterBases");

            migrationBuilder.DropColumn(
                name: "LogSettingsId",
                table: "ParameterBases");

            migrationBuilder.AddForeignKey(
                name: "FK_SingleMeasurePoint_ParameterBases_AvgValueId",
                table: "SingleMeasurePoint",
                column: "AvgValueId",
                principalTable: "ParameterBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleMeasurePoint_ParameterBases_DestinationId",
                table: "SingleMeasurePoint",
                column: "DestinationId",
                principalTable: "ParameterBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleMeasureSettingses_ParameterBases_SourceId",
                table: "SingleMeasureSettingses",
                column: "SourceId",
                principalTable: "ParameterBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SingleMeasurePoint_ParameterBases_AvgValueId",
                table: "SingleMeasurePoint");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleMeasurePoint_ParameterBases_DestinationId",
                table: "SingleMeasurePoint");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleMeasureSettingses_ParameterBases_SourceId",
                table: "SingleMeasureSettingses");

            migrationBuilder.AddColumn<int>(
                name: "LogSettingsId",
                table: "ParameterBases",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParameterBases_LogSettingsId",
                table: "ParameterBases",
                column: "LogSettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterBases_LogSettingses_LogSettingsId",
                table: "ParameterBases",
                column: "LogSettingsId",
                principalTable: "LogSettingses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SingleMeasurePoint_ParameterBases_AvgValueId",
                table: "SingleMeasurePoint",
                column: "AvgValueId",
                principalTable: "ParameterBases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SingleMeasurePoint_ParameterBases_DestinationId",
                table: "SingleMeasurePoint",
                column: "DestinationId",
                principalTable: "ParameterBases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SingleMeasureSettingses_ParameterBases_SourceId",
                table: "SingleMeasureSettingses",
                column: "SourceId",
                principalTable: "ParameterBases",
                principalColumn: "Id");
        }
    }
}
