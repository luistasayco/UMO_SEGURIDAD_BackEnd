using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoMenuEliminarRequest: EntityBase
    {
        public int IdMenu { get; set; }
        public BE_Menu RetornarMenu()
        {
            return new BE_Menu
            {
                IdMenu = this.IdMenu,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
