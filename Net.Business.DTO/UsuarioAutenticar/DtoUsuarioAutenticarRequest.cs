using Net.Business.Entities;
using System.ComponentModel.DataAnnotations;

namespace Net.Business.DTO
{
    public class DtoUsuarioAutenticarRequest
    {
        [Required(ErrorMessage = "Debe Ingresar el Usuario")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Debe Ingresar la clave")]
        public string Clave { get; set; }
        public BE_UsuarioAutenticar UsuarioAutenticar()
        {
            return new BE_UsuarioAutenticar
            {
                Usuario = this.Usuario,
                Clave = this.Clave
            };
        }
    }
}
