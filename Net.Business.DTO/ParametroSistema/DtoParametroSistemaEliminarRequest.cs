using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoParametroSistemaEliminarRequest: EntityBase
    {
        public int IdParametrosSistema { get; set; }

        public BE_ParametroSistema RetornaParametroSistema()
        {
            return new BE_ParametroSistema
            {
                IdParametrosSistema = this.IdParametrosSistema,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
