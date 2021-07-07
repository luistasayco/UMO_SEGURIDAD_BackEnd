using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoOpcionActualizarRequest: EntityBase
    {
        public int IdOpcion { get; set; }
        public string DescripcionOpcion { get; set; }
        public string KeyOpcion { get; set; }

        public BE_Opcion RetornarOpcion()
        {
            return new BE_Opcion
            {
                IdOpcion = this.IdOpcion,
                DescripcionOpcion = this.DescripcionOpcion,
                KeyOpcion = this.KeyOpcion,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
