﻿using Microsoft.EntityFrameworkCore;
using VissmaFlow.Core.Models.AccessControl;
using VissmaFlow.Core.Models.Administration;
using VissmaFlow.Core.Models.Communication;
using VissmaFlow.Core.Models.Event;
using VissmaFlow.Core.Models.Indication;
using VissmaFlow.Core.Models.Logging;
using VissmaFlow.Core.Models.Parameters;
using VissmaFlow.Core.Models.SingleMeasures;
using VissmaFlow.Core.Models.Trends;
using VissmaFlow.Core.Models.ZondMeasuring;

namespace VissmaFlow.Core.Infrastructure.DataAccess
{
    public class VissmaDbContext:DbContext
    {
        public DbSet<RtkUnit> RtkUnits => Set<RtkUnit>();
        public DbSet<PcSettings> PcSettings => Set<PcSettings>();
        public DbSet<ParameterBase> ParameterBases  => Set<ParameterBase>();
        public DbSet<CommSettings> CommSettings => Set<CommSettings>();
        public DbSet<User> Users => Set<User>();        
        public DbSet<Event> Events => Set<Event>();
        public DbSet<SingleMeasureSettings> SingleMeasureSettingses => Set<SingleMeasureSettings>();
        public DbSet<TrendSettings> TrendSettings => Set<TrendSettings>();
        public DbSet<ZondMeasureSettings> ZondMeasureSettingses => Set<ZondMeasureSettings>();

        public DbSet<IndicationCell> IndicationCells => Set<IndicationCell>();

        public DbSet<Curve> Curves => Set<Curve>();
        public DbSet<LogSettings> LogSettingses => Set<LogSettings>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VissmaDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            #region Short
            modelBuilder.Entity<ParameterShort>().Property(entity => entity.Value)
            .HasColumnName("ValueAsShort");
            modelBuilder.Entity<ParameterShort>().Property(entity => entity.WriteValue)
        .HasColumnName("WriteValueAsShort");
            modelBuilder.Entity<ParameterShort>().Property(entity => entity.MinValue)
        .HasColumnName("MinValueAsShort");
            modelBuilder.Entity<ParameterShort>().Property(entity => entity.MaxValue)
        .HasColumnName("MaxValueAsShort");
            #endregion

            #region Ushort
            modelBuilder.Entity<ParameterUshort>().Property(entity => entity.Value)
            .HasColumnName("ValueAsUshort");
            modelBuilder.Entity<ParameterUshort>().Property(entity => entity.WriteValue)
        .HasColumnName("WriteValueAsUshort");
            modelBuilder.Entity<ParameterUshort>().Property(entity => entity.MinValue)
        .HasColumnName("MinValueAsUshort");
            modelBuilder.Entity<ParameterUshort>().Property(entity => entity.MaxValue)
        .HasColumnName("MaxValueAsUshort");
            #endregion

            #region Int
            modelBuilder.Entity<ParameterInt>().Property(entity => entity.Value)
            .HasColumnName("ValueAsInt");
            modelBuilder.Entity<ParameterInt>().Property(entity => entity.WriteValue)
        .HasColumnName("WriteValueAsInt");
            modelBuilder.Entity<ParameterInt>().Property(entity => entity.MinValue)
        .HasColumnName("MinValueAsInt");
            modelBuilder.Entity<ParameterInt>().Property(entity => entity.MaxValue)
        .HasColumnName("MaxValueAsInt");
            #endregion

            #region UInt
            modelBuilder.Entity<ParameterUint>().Property(entity => entity.Value)
            .HasColumnName("ValueAsUint");
            modelBuilder.Entity<ParameterUint>().Property(entity => entity.WriteValue)
        .HasColumnName("WriteValueAsUint");
            modelBuilder.Entity<ParameterUint>().Property(entity => entity.MinValue)
        .HasColumnName("MinValueAsUint");
            modelBuilder.Entity<ParameterUint>().Property(entity => entity.MaxValue)
        .HasColumnName("MaxValueAsUint");
            #endregion

            #region Bool
            modelBuilder.Entity<ParameterBool>().Property(entity => entity.Value)
            .HasColumnName("ValueAsBool");
            modelBuilder.Entity<ParameterBool>().Property(entity => entity.WriteValue)
        .HasColumnName("WriteValueAsBool");
            modelBuilder.Entity<ParameterBool>().Property(entity => entity.MinValue)
        .HasColumnName("MinValueAsBool");
            modelBuilder.Entity<ParameterBool>().Property(entity => entity.MaxValue)
        .HasColumnName("MaxValueAsBool");
            #endregion

            #region float
            modelBuilder.Entity<ParameterFloat>().Property(entity => entity.Value)
            .HasColumnName("ValueAsoFloat");
            modelBuilder.Entity<ParameterFloat>().Property(entity => entity.WriteValue)
        .HasColumnName("WriteValueAsFloat");
            modelBuilder.Entity<ParameterFloat>().Property(entity => entity.MinValue)
        .HasColumnName("MinValueAsFloat");
            modelBuilder.Entity<ParameterFloat>().Property(entity => entity.MaxValue)
        .HasColumnName("MaxValueAsFloat");
            #endregion

            #region double
            modelBuilder.Entity<ParameterDouble>().Property(entity => entity.Value)
            .HasColumnName("ValueAsoDouble");
            modelBuilder.Entity<ParameterDouble>().Property(entity => entity.WriteValue)
        .HasColumnName("WriteValueAsDouble");
            modelBuilder.Entity<ParameterDouble>().Property(entity => entity.MinValue)
        .HasColumnName("MinValueAsDouble");
            modelBuilder.Entity<ParameterDouble>().Property(entity => entity.MaxValue)
        .HasColumnName("MaxValueAsDouble");
            #endregion

            #region string
            modelBuilder.Entity<ParameterString>().Property(entity => entity.Value)
            .HasColumnName("ValueAsString");
            modelBuilder.Entity<ParameterString>().Property(entity => entity.WriteValue)
        .HasColumnName("WriteValueAsString");
            modelBuilder.Entity<ParameterString>().Property(entity => entity.MinValue)
        .HasColumnName("MinValueAsString");
            modelBuilder.Entity<ParameterString>().Property(entity => entity.MaxValue)
        .HasColumnName("MaxValueAsString");
            #endregion

            modelBuilder.Entity<SingleMeasureSettings>().HasOne(s => s.Source).WithMany().OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<SingleMeasurePoint>().HasOne(s=>s.AvgValue).WithMany().OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<SingleMeasurePoint>().HasOne(s => s.Destination).WithMany().OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Event>().HasOne(e => e.RtkUnit).WithMany().OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Event>().HasOne(e => e.Parameter).WithMany().OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<IndicationCell>().HasOne(c => c.RtkUnit).WithMany().OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<IndicationCell>().HasOne(c => c.Parameter).WithMany().OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<LogCell>().HasOne(l => l.RtkUnit).WithMany().OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<LogCell>().HasOne(l => l.Parameter).WithMany().OnDelete(DeleteBehavior.SetNull);

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
            
        }

        public VissmaDbContext()
        {
            
        }
    }
}
