using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoPersonaActualizarRequest : EntityBase
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NroDocumento { get; set; }
        public string NroTelefono { get; set; }
        public Boolean FlgActivo { get; set; }
        public string CodCentroCosto { get; set; }
        public BE_Usuario EntidadUsuario { get; set; }
        public BE_Persona RetornaPersona()
        {
            return new BE_Persona
            {
                IdPersona = this.IdPersona,
                Nombre = this.Nombre,
                ApellidoPaterno = this.ApellidoPaterno,
                ApellidoMaterno = this.ApellidoMaterno,
                NroDocumento = this.NroDocumento,
                NroTelefono = this.NroTelefono,
                FlgActivo = this.FlgActivo,
                EntidadUsuario = this.EntidadUsuario,
                CodCentroCosto = this.CodCentroCosto,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
