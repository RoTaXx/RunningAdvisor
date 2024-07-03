﻿using Microsoft.EntityFrameworkCore;
using RunningAdvisor.Models.Entities;

namespace RunningAdvisor.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        public DbSet<RunningData> RunningData { get; set; }
        public DbSet<BeginnerRunningData> BeginnerRunningData { get; set; }
    }
}
