using LoginAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using System;

namespace LoginAPI.Data
{
    public class AppDbContext : DbContext
    {
        private readonly MyOptions _appSettings;

        public AppDbContext(IOptions<MyOptions> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_appSettings.ConnectionDB)
                .LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted })
                .EnableSensitiveDataLogging();
        }
    }
}
