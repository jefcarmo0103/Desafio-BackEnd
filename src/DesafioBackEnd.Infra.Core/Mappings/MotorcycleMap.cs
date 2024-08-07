using DesafioBackEnd.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioBackEnd.Infra.Core.Mappings
{
    public class MotorcycleMap : IEntityTypeConfiguration<Motorcycle>
    {
        public void Configure(EntityTypeBuilder<Motorcycle> builder)
        {
            builder.ToTable("motorcycle", "public");

            builder.HasKey(x => x.Id)
                .HasName("pk_motorcycleudf");

            builder.Property(x => x.Id)
                .HasColumnName("motorcycleudf");

            builder.Property(x => x.Plate)
                .HasColumnName("plate");

            builder.Property(x => x.Year)
                .HasColumnName("year");

            builder.Property(x => x.Model)
                .HasColumnName("model");

            
        }
    }
}
