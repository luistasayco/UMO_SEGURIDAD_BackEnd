using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoOpcionPorPerfilFindRequest
    {
        public int IdMenu { get; set; }
        public int IdPerfil { get; set; }

        public BE_OpcionxPerfil RetornarOpcionxPerfil()
        {
            return new BE_OpcionxPerfil
            {
                IdMenu = this.IdMenu,
                IdPerfil = this.IdPerfil
            };
        }
    }
}
