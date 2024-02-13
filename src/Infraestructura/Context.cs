///////////////////////////////////////////////////////////
//  Context.cs
//  Implementation of the Class Context
//  Generated by Enterprise Architect
//  Created on:      12-feb.-2024 16:47:18
//  Original author: pmmoy
///////////////////////////////////////////////////////////


using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura {
	public class Context : DbContext
  {

		public DbSet<Cliente> Clientes;
		public DbSet<Vehiculo> Vehiculos;
		public DbSet<Reserva> Reservas;
		public DbSet<MedioPago> MediosPagos;
		public DbSet<Itinerario> Itinerarios;

		/// 
		/// <param name="options"></param>
		public Context(DbContextOptions<Context> options) : base(options)
    {

		}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseInMemoryDatabase("MilerRentaCars");
    }

  }//end Context

}//end namespace Infraestructura