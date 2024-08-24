using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Discount.GRPC.Migrations
{
    /// <inheritdoc />
    public partial class AddSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "Id", "Amount", "Descriptiom", "ProductName" },
                values: new object[,]
                {
                    { new Guid("b35c039f-30ff-465d-87bb-f7cb5a35d9aa"), 499, "IPhone Description", "IPhone 15" },
                    { new Guid("ed52fe5c-0392-4606-8cb1-8725180fe432"), 499, "IPhone Description", "IPhone 15" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: new Guid("b35c039f-30ff-465d-87bb-f7cb5a35d9aa"));

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: new Guid("ed52fe5c-0392-4606-8cb1-8725180fe432"));
        }
    }
}
