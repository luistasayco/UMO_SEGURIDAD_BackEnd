using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoPerfilActualizarRequest : EntityBase
    {
        public int IdPerfil { get; set; }
        public string DescripcionPerfil { get; set; }
        public Boolean FlgActivo { get; set; }
        public BE_Peril RetornarPeril()
        {
            return new BE_Peril
            {
                IdPerfil = this.IdPerfil,
                DescripcionPerfil = this.DescripcionPerfil,
                FlgActivo = this.FlgActivo,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
