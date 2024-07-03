using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunningAdvisor.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RunningData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time10k = table.Column<TimeSpan>(type: "time", nullable: false),
                    Time21k = table.Column<TimeSpan>(type: "time", nullable: false),
                    Time42k = table.Column<TimeSpan>(type: "time", nullable: false),
                    AvgHeartRate10k = table.Column<int>(type: "int", nullable: false),
                    AvgHeartRate21k = table.Column<int>(type: "int", nullable: false),
                    AvgHeartRate42k = table.Column<int>(type: "int", nullable: false),
                    Cadence = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunningData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RunningData");
        }
    }
}
