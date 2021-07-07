using Net.Connection.Attributes;
using System;
using System.Data;

namespace Net.Business.Entities
{
    public class EF_Opcion
    {
        /// <summary>
        /// IdUsuario
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdUsuario { get; set; }
        /// <summary>
        /// IdMenu
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdMenu { get; set; }
    }
}
