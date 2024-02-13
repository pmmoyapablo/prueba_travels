///////////////////////////////////////////////////////////
//  Reserva.cs
//  Implementation of the Class Reserva
//  Generated by Enterprise Architect
//  Created on:      12-feb.-2024 14:58:13
//  Original author: pmmoy
///////////////////////////////////////////////////////////

using System;
using System.Diagnostics;

namespace Dominio {
	public class Reserva : Entidad {

    private Cliente clienteInv;
    private Vehiculo vehiculoInv;
    private DateTime fechaInicio;
    private DateTime fechaFin;
    private double montoTotal;
    private int estatus;
    private MedioPago pago;

    public Cliente ClienteInv { get => clienteInv; set { ExcepcionDominio.LanzarCuando(value == null, "Cliente inv�lido"); clienteInv = value; } }
    public Vehiculo VehiculoInv { get => vehiculoInv; set { ExcepcionDominio.LanzarCuando(value == null, "Vehiculo inv�lido"); vehiculoInv = value; } }
    public DateTime FechaInicio { get => fechaInicio; set { ExcepcionDominio.LanzarCuando(value < DateTime.Now, "Fecha Inicio inv�lida"); fechaInicio = value; } }
    public DateTime FechaFin { get => fechaFin; set { ExcepcionDominio.LanzarCuando(value < DateTime.Now.AddDays(1), "Fecha Fin inv�lida"); fechaFin = value; } }
    public double MontoTotal { get => montoTotal; set { ExcepcionDominio.LanzarCuando(value < 0.00, "Monto Total inv�lido"); montoTotal = value; } }
    public int Estatus { get => estatus; set { ExcepcionDominio.LanzarCuando(value > (int)EstadosReserva.Anulada && value < 0, "Estatus inv�lido"); estatus = value; } }
    public bool PagoDestino { get; set;  }
		public MedioPago Pago { get => pago; set { ExcepcionDominio.LanzarCuando(value == null, "Medio de Pago inv�lido"); pago = value; } }

    public Reserva(){

		}

		public void CalcularMonto(){

      TimeSpan difFechas = FechaFin - FechaInicio;

      this.MontoTotal = VehiculoInv.TarifaDia * difFechas.Days;
    }

	}//end Reserva

}//end namespace Dominio