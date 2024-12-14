using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class personinshareholderfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shareholders_Businesses_BusinessId",
                table: "Shareholders");

            migrationBuilder.DropForeignKey(
                name: "FK_Shareholders_Persons_PersonId1",
                table: "Shareholders");

            migrationBuilder.DropIndex(
                name: "IX_Shareholders_BusinessId",
                table: "Shareholders");

            migrationBuilder.DropIndex(
                name: "IX_Shareholders_PersonId1",
                table: "Shareholders");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Shareholders");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                table: "Shareholders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                table: "Shareholders",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonId1",
                table: "Shareholders",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shareholders_BusinessId",
                table: "Shareholders",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Shareholders_PersonId1",
                table: "Shareholders",
                column: "PersonId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Shareholders_Businesses_BusinessId",
                table: "Shareholders",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shareholders_Persons_PersonId1",
                table: "Shareholders",
                column: "PersonId1",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
