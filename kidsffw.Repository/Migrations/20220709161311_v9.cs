using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kidsffw.Repository.Migrations
{
    public partial class v9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRegistrationEntities",
                table: "UserRegistrationEntities");

            migrationBuilder.RenameTable(
                name: "UserRegistrationEntities",
                newName: "UserRegistrations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRegistrations",
                table: "UserRegistrations",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRegistrations",
                table: "UserRegistrations");

            migrationBuilder.RenameTable(
                name: "UserRegistrations",
                newName: "UserRegistrationEntities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRegistrationEntities",
                table: "UserRegistrationEntities",
                column: "Id");
        }
    }
}
