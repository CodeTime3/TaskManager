using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace TaskManagerDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailConfirm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmailConfirmId",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "email_confirms",
                columns: table => new
                {
                    EmailConfirmId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EmailConfirmToken = table.Column<Guid>(type: "char(36)", nullable: false),
                    EmailConfirmCreateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_email_confirms", x => x.EmailConfirmId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_users_EmailConfirmId",
                table: "users",
                column: "EmailConfirmId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_email_confirms_EmailConfirmId",
                table: "users",
                column: "EmailConfirmId",
                principalTable: "email_confirms",
                principalColumn: "EmailConfirmId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_email_confirms_EmailConfirmId",
                table: "users");

            migrationBuilder.DropTable(
                name: "email_confirms");

            migrationBuilder.DropIndex(
                name: "IX_users_EmailConfirmId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "EmailConfirmId",
                table: "users");
        }
    }
}
