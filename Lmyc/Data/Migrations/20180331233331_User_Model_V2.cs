using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lmyc.Data.Migrations
{
    public partial class User_Model_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmergencyContactTwoPhone",
                table: "AspNetUsers",
                newName: "EmergencyContactTwo");

            migrationBuilder.RenameColumn(
                name: "EmergencyContactOnePhone",
                table: "AspNetUsers",
                newName: "Province");

            migrationBuilder.AlterColumn<int>(
                name: "SailingExperience",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactOne",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Volunteer",
                columns: table => new
                {
                    VoluntterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClassificationCodes = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteer", x => x.VoluntterId);
                    table.ForeignKey(
                        name: "FK_Volunteer_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Volunteer_Id",
                table: "Volunteer",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Volunteer");

            migrationBuilder.DropColumn(
                name: "EmergencyContactOne",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "AspNetUsers",
                newName: "EmergencyContactOnePhone");

            migrationBuilder.RenameColumn(
                name: "EmergencyContactTwo",
                table: "AspNetUsers",
                newName: "EmergencyContactTwoPhone");

            migrationBuilder.AlterColumn<string>(
                name: "SailingExperience",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
