using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUsuarioUpdatePassword: EntityBase
    {
        public int IdUsuario { get; set; }
        public string ClaveOrigen { get; set; }

        public BE_Usuario RetornaUsuario()
        {
            return new BE_Usuario
            {
                IdUsuario = this.IdUsuario,
                ClaveOrigen = this.ClaveOrigen,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
