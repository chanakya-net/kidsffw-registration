using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kidsffw.Repository.Migrations
{
    public partial class v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_otpEntities",
                table: "otpEntities");

            migrationBuilder.RenameTable(
                name: "otpEntities",
                newName: "otps");

            migrationBuilder.AddPrimaryKey(
                name: "PK_otps",
                table: "otps",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_otps",
                table: "otps");

            migrationBuilder.RenameTable(
                name: "otps",
                newName: "otpEntities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_otpEntities",
                table: "otpEntities",
                column: "Id");
        }
    }
}
