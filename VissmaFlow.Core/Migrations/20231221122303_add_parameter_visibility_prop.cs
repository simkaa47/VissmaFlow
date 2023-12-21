﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VissmaFlow.Core.Migrations
{
    /// <inheritdoc />
    public partial class addparametervisibilityprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserAccessLevel",
                table: "ParameterBases",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAccessLevel",
                table: "ParameterBases");
        }
    }
}
