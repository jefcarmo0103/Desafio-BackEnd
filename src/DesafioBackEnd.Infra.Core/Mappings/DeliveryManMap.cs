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
    public class DeliveryManMap : IEntityTypeConfiguration<DeliveryMan>
    {
        public void Configure(EntityTypeBuilder<DeliveryMan> builder)
        {
            builder.ToTable("deliveryman", "public");

            builder.HasKey(x => x.Id)
                .HasName("pk_deliverymanudf");

            builder.Property(x => x.Id)
                .HasColumnName("deliverymanudf");

            builder.Property(x => x.Name)
                .HasColumnName("name");

            builder.Property(x => x.CNPJ)
                .HasColumnName("cnpj");

            builder.Property(x => x.BirthdayDate)
                .HasColumnName("birthdaydate");

            builder.Property(x => x.NumberCNH)
                .HasColumnName("numbercnh");

            builder.Property(x => x.TypeCNHId)
                .HasColumnName("typecnhudf");

            builder.Property(x => x.ImageCNH)
                .HasColumnName("imagecnh");

            builder.HasOne(x => x.TypeCNH)
                .WithMany()
                .HasForeignKey(x => x.TypeCNHId);
        }
    }
}
