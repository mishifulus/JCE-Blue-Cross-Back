using JCEBlueCross.Models;
using Microsoft.EntityFrameworkCore;

namespace JCEBlueCross.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base (options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Provider> Providers { get; set; }

        public DbSet<Payor> Payor { get; set; }

        public DbSet<Error> Errors { get; set; }

        public DbSet<PayorError> PayorErrors { get; set; }

        public DbSet<Condition> Conditions { get; set; }

        public DbSet<Claim> Claims { get; set; }

        public DbSet<JCEBlueCross.Models.Condition> Condition { get; set; } = default!;
    }
}
