using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updatecollumntype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2023, 4, 17, 11, 40, 55, 899, DateTimeKind.Local).AddTicks(731));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "1f9ae837-46ae-4b94-99c7-9b1b0d6f8f32");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d5a6f3bc-b31d-4641-8f5f-39c98115ae1d", "AQAAAAEAACcQAAAAEKQ6pbRhA6zCCgcHaeblsLZ+vQtch2OlOuysxj8jqFd95jhYGCr3apW2HIyu9i6Nug==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2023, 4, 6, 10, 54, 59, 213, DateTimeKind.Local).AddTicks(1640));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "b82d3b44-022f-48b4-9f40-c79657b9cee6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "74d45caa-af31-491e-93cd-6f3386cb90bb", "AQAAAAEAACcQAAAAEIgYQyrMcCmsnVGFZFndsicYuEs5m/aToT63+1nPrLNiSjQk1R2lwvIVR8cR+D+UhA==" });
        }
    }
}
