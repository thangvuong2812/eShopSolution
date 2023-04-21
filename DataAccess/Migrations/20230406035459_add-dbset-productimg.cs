using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class adddbsetproductimg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2023, 4, 6, 10, 44, 1, 812, DateTimeKind.Local).AddTicks(479));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "acbe4d65-5876-4e32-b4c6-3921a4076a29");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "69b16332-aee7-45fe-b1e1-b251928c6f57", "AQAAAAEAACcQAAAAEEHrVUQrTm3bgCa57eiRxRDI72B9S9Ta2SIhPztOel0BLB0v8GtwNtsUY0kVsn5l8w==" });
        }
    }
}
