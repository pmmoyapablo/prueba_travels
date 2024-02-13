///////////////////////////////////////////////////////////
//  Cliente.cs
//  Implementation of the Class Cliente
//  Generated by Enterprise Architect
//  Created on:      12-feb.-2024 14:58:12
//  Original author: pmmoy
///////////////////////////////////////////////////////////


namespace Dominio {

using Dominio;
  public class Cliente : Entidad {

		public String Documento { get => Documento; set { ExcepcionDominio.LanzarCuando(string.IsNullOrEmpty(value), "Documento inválido"); Documento = value; } }
		public String Nombres { get => Nombres; set { ExcepcionDominio.LanzarCuando(string.IsNullOrEmpty(value), "Nombres inválidos"); Nombres = value; } }
    public int Edad { get => Edad; set { ExcepcionDominio.LanzarCuando(value < 18, "Edad no permitida"); Edad = value; } }
    public String Email { get => Email; set { ExcepcionDominio.LanzarCuando(string.IsNullOrEmpty(value), "Email inválido"); Email = value; } }
    public String Direccion { get => Direccion; set { ExcepcionDominio.LanzarCuando(string.IsNullOrEmpty(value), "Dirección inválida"); Direccion = value; } }
    public String Ciudad { get => Ciudad; set { ExcepcionDominio.LanzarCuando(string.IsNullOrEmpty(value), "Ciudad inválida"); Ciudad = value; } }
    public String Telefono { get => Telefono; set { ExcepcionDominio.LanzarCuando(string.IsNullOrEmpty(value), "Teléfono inválido"); Telefono = value; } }

    public Vehiculo[] Preferencias { get; set; }

		public Cliente(){
      Preferencias = new Vehiculo[] { };
		}

	}//end Cliente

}//end namespace Dominio