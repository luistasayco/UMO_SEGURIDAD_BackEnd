using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoPerfilEliminarRequest : EntityBase
    {
        public int IdPerfil { get; set; }
        public BE_Peril RetornarPeril()
        {
            return new BE_Peril
            {
                IdPerfil = this.IdPerfil,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
