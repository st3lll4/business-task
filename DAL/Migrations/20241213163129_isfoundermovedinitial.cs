using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class isfoundermovedinitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BusinessName = table.Column<string>(type: "TEXT", nullable: false),
                    RegistryCode = table.Column<string>(type: "TEXT", nullable: false),
                    TotalCapital = table.Column<int>(type: "INTEGER", nullable: false),
                    FoundingDate = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    IdCode = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Shareholders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: true),
                    ShareholderBusinessId = table.Column<int>(type: "INTEGER", nullable: true),
                    ShareholderTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    BusinessId = table.Column<int>(type: "INTEGER", nullable: true),
                    PersonId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shareholders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shareholders_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shareholders_Businesses_ShareholderBusinessId",
                        column: x => x.ShareholderBusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shareholders_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shareholders_Persons_PersonId1",
                        column: x => x.PersonId1,
                        principalTable: "Persons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shareholders_ShareholdersTypes_ShareholderTypeId",
                        column: x => x.ShareholderTypeId,
                        principalTable: "ShareholdersTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShareholdersInBusinesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BusinessId = table.Column<int>(type: "INTEGER", nullable: false),
                    ShareholderId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareholdersInBusinesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShareholdersInBusinesses_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShareholdersInBusinesses_Shareholders_ShareholderId",
                        column: x => x.ShareholderId,
                        principalTable: "Shareholders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shareholders_BusinessId",
                table: "Shareholders",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Shareholders_PersonId",
                table: "Shareholders",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Shareholders_PersonId1",
                table: "Shareholders",
                column: "PersonId1");

            migrationBuilder.CreateIndex(
                name: "IX_Shareholders_ShareholderBusinessId",
                table: "Shareholders",
                column: "ShareholderBusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Shareholders_ShareholderTypeId",
                table: "Shareholders",
                column: "ShareholderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareholdersInBusinesses_BusinessId",
                table: "ShareholdersInBusinesses",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareholdersInBusinesses_ShareholderId",
                table: "ShareholdersInBusinesses",
                column: "ShareholderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShareholdersInBusinesses");

            migrationBuilder.DropTable(
                name: "Shareholders");

            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "ShareholdersTypes");
        }
    }
}
