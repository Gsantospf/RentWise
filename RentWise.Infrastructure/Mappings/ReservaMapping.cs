using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentWise.Core.Entidades;
using System.Reflection.Emit;
using static Azure.Core.HttpHeader;

namespace RentWise.Infrastructure.Mappings
{
    public class ReservaMapping : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Inicio)
                .IsRequired();

            builder.Property(r => r.Fim)
                .IsRequired();

            builder.Property(r => r.ValorTotal)
                .HasPrecision(18, 2);

            builder.Property(r => r.Status)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(r => r.Sala)
                .WithMany(s => s.Reservas)
                .HasForeignKey(r => r.SalaId);
        }
    }
}