using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HomeSweetHomeServer.Migrations
{
    public partial class Db_Home : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceId",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HomeId",
                table: "User",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Home",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdminId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Home", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Home_User_AdminId",
                        column: x => x.AdminId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_HomeId",
                table: "User",
                column: "HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Home_AdminId",
                table: "Home",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Home_HomeId",
                table: "User",
                column: "HomeId",
                principalTable: "Home",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Home_HomeId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Home");

            migrationBuilder.DropIndex(
                name: "IX_User_HomeId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "HomeId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "User");
        }
    }
}
