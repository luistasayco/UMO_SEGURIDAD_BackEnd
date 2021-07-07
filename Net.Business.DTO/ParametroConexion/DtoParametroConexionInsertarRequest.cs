using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoParametroConexionInsertarRequest: EntityBase
    {
        public int IdParametroConexion { get; set; }
        public string AplicacionServidor { get; set; }
        public string AplicacionBaseDatos { get; set; }
        public string AplicacionPasswordOriginal { get; set; }
        public string AplicacionUsuario { get; set; }
        public string SapServidor { get; set; }
        public string SapBaseDatos { get; set; }
        public string SapUsuario { get; set; }
        public string SapPasswordOriginal { get; set; }
        public BE_ParametroConexion RetornarParametroConexion()
        {
            return new BE_ParametroConexion
            {
                IdParametroConexion = this.IdParametroConexion,
                AplicacionServidor = this.AplicacionServidor,
                AplicacionBaseDatos = this.AplicacionBaseDatos,
                AplicacionUsuario = this.AplicacionUsuario,
                AplicacionPasswordOriginal = this.AplicacionPasswordOriginal,
                SapServidor = this.SapServidor,
                SapBaseDatos = this.SapBaseDatos,
                SapUsuario = this.SapUsuario,
                SapPasswordOriginal = this.SapPasswordOriginal,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
