using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VissmaFlow.Core.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PortName = table.Column<string>(type: "TEXT", nullable: false),
                    Baudrate = table.Column<int>(type: "INTEGER", nullable: false),
                    Parity = table.Column<int>(type: "INTEGER", nullable: false),
                    StopBitsNum = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParameterBases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ModbusRegType = table.Column<int>(type: "INTEGER", nullable: false),
                    ModbRegNum = table.Column<int>(type: "INTEGER", nullable: false),
                    UserAccessLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "INTEGER", nullable: false),
                    BitNum = table.Column<int>(type: "INTEGER", nullable: false),
                    StrLength = table.Column<int>(type: "INTEGER", nullable: false),
                    ByteOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    IsRequired = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsOnlyRead = table.Column<bool>(type: "INTEGER", nullable: false),
                    ValidationOk = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsWriting = table.Column<bool>(type: "INTEGER", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    ValueAsBool = table.Column<bool>(type: "INTEGER", nullable: true),
                    WriteValueAsBool = table.Column<bool>(type: "INTEGER", nullable: true),
                    MinValueAsBool = table.Column<bool>(type: "INTEGER", nullable: true),
                    MaxValueAsBool = table.Column<bool>(type: "INTEGER", nullable: true),
                    ValueAsoDouble = table.Column<double>(type: "REAL", nullable: true),
                    WriteValueAsDouble = table.Column<double>(type: "REAL", nullable: true),
                    MinValueAsDouble = table.Column<double>(type: "REAL", nullable: true),
                    MaxValueAsDouble = table.Column<double>(type: "REAL", nullable: true),
                    ValueAsoFloat = table.Column<float>(type: "REAL", nullable: true),
                    WriteValueAsFloat = table.Column<float>(type: "REAL", nullable: true),
                    MinValueAsFloat = table.Column<float>(type: "REAL", nullable: true),
                    MaxValueAsFloat = table.Column<float>(type: "REAL", nullable: true),
                    ValueAsInt = table.Column<int>(type: "INTEGER", nullable: true),
                    WriteValueAsInt = table.Column<int>(type: "INTEGER", nullable: true),
                    MinValueAsInt = table.Column<int>(type: "INTEGER", nullable: true),
                    MaxValueAsInt = table.Column<int>(type: "INTEGER", nullable: true),
                    ValueAsShort = table.Column<short>(type: "INTEGER", nullable: true),
                    WriteValueAsShort = table.Column<short>(type: "INTEGER", nullable: true),
                    MinValueAsShort = table.Column<short>(type: "INTEGER", nullable: true),
                    MaxValueAsShort = table.Column<short>(type: "INTEGER", nullable: true),
                    ValueAsString = table.Column<string>(type: "TEXT", nullable: true),
                    WriteValueAsString = table.Column<string>(type: "TEXT", nullable: true),
                    MinValueAsString = table.Column<string>(type: "TEXT", nullable: true),
                    MaxValueAsString = table.Column<string>(type: "TEXT", nullable: true),
                    ValueAsUint = table.Column<uint>(type: "INTEGER", nullable: true),
                    WriteValueAsUint = table.Column<uint>(type: "INTEGER", nullable: true),
                    MinValueAsUint = table.Column<uint>(type: "INTEGER", nullable: true),
                    MaxValueAsUint = table.Column<uint>(type: "INTEGER", nullable: true),
                    ValueAsUshort = table.Column<ushort>(type: "INTEGER", nullable: true),
                    WriteValueAsUshort = table.Column<ushort>(type: "INTEGER", nullable: true),
                    MinValueAsUshort = table.Column<ushort>(type: "INTEGER", nullable: true),
                    MaxValueAsUshort = table.Column<ushort>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterBases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RtkUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    UnitId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RtkUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    AccessLevel = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommSettings");

            migrationBuilder.DropTable(
                name: "ParameterBases");

            migrationBuilder.DropTable(
                name: "RtkUnits");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
