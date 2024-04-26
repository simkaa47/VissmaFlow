using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VissmaFlow.Core.Migrations
{
    /// <inheritdoc />
    public partial class addlogsettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LogSettingsId",
                table: "ParameterBases",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LogSettingses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Path = table.Column<string>(type: "TEXT", nullable: true),
                    MinPeriod = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogSettingses", x => x.Id);
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParameterBases_LogSettingses_LogSettingsId",
                table: "ParameterBases");

            migrationBuilder.DropTable(
                name: "LogSettingses");

            migrationBuilder.DropIndex(
                name: "IX_ParameterBases_LogSettingsId",
                table: "ParameterBases");

            migrationBuilder.DropColumn(
                name: "LogSettingsId",
                table: "ParameterBases");
        }
    }
}
