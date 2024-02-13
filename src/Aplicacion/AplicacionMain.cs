///////////////////////////////////////////////////////////
//  Aplicacion.cs
//  Implementation of the Class Aplicacion
//  Generated by Enterprise Architect
//  Created on:      12-feb.-2024 15:00:58
//  Original author: pmmoy
///////////////////////////////////////////////////////////


using Azure;
using Dominio;
using Infraestructura;
using System;

namespace Aplicacion {
	public class AplicacionMain {

		private readonly IClienteRepositorio _clienteRepositorio;
		private readonly IVehiculoRepositorio _vehiculoRepositorio;
		private readonly IReservaRepositorio _reservaRepositorio;
		private readonly IMedioPagoRepositorio _medioPagoRepositorio;

		public AplicacionMain(
		IClienteRepositorio clienteRepositorio,
		IVehiculoRepositorio vehiculoRepositorio,
		IReservaRepositorio reservaRepositorio,
		IMedioPagoRepositorio medioPagoRepositorio){
   
			_clienteRepositorio = clienteRepositorio;
			_vehiculoRepositorio = vehiculoRepositorio;
			_reservaRepositorio = reservaRepositorio;
			_medioPagoRepositorio = medioPagoRepositorio;

    }

		public Repuesta<Vehiculo[]> ListarVehiculos(){

      var response = new Repuesta<Vehiculo[]>();
      try
      {
        var vehiculos = _vehiculoRepositorio.ListarTodos().Where(v => v.IsDisponible = true).ToArray();
        
        response.Dato = vehiculos;

        if (response.Dato != null)
        {
          response.IsValida = true;
          response.Mensaje = "Consulta Exitosa!!!";
        }

      }
      catch (Exception ex)
      {
        response.Mensaje = ex.Message;
      }

      return response;
    }

		/// 
		/// <param name="cliente"></param>
		public Repuesta<int> ConsignarDatosCliente(Cliente cliente){

      var response = new Repuesta<int>();
      try
      {
        //Validate on Domain
        var clientePersiter = new Cliente();
				clientePersiter.Id = cliente.Id;
        clientePersiter.Nombres = cliente.Nombres;
        clientePersiter.Documento = cliente.Documento;
        clientePersiter.Edad = cliente.Edad;
        clientePersiter.Direccion = cliente.Direccion;
        clientePersiter.Ciudad = cliente.Ciudad;
        clientePersiter.Email = cliente.Email;
        clientePersiter.Telefono = cliente.Telefono;

        //Persitencia
        _clienteRepositorio.Crear(clientePersiter);

        response.Dato = 200;
        response.IsValida = true;
        response.Mensaje = "Registro Exitoso!!!";

      }
      catch (Exception e)
      {
        response.Mensaje = e.Message;
      }
      return response;
    }

		/// 
		/// <param name="vehiculos"></param>
		public Repuesta<int> DefinirPreferencias(String clienteId, Vehiculo[] vehiculos){

      var response = new Repuesta<int>();
      try
      {

        //Busqueda
        var cliente = _clienteRepositorio.Detallar(clienteId);

        if (cliente == null)
        {
          response.Dato = 400;
          response.IsValida = false;
          response.Mensaje = "No se encontro el cliente con el identidicador: " + clienteId;
        }
        else
        {
          cliente.Preferencias = vehiculos;
          response.Dato = 200;
          response.IsValida = true;
          response.Mensaje = "Consulta Exitosa!!!";
        }

      }
      catch (Exception e)
      {
        response.Mensaje = e.Message;
      }
      return response;
    }

		/// 
		/// <param name="mediosPagos"></param>
		/// <param name="vehiculo"></param>
		/// <param name="cliente"></param>
		public Repuesta<Reserva> ReservarVehiculo(Vehiculo vehiculo, Cliente cliente, MedioPago medioPago, DateTime desde, DateTime hasta)
    {
      var response = new Repuesta<Reserva>();
      try
      {
        //Validate on Domain
        var newReserva = new Reserva();
        vehiculo.IsDisponible = false;
        newReserva.VehiculoInv = vehiculo;
        newReserva.ClienteInv = cliente;
        newReserva.Pago = medioPago;
        newReserva.FechaInicio = desde;
        newReserva.FechaFin = hasta;
        newReserva.Estatus = (int)EstadosReserva.Iniciada;
        newReserva.CalcularMonto();

        //Persitencia
        _reservaRepositorio.Crear(newReserva);
        _vehiculoRepositorio.Actualizar(vehiculo, vehiculo.Id);

        response.Dato = newReserva;
        response.IsValida = true;
        response.Mensaje = "Registro Exitoso!!!";

      }
      catch (Exception e)
      {
        response.Mensaje = e.Message;
      }
      return response;
    }

		/// 
		/// <param name="reserva"></param>
		public Repuesta<int> ActualizarReserva(String reservaId,  Reserva reserva){

      var response = new Repuesta<int>();
      try
      {
        //Busqueda
        var reservaRecord = _reservaRepositorio.Detallar(reservaId);

        if (reservaRecord == null)
        {
          response.Dato = 400;
          response.IsValida = false;
          response.Mensaje = "No se encontro la reserva con el identidicador: " + reservaId;
        }
        else
        {
          reservaRecord.Estatus = reserva.Estatus;

          response.Dato = 203;
          response.IsValida = true;
          response.Mensaje = "Actualizaci�n Exitosa!!!";
        }

      }
      catch (Exception e)
      {
        response.Mensaje = e.Message;
      }
      return response;
    }

		/// 
		/// <param name="cliente"></param>
		public Repuesta<Itinerario[]> ConsultarItinerarios(String clienteId, DateTime desde, DateTime hasta){

      var response = new Repuesta<Itinerario[]>();
      try
      {
        var itinerarios = new List<Itinerario>();
        var vehiculos = _vehiculoRepositorio.ListarTodos().Where(v => v.IsDisponible = true).ToArray();

        var cliente = _clienteRepositorio.Detallar(clienteId);

        if (cliente == null)
        {
          response.IsValida = false;
          response.Mensaje = "No se encontro el cliente con el identidicador: " + clienteId;
        }
        else
        {
          foreach (var v in vehiculos)
          {
            var newReserva = new Reserva();
            newReserva.VehiculoInv = v;
            newReserva.ClienteInv = cliente;   
            newReserva.FechaInicio = desde;
            newReserva.FechaFin = hasta;
            newReserva.CalcularMonto();

            var itinerario = new Itinerario();
            itinerario.Monto = newReserva.MontoTotal;
            TimeSpan difFechas = hasta - desde;
            itinerario.Dias = difFechas.Days;
            itinerario.VehiculoInt = v;
            itinerario.ClienteInt = cliente;

            itinerarios.Add(itinerario);
          }
        }

        response.Dato = itinerarios.ToArray();

        if (response.Dato != null)
        {
          response.IsValida = true;
          response.Mensaje = "Consulta Exitosa!!!";
        }

      }
      catch (Exception ex)
      {
        response.Mensaje = ex.Message;
      }

      return response;
    }

    public Repuesta<Reserva[]> ListarAlgunasReservas(int estatus)
    {

      var response = new Repuesta<Reserva[]>();
      try
      {
        var reservas = _reservaRepositorio.ListarTodos().Where(r => r.Estatus == estatus).ToArray();

        response.Dato = reservas;

        if (response.Dato != null)
        {
          response.IsValida = true;
          response.Mensaje = "Consulta Exitosa!!!";
        }

      }
      catch (Exception ex)
      {
        response.Mensaje = ex.Message;
      }

      return response;
    }

  }//end Aplicacion

}//end namespace Aplicacion