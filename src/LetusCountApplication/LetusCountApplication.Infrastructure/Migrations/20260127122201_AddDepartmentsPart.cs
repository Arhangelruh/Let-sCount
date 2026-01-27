using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LetusCountApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentsPart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dep");

            migrationBuilder.CreateTable(
                name: "CashMachines",
                schema: "dep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Serial = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashMachines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "dep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Address = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cashes",
                schema: "dep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cashes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cashes_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "dep",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CashCashMachines",
                schema: "dep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CashId = table.Column<int>(type: "integer", nullable: false),
                    CashMachineId = table.Column<int>(type: "integer", nullable: false),
                    StartWorking = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndWorking = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashCashMachines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashCashMachines_CashMachines_CashMachineId",
                        column: x => x.CashMachineId,
                        principalSchema: "dep",
                        principalTable: "CashMachines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashCashMachines_Cashes_CashId",
                        column: x => x.CashId,
                        principalSchema: "dep",
                        principalTable: "Cashes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashCashMachines_CashId",
                schema: "dep",
                table: "CashCashMachines",
                column: "CashId");

            migrationBuilder.CreateIndex(
                name: "IX_CashCashMachines_CashMachineId",
                schema: "dep",
                table: "CashCashMachines",
                column: "CashMachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Cashes_DepartmentId",
                schema: "dep",
                table: "Cashes",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashCashMachines",
                schema: "dep");

            migrationBuilder.DropTable(
                name: "CashMachines",
                schema: "dep");

            migrationBuilder.DropTable(
                name: "Cashes",
                schema: "dep");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "dep");
        }
    }
}
