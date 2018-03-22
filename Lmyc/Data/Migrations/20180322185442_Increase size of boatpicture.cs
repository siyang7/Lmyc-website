using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lmyc.Data.Migrations
{
    public partial class Increasesizeofboatpicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "BoatPicture",
                table: "Boats",
                maxLength: 2147483647,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldMaxLength: 1024,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "BoatPicture",
                table: "Boats",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldMaxLength: 2147483647,
                oldNullable: true);
        }
    }
}
