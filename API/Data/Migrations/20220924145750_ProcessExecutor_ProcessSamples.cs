using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class ProcessExecutor_ProcessSamples : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessElementSamples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProcessElementInstanseType = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessElementSamples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessSamples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessSamples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessElementConnections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OutElementId = table.Column<Guid>(type: "TEXT", nullable: false),
                    InElementId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsMain = table.Column<bool>(type: "INTEGER", nullable: false),
                    Condition = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessElementConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessElementConnections_ProcessElementSamples_InElementId",
                        column: x => x.InElementId,
                        principalTable: "ProcessElementSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessElementConnections_ProcessElementSamples_OutElementId",
                        column: x => x.OutElementId,
                        principalTable: "ProcessElementSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessParam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ParamRouteType = table.Column<int>(type: "INTEGER", nullable: false),
                    ParamType = table.Column<int>(type: "INTEGER", nullable: false),
                    Condition = table.Column<string>(type: "TEXT", nullable: true),
                    stringParam = table.Column<string>(type: "TEXT", nullable: true),
                    intParam = table.Column<int>(type: "INTEGER", nullable: true),
                    doubleParam = table.Column<double>(type: "REAL", nullable: true),
                    boolParam = table.Column<bool>(type: "INTEGER", nullable: true),
                    ProcessElementSampleId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ProcessSampleId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessParam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessParam_ProcessElementSamples_ProcessElementSampleId",
                        column: x => x.ProcessElementSampleId,
                        principalTable: "ProcessElementSamples",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessParam_ProcessSamples_ProcessSampleId",
                        column: x => x.ProcessSampleId,
                        principalTable: "ProcessSamples",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessElementConnections_InElementId",
                table: "ProcessElementConnections",
                column: "InElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessElementConnections_OutElementId",
                table: "ProcessElementConnections",
                column: "OutElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessParam_ProcessElementSampleId",
                table: "ProcessParam",
                column: "ProcessElementSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessParam_ProcessSampleId",
                table: "ProcessParam",
                column: "ProcessSampleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessElementConnections");

            migrationBuilder.DropTable(
                name: "ProcessParam");

            migrationBuilder.DropTable(
                name: "ProcessElementSamples");

            migrationBuilder.DropTable(
                name: "ProcessSamples");
        }
    }
}
