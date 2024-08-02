using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final01Demo.Migrations
{
    /// <inheritdoc />
    public partial class CreateV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "toaNha",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_toaNha", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "canHos",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DienTich = table.Column<double>(type: "float", nullable: false),
                    SoNha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDToaNha = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_canHos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_canHos_toaNha_IDToaNha",
                        column: x => x.IDToaNha,
                        principalTable: "toaNha",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_canHos_IDToaNha",
                table: "canHos",
                column: "IDToaNha");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "canHos");

            migrationBuilder.DropTable(
                name: "toaNha");
        }
    }
}
