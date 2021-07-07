using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUsuarioFind
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }

        public BE_Usuario UsuarioFind()
        {
            return new BE_Usuario
            {
                IdUsuario = this.IdUsuario,
                Usuario = this.Usuario,
            };
        }
    }
}
