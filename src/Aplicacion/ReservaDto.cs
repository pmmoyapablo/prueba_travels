using Dominio;

namespace Aplicacion
{
  public class ReservaDto
  {
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public double MontoTotal { get; set; }
    public int Estatus { get; set; }
    public bool PagoDestino { get; set; }
    public Cliente Cliente { get; set; }
    public MedioPago Pago { get; set; }
    public Vehiculo Vehiculo { get; set; }
  }
}
