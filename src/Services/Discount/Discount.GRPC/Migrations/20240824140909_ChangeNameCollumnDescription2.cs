using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Discount.GRPC.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameCollumnDescription2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description",
                table: "Coupons",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Coupons",
                newName: "description");
        }
    }
}
