using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class ProcessSample_ProcessElements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProcessId",
                table: "ProcessElementSamples",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProcessElementSamples_ProcessId",
                table: "ProcessElementSamples",
                column: "ProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessElementSamples_ProcessSamples_ProcessId",
                table: "ProcessElementSamples",
                column: "ProcessId",
                principalTable: "ProcessSamples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessElementSamples_ProcessSamples_ProcessId",
                table: "ProcessElementSamples");

            migrationBuilder.DropIndex(
                name: "IX_ProcessElementSamples_ProcessId",
                table: "ProcessElementSamples");

            migrationBuilder.DropColumn(
                name: "ProcessId",
                table: "ProcessElementSamples");
        }
    }
}
