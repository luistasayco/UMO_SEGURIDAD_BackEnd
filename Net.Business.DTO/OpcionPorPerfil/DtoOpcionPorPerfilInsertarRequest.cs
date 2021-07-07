using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoOpcionPorPerfilInsertarRequest: EntityBase
    {
        public int IdOpcionxPerfil { get; set; }
        public int IdOpcion { get; set; }
        public int IdPerfil { get; set; }

        public BE_OpcionxPerfil RetornarOpcionxPerfil()
        {
            return new BE_OpcionxPerfil
            {
                IdOpcionxPerfil = this.IdOpcionxPerfil,
                IdOpcion = this.IdOpcion,
                IdPerfil = this.IdPerfil,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
