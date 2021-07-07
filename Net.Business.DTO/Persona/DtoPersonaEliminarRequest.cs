using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoPersonaEliminarRequest : EntityBase
    {
        public int IdPersona { get; set; }
        public BE_Persona RetornaPersona()
        {
            return new BE_Persona
            {
                IdPersona = this.IdPersona,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
