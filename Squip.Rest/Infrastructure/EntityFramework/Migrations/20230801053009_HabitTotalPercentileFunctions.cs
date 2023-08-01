using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Squip.Rest.Migrations
{
    public partial class HabitTotalPercentileFunctions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var dropFunctionQuery = "drop function if exists get_daily_habit_total_by_percentile";
            migrationBuilder.Sql(dropFunctionQuery);

            var habitTotalsByDayWithTimeZoneQuery =
                File.ReadAllText("Infrastructure/EntityFramework/Functions/habit-totals-by-day-with-time-zone.sql");
            migrationBuilder.Sql(habitTotalsByDayWithTimeZoneQuery);

            var habitTotalByPercentileQuery = File.ReadAllText("Infrastructure/EntityFramework/Functions/habit-total-by-percentile.sql");
            migrationBuilder.Sql(habitTotalByPercentileQuery);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop function if exists habit_totals_by_day_with_time_zone");
            migrationBuilder.Sql("drop function if exists habit_total_by_percentile");
        }
    }
}
