using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentWise.Core.Entidades;

namespace RentWise.Infrastructure.Mappings
{
    public class SalaMapping : IEntityTypeConfiguration<Sala>
    {
        public void Configure(EntityTypeBuilder<Sala> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.PrecoPorHora)
                .HasColumnType("decimal(18,2)");

            // Relacionamento: Uma Sala tem muitas Reservas
            builder.HasMany(s => s.Reservas)
                .WithOne(r => r.Sala)
                .HasForeignKey(r => r.SalaId); 
        }
    }
}