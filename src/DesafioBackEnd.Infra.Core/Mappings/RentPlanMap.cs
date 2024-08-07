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
    public class RentPlanMap : IEntityTypeConfiguration<RentPlan>
    {
        public void Configure(EntityTypeBuilder<RentPlan> builder)
        {
            builder.ToTable("rentplan", "public");

            builder.HasKey(x => x.Id)
                .HasName("pk_rentplanddf");

            builder.Property(x => x.Id)
                .HasColumnName("rentplanudf");

            builder.Property(x => x.QtyDays)
                .HasColumnName("quantitydays");

            builder.Property(x => x.Description)
                .HasColumnName("description");

            builder.Property(x => x.Price)
                .HasColumnName("price");

            builder.Property(x => x.HasFeeForAnticipation)
                .HasColumnName("hasfeeforanticipation");

            builder.Property(x => x.AnticipationFeePercentage)
                .HasColumnName("anticipationfeepercentage");

            builder.Property(x => x.DelayFeePrice)
                .HasColumnName("delayfeeprice");

            builder.Property(x => x.Active)
                .HasColumnName("active");
        }
    }
}
