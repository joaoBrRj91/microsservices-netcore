using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Discount.GRPC.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameCollumnDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descriptiom",
                table: "Coupons",
                newName: "description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description",
                table: "Coupons",
                newName: "Descriptiom");
        }
    }
}
