using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminBlog.Migrations
{
    public partial class Contacttableisadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 1, 16, 12, 7, 201, DateTimeKind.Local).AddTicks(1224),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 29, 16, 8, 24, 371, DateTimeKind.Local).AddTicks(1717));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 29, 16, 8, 24, 371, DateTimeKind.Local).AddTicks(1717),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 1, 16, 12, 7, 201, DateTimeKind.Local).AddTicks(1224));
        }
    }
}
