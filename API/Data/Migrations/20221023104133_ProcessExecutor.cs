using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class ProcessExecutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Processes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProcessId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProcessElementInstanseType = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessElements_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProcessId = table.Column<int>(type: "INTEGER", nullable: false),
                    OutElementId = table.Column<int>(type: "INTEGER", nullable: true),
                    InElementId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsMain = table.Column<bool>(type: "INTEGER", nullable: false),
                    Condition = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessConnections_ProcessElements_InElementId",
                        column: x => x.InElementId,
                        principalTable: "ProcessElements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessConnections_ProcessElements_OutElementId",
                        column: x => x.OutElementId,
                        principalTable: "ProcessElements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessConnections_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessParamEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParamRouteType = table.Column<int>(type: "INTEGER", nullable: false),
                    ParamType = table.Column<int>(type: "INTEGER", nullable: false),
                    Condition = table.Column<string>(type: "TEXT", nullable: true),
                    stringParam = table.Column<string>(type: "TEXT", nullable: true),
                    intParam = table.Column<int>(type: "INTEGER", nullable: false),
                    doubleParam = table.Column<double>(type: "REAL", nullable: false),
                    boolParam = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProcessElementEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    ProcessEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessParamEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessParamEntity_ProcessElements_ProcessElementEntityId",
                        column: x => x.ProcessElementEntityId,
                        principalTable: "ProcessElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessParamEntity_Processes_ProcessEntityId",
                        column: x => x.ProcessEntityId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessConnections_InElementId",
                table: "ProcessConnections",
                column: "InElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessConnections_OutElementId",
                table: "ProcessConnections",
                column: "OutElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessConnections_ProcessId",
                table: "ProcessConnections",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessElements_ProcessId",
                table: "ProcessElements",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessParamEntity_ProcessElementEntityId",
                table: "ProcessParamEntity",
                column: "ProcessElementEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessParamEntity_ProcessEntityId",
                table: "ProcessParamEntity",
                column: "ProcessEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessConnections");

            migrationBuilder.DropTable(
                name: "ProcessParamEntity");

            migrationBuilder.DropTable(
                name: "ProcessElements");

            migrationBuilder.DropTable(
                name: "Processes");
        }
    }
}
