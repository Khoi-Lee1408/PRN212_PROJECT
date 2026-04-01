using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContactManager.Migrations
{
    /// <inheritdoc />
    public partial class DataLayerInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Address", "Email", "FullName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Ha Noi", "nguyenvanan@example.com", "Nguyen Van An", "0912345678" },
                    { 2, "Ho Chi Minh", "tranthibinh@example.com", "Tran Thi Binh", "0912345678" },
                    { 3, "Đa Nang", "levancuong@example.com", "Le Van Cuong", "0912345678" },
                    { 4, "Hai Phong", "phamthidung@example.com", "Pham Thi Dung", "0912345678" },
                    { 5, "Can Tho", "hoangvanduc@example.com", "Hoang Van Đuc", "0912345678" },
                    { 6, "Hue", "dothihanh@example.com", "Đo Thi Hanh", "0912345678" },
                    { 7, "Nha Trang", "buivangiang@example.com", "Bui Van Giang", "0912345678" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
