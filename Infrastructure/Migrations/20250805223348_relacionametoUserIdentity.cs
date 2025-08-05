using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class relacionametoUserIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_DomainUsers_DomainUserId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_DomainUsers_AspNetUsers_IdentityUserId",
                table: "DomainUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ItServiceProviders_DomainUsers_DomainUserId",
                table: "ItServiceProviders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DomainUsers",
                table: "DomainUsers");

            migrationBuilder.RenameTable(
                name: "DomainUsers",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "DomainUserId",
                table: "ItServiceProviders",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ItServiceProviders_DomainUserId",
                table: "ItServiceProviders",
                newName: "IX_ItServiceProviders_UserId");

            migrationBuilder.RenameColumn(
                name: "DomainUserId",
                table: "Companies",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_DomainUserId",
                table: "Companies",
                newName: "IX_Companies_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DomainUsers_IdentityUserId",
                table: "Users",
                newName: "IX_Users_IdentityUserId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_UserId",
                table: "Companies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItServiceProviders_Users_UserId",
                table: "ItServiceProviders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AspNetUsers_IdentityUserId",
                table: "Users",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_UserId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_ItServiceProviders_Users_UserId",
                table: "ItServiceProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AspNetUsers_IdentityUserId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "DomainUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ItServiceProviders",
                newName: "DomainUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ItServiceProviders_UserId",
                table: "ItServiceProviders",
                newName: "IX_ItServiceProviders_DomainUserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Companies",
                newName: "DomainUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_UserId",
                table: "Companies",
                newName: "IX_Companies_DomainUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_IdentityUserId",
                table: "DomainUsers",
                newName: "IX_DomainUsers_IdentityUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DomainUsers",
                table: "DomainUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_DomainUsers_DomainUserId",
                table: "Companies",
                column: "DomainUserId",
                principalTable: "DomainUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DomainUsers_AspNetUsers_IdentityUserId",
                table: "DomainUsers",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItServiceProviders_DomainUsers_DomainUserId",
                table: "ItServiceProviders",
                column: "DomainUserId",
                principalTable: "DomainUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
