using Microsoft.EntityFrameworkCore;
using RentWise.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentWise.Infrastructure.Context
{
    public class RentWiseDbContext : DbContext
    {
        public RentWiseDbContext(DbContextOptions<RentWiseDbContext> options) : base(options) { }

        public DbSet<Sala> Salas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RentWiseDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
