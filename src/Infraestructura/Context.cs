///////////////////////////////////////////////////////////
//  Context.cs
//  Implementation of the Class Context
//  Generated by Enterprise Architect
//  Created on:      12-feb.-2024 16:47:18
//  Original author: pmmoy
///////////////////////////////////////////////////////////


using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Infraestructura {
	public class Context : DbContext
  {

		public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Vehiculo> Vehiculos { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<MedioPago> MediosPagos { get; set; }
    public DbSet<Itinerario> Itinerarios { get; set; }

    /// 
    /// <param name="options"></param>
    public Context(DbContextOptions<Context> options) : base(options)
    {
      var vehiculos = new Vehiculo[]{
      new Vehiculo { Ano = 2002, Marca = "FIAT", Modelo = "Uno", TarifaDia = 100.50, Serial = "ADT-" + DateTime.Now.Microsecond.ToString() },
      new Vehiculo { Ano = 2005, Marca = "FORD", Modelo = "Fieta power", TarifaDia = 85.50, Serial = "ADT-" + DateTime.Now.Microsecond.ToString()  },
      new Vehiculo { Ano = 2010, Marca = "CHEVROLET", Modelo = "Malibu", TarifaDia = 200.75, Serial = "XHY-" + DateTime.Now.Microsecond.ToString() },
      new Vehiculo { Ano = 2012, Marca = "TOYOTA", Modelo = "Corolla", TarifaDia = 325.50, Serial = "RTY-" + DateTime.Now.Microsecond.ToString()  }
      };

      this.Set<Vehiculo>().AddRange(vehiculos);
      this.SaveChanges();
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