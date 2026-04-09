using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web_API_Demo.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shirts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shirts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Shirts",
                columns: new[] { "Id", "Brand", "Colour", "Sex", "Size" },
                values: new object[,]
                {
                    { 1, "Nike", "Black", "Female", 10 },
                    { 2, "Puma", "Red", "Male", 12 },
                    { 3, "RHCP", "Grey", "Female", 10 },
                    { 4, "TBK", "Pink", "Female", 6 },
                    { 5, "TBS", "Black", "Male", 8 },
                    { 6, "DC", "White", "Male", 11 },
                    { 7, "Adidas", "Grey", "Female", 10 },
                    { 8, "[no-name]", "White", "Male", 12 },
                    { 9, "Nike", "Gold", "Female", 10 },
                    { 10, "RHCP", "Red", "Female", 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shirts");
        }
    }
}
