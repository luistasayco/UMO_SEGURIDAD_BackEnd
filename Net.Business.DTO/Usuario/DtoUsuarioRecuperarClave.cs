using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUsuarioRecuperarClave
    {
        public string Usuario { get; set; }

        public BE_Usuario RetornaUsuario()
        {
            return new BE_Usuario
            {
                Usuario = this.Usuario
            };
        }
    }
}
