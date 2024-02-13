///////////////////////////////////////////////////////////
//  ClienteRepositorioEF.cs
//  Implementation of the Class ClienteRepositorioEF
//  Generated by Enterprise Architect
//  Created on:      12-feb.-2024 15:05:15
//  Original author: pmmoy
///////////////////////////////////////////////////////////


using Dominio;
using Aplicacion;

namespace Infraestructura {
	public class ClienteRepositorioEF : RepositorioBaseEF<Cliente>, IClienteRepositorio {

	public ClienteRepositorioEF(Context context) : base(context)
    {}

	}//end ClienteRepositorioEF

}//end namespace Infraestructura