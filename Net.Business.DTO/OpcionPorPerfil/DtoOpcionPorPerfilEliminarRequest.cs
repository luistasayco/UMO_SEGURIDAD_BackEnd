using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoOpcionPorPerfilEliminarRequest: EntityBase
    {
        public int IdOpcionxPerfil { get; set; }

        public BE_OpcionxPerfil RetornarOpcionxPerfil()
        {
            return new BE_OpcionxPerfil
            {
                IdOpcionxPerfil = this.IdOpcionxPerfil,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
