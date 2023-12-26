using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VissmaFlow.Core.Migrations
{
    /// <inheritdoc />
    public partial class addevent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RtkUnitId = table.Column<int>(type: "INTEGER", nullable: true),
                    ParameterId = table.Column<int>(type: "INTEGER", nullable: true),
                    EventType = table.Column<int>(type: "INTEGER", nullable: false),
                    ActiveMessage = table.Column<string>(type: "TEXT", nullable: true),
                    NonActiveMessage = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    EventCondition = table.Column<int>(type: "INTEGER", nullable: false),
                    CompareValue = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_ParameterBases_ParameterId",
                        column: x => x.ParameterId,
                        principalTable: "ParameterBases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Events_RtkUnits_RtkUnitId",
                        column: x => x.RtkUnitId,
                        principalTable: "RtkUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_ParameterId",
                table: "Events",
                column: "ParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_RtkUnitId",
                table: "Events",
                column: "RtkUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
