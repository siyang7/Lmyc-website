using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lmyc.Data.Migrations
{
    public partial class DeleteUserStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberStatus",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SkipperStatus",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberStatus",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkipperStatus",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
