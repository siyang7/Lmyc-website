using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lmyc.Data.Migrations
{
    public partial class DeletedAllocatedCredits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllocatedCredit",
                table: "Reservations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AllocatedCredit",
                table: "Reservations",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
