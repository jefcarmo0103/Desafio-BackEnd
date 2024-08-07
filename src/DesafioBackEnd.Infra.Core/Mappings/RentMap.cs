using DesafioBackEnd.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Infra.Core.Mappings
{
    public class RentMap : IEntityTypeConfiguration<Rent>
    {
        public void Configure(EntityTypeBuilder<Rent> builder)
        {
            builder.ToTable("rent", "public");

            builder.HasKey(x => x.Id)
                .HasName("pk_rentudf");

            builder.Property(x => x.Id)
                .HasColumnName("rentudf");

            builder.Property(x => x.Begin)
                .HasColumnName("begindate");

            builder.Property(x => x.End)
                .HasColumnName("enddate");

            builder.Property(x => x.ExpectedEnd)
                .HasColumnName("expectedenddate");

            builder.Property(x => x.CreationDate)
                .HasColumnName("creationdate");

            builder.Property(x => x.RentValue)
                .HasColumnName("finalprice");

            builder.Property(x => x.PlanId)
                .HasColumnName("rentplanudf");

            builder.Property(x => x.DeliveryManId)
                .HasColumnName("deliverymanudf");

            builder.Property(x => x.MotorcycleId)
                .HasColumnName("motorcycleudf");

            builder.HasOne(x => x.Plan)
                .WithMany()
                .HasForeignKey(x => x.PlanId);

            builder.HasOne(x => x.DeliveryMan)
                .WithMany()
                .HasForeignKey(x => x.DeliveryManId);

            builder.HasOne(x => x.Motorcycle)
                .WithMany()
                .HasForeignKey(x => x.MotorcycleId);

        }
    }
}
