using Microsoft.EntityFrameworkCore;
using VissmaFlow.Core.Models.Communication;
using VissmaFlow.Core.Models.Parameters;

namespace VissmaFlow.Core.Infrastructure.DataAccess
{
    public class VissmaDbContext:DbContext
    {

        public DbSet<ParameterBase> ParameterBases  => Set<ParameterBase>();
        public DbSet<CommSettings> CommSettings => Set<CommSettings>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VissmaDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlite("Data Source=vissma_flow.db");
        }

        public VissmaDbContext(DbContextOptions<VissmaDbContext> options)
    : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public VissmaDbContext()
        {
            
        }
    }
}
