using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoMenuActualizarRequest: EntityBase
    {
        public int IdMenu { get; set; }
        public string DescripcionTitulo { get; set; }
        public string Icono { get; set; }
        public string Url { get; set; }
        public int NroNivel { get; set; }
        public Boolean FlgActivo { get; set; }
        public int IdMenuPadre { get; set; }
        public string NombreFormulario { get; set; }
        public BE_Menu RetornarMenu()
        {
            return new BE_Menu
            {
                IdMenu = this.IdMenu,
                DescripcionTitulo = this.DescripcionTitulo,
                Icono = this.Icono,
                Url = this.Url,
                NroNivel = this.NroNivel,
                FlgActivo = this.FlgActivo,
                IdMenuPadre = this.IdMenuPadre,
                NombreFormulario = this.NombreFormulario,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
