using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adicionaDomainUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_AspNetUsers_IdentityUserId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "DomainUsers");

            migrationBuilder.RenameIndex(
                name: "IX_User_IdentityUserId",
                table: "DomainUsers",
                newName: "IX_DomainUsers_IdentityUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DomainUsers",
                table: "DomainUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DomainUsers_AspNetUsers_IdentityUserId",
                table: "DomainUsers",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DomainUsers_AspNetUsers_IdentityUserId",
                table: "DomainUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DomainUsers",
                table: "DomainUsers");

            migrationBuilder.RenameTable(
                name: "DomainUsers",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_DomainUsers_IdentityUserId",
                table: "User",
                newName: "IX_User_IdentityUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_AspNetUsers_IdentityUserId",
                table: "User",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
