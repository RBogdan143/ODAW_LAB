using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Update_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discount_Cos_Cos_CumparaturiId",
                table: "Discount");

            migrationBuilder.DropIndex(
                name: "IX_Discount_Cos_CumparaturiId",
                table: "Discount");

            migrationBuilder.AlterColumn<Guid>(
                name: "Cos_CumparaturiId",
                table: "Discount",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Cos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Discount_Cos_CumparaturiId",
                table: "Discount",
                column: "Cos_CumparaturiId",
                unique: true,
                filter: "[Cos_CumparaturiId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cos_UserId",
                table: "Cos",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cos_Users_UserId",
                table: "Cos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_Cos_Cos_CumparaturiId",
                table: "Discount",
                column: "Cos_CumparaturiId",
                principalTable: "Cos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cos_Users_UserId",
                table: "Cos");

            migrationBuilder.DropForeignKey(
                name: "FK_Discount_Cos_Cos_CumparaturiId",
                table: "Discount");

            migrationBuilder.DropIndex(
                name: "IX_Discount_Cos_CumparaturiId",
                table: "Discount");

            migrationBuilder.DropIndex(
                name: "IX_Cos_UserId",
                table: "Cos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cos");

            migrationBuilder.AlterColumn<Guid>(
                name: "Cos_CumparaturiId",
                table: "Discount",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discount_Cos_CumparaturiId",
                table: "Discount",
                column: "Cos_CumparaturiId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_Cos_Cos_CumparaturiId",
                table: "Discount",
                column: "Cos_CumparaturiId",
                principalTable: "Cos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
