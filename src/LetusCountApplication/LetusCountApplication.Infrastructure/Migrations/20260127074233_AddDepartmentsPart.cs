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
                name: "CashMashines",
                schema: "dep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Serial = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashMashines", x => x.Id);
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
                name: "CashCashMashines",
                schema: "dep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CashId = table.Column<int>(type: "integer", nullable: false),
                    CashMashineId = table.Column<int>(type: "integer", nullable: false),
                    StartWorking = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndWorking = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashCashMashines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashCashMashines_CashMashines_CashMashineId",
                        column: x => x.CashMashineId,
                        principalSchema: "dep",
                        principalTable: "CashMashines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashCashMashines_Cashes_CashId",
                        column: x => x.CashId,
                        principalSchema: "dep",
                        principalTable: "Cashes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashCashMashines_CashId",
                schema: "dep",
                table: "CashCashMashines",
                column: "CashId");

            migrationBuilder.CreateIndex(
                name: "IX_CashCashMashines_CashMashineId",
                schema: "dep",
                table: "CashCashMashines",
                column: "CashMashineId");

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
                name: "CashCashMashines",
                schema: "dep");

            migrationBuilder.DropTable(
                name: "CashMashines",
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
