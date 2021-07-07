using Net.Connection.Attributes;
using System;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_Usuario: EntityBase
    {
        /// <summary>
        /// IdUsuario
        /// </summary>
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int? IdUsuario { get; set; }
        /// <summary>
        /// IdPersona
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdPersona { get; set; }
        /// <summary>
        /// IdPerfil
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdPerfil { get; set; }
        /// <summary>
        /// DescripcionPerfil
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionPerfil { get; set; }
        /// <summary>
        /// Usuario
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 20, ActionType.Everything)]
        public string Usuario { get; set; }
        /// <summary>
        /// Clave Texto Origen
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 20, ActionType.Everything)]
        public string ClaveOrigen { get; set; }
        /// <summary>
        /// Clave Encriptada
        /// </summary>
        [DBParameter(SqlDbType.Text,0, ActionType.Everything)]
        public string Clave { get; set; }
        /// <summary>
        /// Email del usuario
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string Email { get; set; }
        /// <summary>
        /// Imagen
        /// </summary>
        [DBParameter(SqlDbType.Text, 0, ActionType.Everything)]
        public string Imagen { get; set; }
        /// <summary>
        /// ThemeDark
        /// </summary>
        [DBParameter(SqlDbType.Bit, 0, ActionType.Everything)]
        public Boolean? ThemeDark { get; set; }
        /// <summary>
        /// ThemeColor
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 20, ActionType.Everything)]
        public string ThemeColor { get; set; }
        /// <summary>
        /// TypeMenu
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 20, ActionType.Everything)]
        public string TypeMenu { get; set; }
        /// <summary>
        /// Nombre
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string Nombre { get; set; }
        /// <summary>
        /// Nombre
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 10, ActionType.Everything)]
        public string CodCentroCosto { get; set; }
        /// <summary>
        /// Nombre
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DesCentroCosto { get; set; }

    }
}
