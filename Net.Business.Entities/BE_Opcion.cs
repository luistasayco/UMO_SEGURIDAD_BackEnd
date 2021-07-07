using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_Opcion: EntityBase
    {
        /// <summary>
        /// IdPersona
        /// </summary>
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int? IdOpcion { get; set; }
        /// <summary>
        /// Nombre
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdMenu { get; set; }
        /// <summary>
        /// Nombre
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string Nombre { get; set; }
        /// <summary>
        /// DescripcionOpcion
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionOpcion { get; set; }
        /// <summary>
        /// KeyOpcion
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 10, ActionType.Everything)]
        public string KeyOpcion { get; set; }
    }
}
