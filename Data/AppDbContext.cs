using KeyValueIoT.Models;
using Microsoft.EntityFrameworkCore;

namespace KeyValueIoT.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        public DbSet<KeyValueModel> KeyValueRepo { get; set; }
    }
}
