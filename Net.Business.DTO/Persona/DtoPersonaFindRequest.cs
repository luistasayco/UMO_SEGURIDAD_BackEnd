using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoPersonaFindRequest
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public BE_Persona RetornaPersona()
        {
            return new BE_Persona
            {
                IdPersona = this.IdPersona,
                Nombre = this.Nombre
            };
        }
    }
}
