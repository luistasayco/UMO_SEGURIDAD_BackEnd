using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoParametroSistemaFindRequest 
    {
        public int IdParametrosSistema { get; set; }
       
        public BE_ParametroSistema RetornaParametroSistema()
        {
            return new BE_ParametroSistema
            {
                IdParametrosSistema = this.IdParametrosSistema
            };
        }
    }
}
