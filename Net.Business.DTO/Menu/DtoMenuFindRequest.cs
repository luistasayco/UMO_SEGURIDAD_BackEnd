using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoMenuFindRequest
    {
        public string DescripcionTitulo { get; set; }
        public BE_Menu RetornarMenu()
        {
            return new BE_Menu
            {
                DescripcionTitulo = this.DescripcionTitulo
            };
        }
    }
}
