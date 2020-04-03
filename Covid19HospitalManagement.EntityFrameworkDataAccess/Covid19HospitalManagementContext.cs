using Covid19HospitalManagement.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Covid19HospitalManagement.EntityFrameworkDataAccess
{
    public class Covid19HospitalManagementContext: DbContext
    {
        public DbSet<PatientPoco> PatientPoco { get; set; }

        public DbSet<HospitalPoco> HospitalPocos { get; set; }

        public Covid19HospitalManagementContext(DbContextOptions options): base(options)
        {
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            string _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            optionsBuilder.UseSqlServer(_connStr);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
