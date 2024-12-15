using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class removetype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shareholders_ShareholdersTypes_ShareholderTypeId",
                table: "Shareholders");

            migrationBuilder.DropTable(
                name: "ShareholdersTypes");

            migrationBuilder.DropIndex(
                name: "IX_Shareholders_ShareholderTypeId",
                table: "Shareholders");

            migrationBuilder.DropColumn(
                name: "ShareholderTypeId",
                table: "Shareholders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShareholderTypeId",
                table: "Shareholders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ShareholdersTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareholdersTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shareholders_ShareholderTypeId",
                table: "Shareholders",
                column: "ShareholderTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shareholders_ShareholdersTypes_ShareholderTypeId",
                table: "Shareholders",
                column: "ShareholderTypeId",
                principalTable: "ShareholdersTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
