using Net.Business.Entities;
using System.ComponentModel.DataAnnotations;

namespace Net.Business.DTO
{
    public class DtoUsuarioDatosRequest
    {
        [Required(ErrorMessage = "Debe Ingresar el Usuario")]
        public string Usuario { get; set; }
        public BE_UsuarioDatos UsuarioDatos()
        {
            return new BE_UsuarioDatos
            {
                Usuario = this.Usuario.ToUpper()
            };
        }
    }
}
