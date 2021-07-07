using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoOpcionFindRequest
    {
        public int IdMenu { get; set; }

        public BE_Opcion RetornarOpcion()
        {
            return new BE_Opcion
            {
                IdMenu = this.IdMenu
            };
        }
    }
}
