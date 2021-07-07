using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoOpcionEliminarRequest : EntityBase
    {
        public int IdOpcion { get; set; }

        public BE_Opcion RetornarOpcion()
        {
            return new BE_Opcion
            {
                IdOpcion = this.IdOpcion,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
