using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cresk.Migrations
{
    public partial class AddedTicketNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketDisplayNumber",
                table: "DbTicket",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketDisplayNumber",
                table: "DbTicket");
        }
    }
}
