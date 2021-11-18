using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBookAPI.Persistance.Migrations
{
    public partial class AddedDescriptionValueObjectAndModifiedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Users",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PublishingHouses",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ToBeSold",
                table: "Books",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Books",
                type: "decimal(2,2)",
                precision: 2,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,2)",
                oldPrecision: 2,
                oldScale: 2);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Books",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublicationDate",
                table: "Books",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Authors",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 11, 18, 14, 39, 48, 333, DateTimeKind.Local).AddTicks(5111));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: null);

            migrationBuilder.UpdateData(
                table: "PublishingHouses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 11, 18, 14, 39, 48, 353, DateTimeKind.Local).AddTicks(4694));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PublishingHouses");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PublicationDate",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Authors");

            migrationBuilder.AlterColumn<bool>(
                name: "ToBeSold",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Books",
                type: "decimal(2,2)",
                precision: 2,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,2)",
                oldPrecision: 2,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 2000);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 11, 7, 19, 45, 51, 598, DateTimeKind.Local).AddTicks(2968));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "PublishingHouses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 11, 7, 19, 45, 51, 614, DateTimeKind.Local).AddTicks(2909));
        }
    }
}
