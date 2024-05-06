using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dental_lab_Application_MVC_.Migrations
{
    /// <inheritdoc />
    public partial class thirdmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_ServicesId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ServicesId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ServicesId",
                table: "Appointments");

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: new Guid("50b815eb-02e3-4579-9a83-1d39884ee56c"),
                column: "DateCreated",
                value: new DateTime(2024, 4, 30, 21, 24, 17, 904, DateTimeKind.Local).AddTicks(2858));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0504ea46-35aa-4949-9c59-b8b32a083ef6"),
                column: "DateCreated",
                value: new DateTime(2024, 4, 30, 21, 24, 17, 904, DateTimeKind.Local).AddTicks(2767));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6b541cc9-b08b-47d3-b52f-3ca6aa06a1e6"),
                column: "DateCreated",
                value: new DateTime(2024, 4, 30, 21, 24, 17, 904, DateTimeKind.Local).AddTicks(2687));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cf58ec7c-7829-4549-a924-ffe60ea3ca14"),
                column: "DateCreated",
                value: new DateTime(2024, 4, 30, 21, 24, 17, 904, DateTimeKind.Local).AddTicks(2793));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("419460cf-bda5-41bd-a342-eb30c795fda3"),
                column: "DateCreated",
                value: new DateTime(2024, 4, 30, 21, 24, 17, 904, DateTimeKind.Local).AddTicks(2823));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ServicesId",
                table: "Appointments",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: new Guid("50b815eb-02e3-4579-9a83-1d39884ee56c"),
                column: "DateCreated",
                value: new DateTime(2024, 4, 24, 15, 12, 17, 521, DateTimeKind.Local).AddTicks(3739));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0504ea46-35aa-4949-9c59-b8b32a083ef6"),
                column: "DateCreated",
                value: new DateTime(2024, 4, 24, 15, 12, 17, 521, DateTimeKind.Local).AddTicks(3641));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6b541cc9-b08b-47d3-b52f-3ca6aa06a1e6"),
                column: "DateCreated",
                value: new DateTime(2024, 4, 24, 15, 12, 17, 521, DateTimeKind.Local).AddTicks(3572));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cf58ec7c-7829-4549-a924-ffe60ea3ca14"),
                column: "DateCreated",
                value: new DateTime(2024, 4, 24, 15, 12, 17, 521, DateTimeKind.Local).AddTicks(3663));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("419460cf-bda5-41bd-a342-eb30c795fda3"),
                column: "DateCreated",
                value: new DateTime(2024, 4, 24, 15, 12, 17, 521, DateTimeKind.Local).AddTicks(3691));

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServicesId",
                table: "Appointments",
                column: "ServicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_ServicesId",
                table: "Appointments",
                column: "ServicesId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
