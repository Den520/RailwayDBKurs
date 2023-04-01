using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RailwayDBKurs.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Region> Regions { get; set; }
        public DbSet<Depot> Depot { get; set; }
        public DbSet<CommonRepairEvent> RepairEvents { get; set; }
        public DbSet<RepairRailwayEvent> RepairRailwayEvents { get; set; }
        public DbSet<RepairVanEvent> RepairVanEvents { get; set; }
        public DbSet<Van> Vans { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
