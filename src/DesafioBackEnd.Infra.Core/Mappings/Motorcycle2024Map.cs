using DesafioBackEnd.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Infra.Core.Mappings
{
    public class Motorcycle2024Map : IEntityTypeConfiguration<Motorcycle2024>
    {
        public void Configure(EntityTypeBuilder<Motorcycle2024> builder)
        {
            builder.ToTable("motorcycle2024", "public");

            builder.HasKey(x => x.Id)
                .HasName("pk_motorcycle2024udf");

            builder.Property(x => x.Id)
                .HasColumnName("motorcycle2024udf");

            builder.Property(x => x.MotorcycleId)
                .HasColumnName("motorcycleudf");

        }
    }
}
