using DesafioBackEnd.Infra.Core.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Infra.Core
{
    public class DesafioContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = Environment.GetEnvironmentVariable("DESAFIO_CONNECTION_STRING");
            ArgumentException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));

            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyMapConfiguration(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public virtual void ApplyMapConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CNHTypeMap());
            modelBuilder.ApplyConfiguration(new DeliveryManMap());
            modelBuilder.ApplyConfiguration(new MotorcycleMap());
            modelBuilder.ApplyConfiguration(new RentMap());
            modelBuilder.ApplyConfiguration(new RentPlanMap());
            modelBuilder.ApplyConfiguration(new Motorcycle2024Map());
        }
    }
}
