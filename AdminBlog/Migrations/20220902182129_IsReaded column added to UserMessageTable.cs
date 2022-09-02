using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminBlog.Migrations
{
    public partial class IsReadedcolumnaddedtoUserMessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReaded",
                table: "UserMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 2, 22, 21, 29, 571, DateTimeKind.Local).AddTicks(2428),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 1, 16, 13, 15, 523, DateTimeKind.Local).AddTicks(8720));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReaded",
                table: "UserMessages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 1, 16, 13, 15, 523, DateTimeKind.Local).AddTicks(8720),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 2, 22, 21, 29, 571, DateTimeKind.Local).AddTicks(2428));
        }
    }
}
