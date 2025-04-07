using Microsoft.EntityFrameworkCore;
using MunicipalManagementSystem.Models;

namespace MunicipalManagementSystem.Data
{
    public class MunicipalDbContext : DbContext
    {
        public MunicipalDbContext(DbContextOptions<MunicipalDbContext> options) : base(options) { }

        public DbSet<Staff> Staff { get; set; }
        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
