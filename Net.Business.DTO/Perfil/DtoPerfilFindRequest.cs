using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoPerfilFindRequest
    {
        public int IdPerfil { get; set; }
        public string DescripcionPerfil { get; set; }
        public BE_Peril RetornarPeril()
        {
            return new BE_Peril
            {
                IdPerfil = this.IdPerfil,
                DescripcionPerfil = this.DescripcionPerfil
            };
        }
    }
}
