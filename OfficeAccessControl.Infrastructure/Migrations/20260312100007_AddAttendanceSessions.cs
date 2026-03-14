using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeAccessControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAttendanceSessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InTime",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "OutTime",
                table: "Attendances");

            migrationBuilder.AddColumn<double>(
                name: "TotalMinutes",
                table: "Attendances",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Direction",
                table: "AccessLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AttendanceSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttendanceId = table.Column<int>(type: "int", nullable: false),
                    InTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OutTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceSessions_Attendances_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "Attendances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSessions_AttendanceId",
                table: "AttendanceSessions",
                column: "AttendanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendanceSessions");

            migrationBuilder.DropColumn(
                name: "TotalMinutes",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "Direction",
                table: "AccessLogs");

            migrationBuilder.AddColumn<DateTime>(
                name: "InTime",
                table: "Attendances",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OutTime",
                table: "Attendances",
                type: "datetime2",
                nullable: true);
        }
    }
}
