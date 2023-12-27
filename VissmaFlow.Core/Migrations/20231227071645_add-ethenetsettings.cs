using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VissmaFlow.Core.Migrations
{
    /// <inheritdoc />
    public partial class addethenetsettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Interface",
                table: "CommSettings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "CommSettings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PortNumber",
                table: "CommSettings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interface",
                table: "CommSettings");

            migrationBuilder.DropColumn(
                name: "Ip",
                table: "CommSettings");

            migrationBuilder.DropColumn(
                name: "PortNumber",
                table: "CommSettings");
        }
    }
}
