using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContactManager.Migrations
{
    /// <inheritdoc />
    public partial class AddCallHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    CallDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CallTime = table.Column<TimeOnly>(type: "time(0)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    CallType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallHistory", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CallHistory",
                columns: new[] { "Id", "CallDate", "CallTime", "CallType", "ContactId", "Duration" },
                values: new object[,]
                {
                    { 1, new DateOnly(2026, 3, 25), new TimeOnly(9, 30, 0), "Incoming", 1, 120 },
                    { 2, new DateOnly(2026, 3, 25), new TimeOnly(10, 15, 0), "Outgoing", 2, 300 },
                    { 3, new DateOnly(2026, 3, 26), new TimeOnly(14, 45, 0), "Missed", 1, 0 },
                    { 4, new DateOnly(2026, 3, 27), new TimeOnly(8, 20, 0), "Incoming", 3, 180 },
                    { 5, new DateOnly(2026, 3, 27), new TimeOnly(19, 5, 0), "Outgoing", 2, 240 },
                    { 6, new DateOnly(2026, 3, 28), new TimeOnly(11, 0, 0), "Incoming", 4, 60 },
                    { 7, new DateOnly(2026, 3, 28), new TimeOnly(21, 15, 0), "Missed", 1, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallHistory");
        }
    }
}
