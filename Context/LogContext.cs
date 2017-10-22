using System.Data.Entity;
using WebApplication3.Entity;

namespace WebApplication3.Context
{
    class LogContext : DbContext
    {
        public LogContext()
            : base("DbConnection")
        { }

        public DbSet<LogItem> Log { get; set; }
    }
}