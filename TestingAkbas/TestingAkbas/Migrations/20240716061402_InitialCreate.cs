using Microsoft.EntityFrameworkCore.Migrations;

namespace TestingAkbas.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fabrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QualityClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FabricCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualityGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualityComposition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatternType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    RawFabricPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DomesticPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExportPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fabrics", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fabrics");
        }
    }
}
