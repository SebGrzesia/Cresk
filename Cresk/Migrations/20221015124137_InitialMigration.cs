using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cresk.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbTag",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbTag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbTicket",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DbTagId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyData = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbTicket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbTicket_DbTag_DbTagId",
                        column: x => x.DbTagId,
                        principalTable: "DbTag",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbTicket_DbTagId",
                table: "DbTicket",
                column: "DbTagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbTicket");

            migrationBuilder.DropTable(
                name: "DbTag");
        }
    }
}
