using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountMembers",
                columns: table => new
                {
                    MemberID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MemberPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountMembers", x => x.MemberID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    UnitsInStock = table.Column<short>(type: "smallint", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountMembers",
                columns: new[] { "MemberID", "EmailAddress", "FullName", "MemberPassword", "MemberRole" },
                values: new object[,]
                {
                    { "PS0001", "admin@CompanyName.com", "Administrator", "@1", 1 },
                    { "PS0002", "staff@CompanyName.com", "Staff", "@2", 2 },
                    { "PS0003", "member1@CompanyName.com", "Member 1", "@3", 3 },
                    { "PS0004", "member2@CompanyName.com", "Member 2", "@3", 3 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Beverages" },
                    { 2, "Condiments" },
                    { 3, "Confections" },
                    { 4, "Dairy Products" },
                    { 5, "Grains/Cereals" },
                    { 6, "Meat/Poultry" },
                    { 7, "Produce" },
                    { 8, "Seafood" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "CategoryID", "ProductName", "UnitPrice", "UnitsInStock" },
                values: new object[,]
                {
                    { 1, 3, "Chai", 18.00m, (short)12 },
                    { 2, 1, "Chang", 19.00m, (short)23 },
                    { 3, 2, "Aniseed Syrup", 10.00m, (short)23 },
                    { 4, 2, "Chef Anton's Cajun Seasoning", 22.00m, (short)34 },
                    { 5, 2, "Chef Anton's Gumbo Mix", 21.35m, (short)45 },
                    { 6, 2, "Grandma's Boysenberry Spread", 25.00m, (short)21 },
                    { 7, 7, "Uncle Bob's Organic Dried Pears", 30.00m, (short)22 },
                    { 8, 2, "Northwoods Cranberry Sauce", 40.00m, (short)10 },
                    { 9, 6, "Mishi Kobe Niku", 97.00m, (short)12 },
                    { 10, 8, "Ikura", 31.00m, (short)13 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountMembers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
