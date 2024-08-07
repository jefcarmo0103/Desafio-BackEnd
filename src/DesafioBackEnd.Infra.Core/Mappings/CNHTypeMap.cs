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
    public class CNHTypeMap : IEntityTypeConfiguration<CNHType>
    {
        public void Configure(EntityTypeBuilder<CNHType> builder)
        {
            builder.ToTable("typecnh", "public");

            builder.HasKey(x => x.Id)
                .HasName("pk_typecnhudf");

            builder.Property(x => x.Id)
                .HasColumnName("typecnhudf");

            builder.Property(x => x.Name)
                .HasColumnName("description");

            builder.Property(x => x.QualifiedToRentMotorcyle)
                .HasColumnName("qualifiedrentmotorcyle");

            builder.Property(x => x.Active)
                .HasColumnName("active");
            
        }
    }
}
