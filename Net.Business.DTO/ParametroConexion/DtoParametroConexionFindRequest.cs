using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoParametroConexionFindRequest
    {
        public int IdParametroConexion { get; set; }
        public BE_ParametroConexion RetornarParametroConexion()
        {
            return new BE_ParametroConexion
            {
                IdParametroConexion = this.IdParametroConexion
            };
        }
    }
}
