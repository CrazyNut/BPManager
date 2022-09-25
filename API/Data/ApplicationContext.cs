using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.ProcessExecutor;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationContext : DbContext
    {
        
        public DbSet<ProcessSample> ProcessSamples {get;set;}
        public DbSet<ProcessElementSample> ProcessElementSamples {get;set;}
        public DbSet<ProcessElementConnection> ProcessElementConnections {get;set;}

        public ApplicationContext(DbContextOptions options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProcessElementSample>()
                .HasMany(e => e.InConnections)
                .WithOne(e => e.InElement);
            modelBuilder.Entity<ProcessElementSample>()
                .HasMany(e => e.OutConnections)
                .WithOne(e => e.OutElement);
            modelBuilder.Entity<ProcessElementSample>()
            .Property(x => x.ProcessElementInstanseType)
            .IsRequired()
            .HasConversion(
                convertToProviderExpression: x => x.AssemblyQualifiedName,
                convertFromProviderExpression: x => Type.GetType(x));
            base.OnModelCreating(modelBuilder);
        }
    }
}