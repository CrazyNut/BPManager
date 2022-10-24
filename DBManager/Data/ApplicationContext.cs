using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.DBManager.Data
{
    public class ApplicationContext : DbContext
    {
        
        public DbSet<ProcessEntity> Processes {get;set;}
        public DbSet<ProcessElementEntity> ProcessElements {get;set;}
        public DbSet<ProcessElementConnectionEntity> ProcessConnections {get;set;}

        public ApplicationContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProcessElementEntity>()
                .HasMany(e => e.InConnections)
                .WithOne(e => e.InElement);
            modelBuilder.Entity<ProcessElementEntity>()
                .HasMany(e => e.OutConnections)
                .WithOne(e => e.OutElement);
            modelBuilder.Entity<ProcessElementEntity>()
            .Property(x => x.ProcessElementInstanseType)
            .IsRequired()
            .HasConversion(
                convertToProviderExpression: x => x.AssemblyQualifiedName,
                convertFromProviderExpression: x => Type.GetType(x));
            modelBuilder.Entity<ProcessElementConnectionEntity>()
                .HasOne(c => c.OutElement)
                .WithMany(e => e.OutConnections);
            modelBuilder.Entity<ProcessElementConnectionEntity>()
                .HasOne(c => c.InElement)
                .WithMany(e => e.InConnections);

            base.OnModelCreating(modelBuilder);
        }
    }
}