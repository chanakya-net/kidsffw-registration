using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kidsffw.Repository.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "UserRegistrationEntities",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "TransactionDate",
                table: "UserRegistrationEntities",
                newName: "OrderCreationDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "UserRegistrationEntities",
                newName: "TransactionId");

            migrationBuilder.RenameColumn(
                name: "OrderCreationDate",
                table: "UserRegistrationEntities",
                newName: "TransactionDate");
        }
    }
}
