using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunningAdvisor.Migrations
{
    /// <inheritdoc />
    public partial class AddBeginnerRunningData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeginnerRunningData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time3k = table.Column<TimeSpan>(type: "time", nullable: false),
                    AvgHeartRate3k = table.Column<int>(type: "int", nullable: false),
                    Time5k = table.Column<TimeSpan>(type: "time", nullable: false),
                    AvgHeartRate5k = table.Column<int>(type: "int", nullable: false),
                    Time10k = table.Column<TimeSpan>(type: "time", nullable: false),
                    AvgHeartRate10k = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeginnerRunningData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeginnerRunningData");
        }
    }
}
