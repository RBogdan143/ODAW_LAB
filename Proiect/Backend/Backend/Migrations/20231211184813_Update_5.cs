using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Update_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produs_Cos_Cos_CumparaturiId",
                table: "Produs");

            migrationBuilder.AlterColumn<Guid>(
                name: "Cos_CumparaturiId",
                table: "Produs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Produs_Cos_Cos_CumparaturiId",
                table: "Produs",
                column: "Cos_CumparaturiId",
                principalTable: "Cos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produs_Cos_Cos_CumparaturiId",
                table: "Produs");

            migrationBuilder.AlterColumn<Guid>(
                name: "Cos_CumparaturiId",
                table: "Produs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Produs_Cos_Cos_CumparaturiId",
                table: "Produs",
                column: "Cos_CumparaturiId",
                principalTable: "Cos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
