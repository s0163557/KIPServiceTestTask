using KIPServiceTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace KIPServiceTestTask
{
    public class ReportDBContext : DbContext
    {
        public ReportDBContext(DbContextOptions<ReportDBContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<QueryInfo> QueryInfos { get; set; }
    }
}
