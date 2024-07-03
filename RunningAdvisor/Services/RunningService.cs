using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RunningAdvisor.Data;
using RunningAdvisor.Models.Entities;

namespace RunningAdvisor.Services
{
    public class RunningService
    {
        private readonly ApplicationDbContext _context;

        public RunningService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RunningData>> GetAllRecordsAsync()
        {
            return await _context.RunningData.ToListAsync();
        }

        public async Task<RunningData> GetRecordByIdAsync(int id)
        {
            return await _context.RunningData.FindAsync(id);
        }

        public async Task AddRecordAsync(RunningData record)
        {
            _context.RunningData.Add(record);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRecordAsync(RunningData record)
        {
            _context.RunningData.Update(record);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRecordAsync(int id)
        {
            var record = await _context.RunningData.FindAsync(id);
            if (record != null)
            {
                _context.RunningData.Remove(record);
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

            bool isAdvanced = record.Time10k < TimeSpan.FromMinutes(50) && record.Time21k < TimeSpan.FromMinutes(90) && record.Time42k < TimeSpan.FromMinutes(200);

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
                                plan.AppendLine($"Day {day}: Long run, 25+ km at a pace above 5 mins/km");
                            }
                            else
                            {
                                plan.AppendLine($"Day {day}: Intervals at a pace under 3 mins/km");
                            }
                        }
                        else
                        {
                            if (day == 7)
                            {
                                plan.AppendLine($"Day {day}: Long run, around 20 km at a pace above 5.5 mins/km");
                            }
                            else
                            {
                                plan.AppendLine($"Day {day}: Easy run at a pace above 5.5 mins/km");
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
