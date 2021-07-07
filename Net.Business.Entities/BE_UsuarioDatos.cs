using System.Collections.Generic;

namespace Net.Business.Entities
{
    public class BE_UsuarioDatos
    {
        public int? IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int? IdPersona { get; set; }
        public int? IdPerfil { get; set; }
        public string CodCentroCosto { get; set; }
        public string DesCentroCosto { get; set; }
        public List<BE_Menu> ListaAccesoMenu { get; set; }
    }
}
