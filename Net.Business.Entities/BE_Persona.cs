using Net.Connection.Attributes;
using System;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_Persona : EntityBase
    {
        /// <summary>
        /// IdPersona
        /// </summary>
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int? IdPersona { get; set; }
        /// <summary>
        /// Usuario
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string Usuario { get; set; }
        /// <summary>
        /// Nombre
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string Nombre { get; set; }
        /// <summary>
        /// ApellidoPaterno
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string ApellidoPaterno { get; set; }
        /// <summary>
        /// ApellidoMaterno
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string ApellidoMaterno { get; set; }
        /// <summary>
        /// NombreCompleto
        /// </summary>
        public string NombreCompleto { get => ApellidoPaterno + " " + ApellidoMaterno + " " + Nombre; }
        /// <summary>
        /// NroDocumento
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 20, ActionType.Everything)]
        public string NroDocumento { get; set; }
        /// <summary>
        /// NroTelefono
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 20, ActionType.Everything)]
        public string NroTelefono { get; set; }
        /// <summary>
        /// FlgActivo
        /// </summary>
        [DBParameter(SqlDbType.Bit, 0, ActionType.Everything)]
        public Boolean? FlgActivo { get; set; }
        /// <summary>
        /// DescripcionPerfil
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionPerfil { get; set; }
        /// <summary>
        /// CodCentroCosto
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodCentroCosto { get; set; }
        /// <summary>
        /// DesCentroCosto
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DesCentroCosto { get; set; }
        /// <summary>
        /// EntidadUsuario
        /// </summary>
        public BE_Usuario EntidadUsuario { get; set; }
    }
}
