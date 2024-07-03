using Microsoft.EntityFrameworkCore;
using RunningAdvisor.Data;
using RunningAdvisor.Models.Entities;
using System.Text;

namespace RunningAdvisor.Services
{
    public class BeginnerRunningService
    {
        private readonly ApplicationDbContext _context;

        public BeginnerRunningService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BeginnerRunningData>> GetAllRecordsAsync()
        {
            return await _context.BeginnerRunningData.ToListAsync();
        }

        public async Task<BeginnerRunningData> GetRecordByIdAsync(int id)
        {
            return await _context.BeginnerRunningData.FindAsync(id);
        }

        public async Task AddRecordAsync(BeginnerRunningData record)
        {
            _context.BeginnerRunningData.Add(record);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRecordAsync(BeginnerRunningData record)
        {
            _context.BeginnerRunningData.Update(record);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRecordAsync(int id)
        {
            var record = await _context.BeginnerRunningData.FindAsync(id);
            if (record != null)
            {
                _context.BeginnerRunningData.Remove(record);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> GenerateIndividualPlanAsync(int id)
        {
            var record = await GetRecordByIdAsync(id);
            if (record == null)
            {
                throw new Exception("Record not found");
            }

            bool isAdvanced = record.Time3k < TimeSpan.FromMinutes(15) && record.Time5k < TimeSpan.FromMinutes(30) && record.Time10k < TimeSpan.FromHours(1);

            StringBuilder plan = new StringBuilder();

            plan.AppendLine($"Training Plan for Record {id}:");

            for (int week = 1; week <= 6; week++)
            {
                plan.AppendLine($"Week {week}:");

                for (int day = 1; day <= 7; day++)
                {
                    if (day % 2 == 0) // Rest day every other day
                    {
                        plan.AppendLine($"Day {day}: Rest day");
                    }
                    else
                    {
                        if (isAdvanced)
                        {
                            if (day == 7)
                            {
                                plan.AppendLine($"Day {day}: Long run, 15+ km at a pace above 6 mins/km");
                            }
                            else
                            {
                                plan.AppendLine($"Day {day}: Intervals at a pace under 5 mins/km");
                            }
                        }
                        else
                        {
                            if (day == 7)
                            {
                                plan.AppendLine($"Day {day}: Long run, around 10 km at a pace above 6.5 mins/km");
                            }
                            else
                            {
                                plan.AppendLine($"Day {day}: Easy run at a pace above 6.5 mins/km");
                            }
                        }
                    }
                }
                plan.AppendLine();
            }

            return plan.ToString();
        }
    }
}
