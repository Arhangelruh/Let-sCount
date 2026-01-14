using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LetusCountService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "opr");

            migrationBuilder.CreateTable(
                name: "Operations",
                schema: "opr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MachineSerial = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationUnits",
                schema: "opr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Currency = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    OperationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationUnits_Operations_OperationId",
                        column: x => x.OperationId,
                        principalSchema: "opr",
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Banknotes",
                schema: "opr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DenomName = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Value = table.Column<int>(type: "integer", maxLength: 32, nullable: false),
                    SerialNumber = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    OperationUnitId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banknotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banknotes_OperationUnits_OperationUnitId",
                        column: x => x.OperationUnitId,
                        principalSchema: "opr",
                        principalTable: "OperationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banknotes_OperationUnitId",
                schema: "opr",
                table: "Banknotes",
                column: "OperationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationUnits_OperationId",
                schema: "opr",
                table: "OperationUnits",
                column: "OperationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banknotes",
                schema: "opr");

            migrationBuilder.DropTable(
                name: "OperationUnits",
                schema: "opr");

            migrationBuilder.DropTable(
                name: "Operations",
                schema: "opr");
        }
    }
}
